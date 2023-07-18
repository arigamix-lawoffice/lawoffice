using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.IO;
using Tessa.Platform.Plugins;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public abstract class MailSender
    {
        #region Constructors

        protected MailSender(
            ICardServerPermissionsProvider permissionsProvider,
            ICardContentStrategy contentStrategy,
            ICardStreamClientRepository clientRepository,
            ICardRepository extendedRepository,
            IDbScope dbScope,
            IOutboxManager outboxManager,
            IFormattingSettingsCache formattingSettingsCache)
        {
            this.permissionsProvider = permissionsProvider;
            this.contentStrategy = contentStrategy;
            this.clientRepository = clientRepository;
            this.extendedRepository = extendedRepository;
            this.dbScope = dbScope;
            this.outboxManager = outboxManager;
            this.formattingSettingsCache = formattingSettingsCache;
        }

        #endregion

        #region Properties

        public long MaxFilesSizeEmail { get; set; }
        public int MaxAttemptsBeforeDelete { get; set; }
        public IPluginExtensionContext Context { get; set; }

        #endregion

        #region Fields

        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ICardServerPermissionsProvider permissionsProvider;
        private readonly ICardContentStrategy contentStrategy;
        private readonly ICardStreamClientRepository clientRepository;
        private readonly ICardRepository extendedRepository;
        private readonly IDbScope dbScope;
        private readonly IOutboxManager outboxManager;
        private readonly IFormattingSettingsCache formattingSettingsCache;

        protected static Regex MissedFilesRegex =
            new Regex(
                @"<!--\s*[.]missed_files\s*-->",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        protected static Regex OversizedFilesRegex =
            new Regex(
                @"<!--\s*[.]oversized_files\s*-->",
                RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        #endregion

        #region Methods

        public Task StartAsync(ConcurrentQueue<OutboxMessage> queue, CancellationToken cancellationToken = default) =>
            Task.Run(() => this.ProcessAsync(queue, cancellationToken), cancellationToken);

        protected abstract Task<bool> SendMessageAsync(MailSenderMessage message, CancellationToken cancellationToken = default);

        protected bool StopRequested => this.Context is { StopRequested: true };

        protected async Task LogProcessingErrorAsync(OutboxMessage message, object error, CancellationToken cancellationToken = default)
        {
            string errorText = error is Exception ex ? ex.GetFullText() : error?.ToString();

            Logger.Error(
                "Error while processing message ID='{1}', Email='{2}', Subject='{3}'.{0}{4}",
                Environment.NewLine,
                message.ID,
                message.Email,
                message.Subject,
                errorText);

            message.Attempts++;

            if (message.Attempts < this.MaxAttemptsBeforeDelete)
            {
                await this.outboxManager.MarkAsBadMessageAsync(message.ID, message.Attempts, errorText, cancellationToken);
                return;
            }

            Logger.Trace(
                "Message has reached maximum number of attempts to send. Message is deleted from database."
                + " ID='{0}', Email='{1}', Subject='{2}'",
                message.ID,
                message.Email,
                message.Subject);

            await this.outboxManager.DeleteAsync(message.ID, cancellationToken);
        }

        protected void AddMissedFilesInfo(ref string body, List<string> missedFiles, Regex template)
        {
            try
            {
                if (missedFiles.Count > 0)
                {
                    body = template.Replace(body, m =>
                    {
                        var sb = StringBuilderHelper.Acquire(64);
                        foreach (var missedFile in missedFiles)
                        {
                            sb.Append("<p><span class='data_description'>");
                            sb.Append(missedFile);
                            sb.Append("</span></p>");
                        }

                        return sb.ToStringAndRelease();
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogLevel.Error);
            }
        }

        protected async Task ProcessAsync(ConcurrentQueue<OutboxMessage> queue, CancellationToken cancellationToken = default)
        {
            if (queue.Count == 0 || this.StopRequested)
            {
                return;
            }

            await using (this.dbScope.Create())
            {
                while (queue.TryDequeue(out OutboxMessage outboxMessage))
                {
                    Logger.Trace("Processing message ID={0:B}, e-mail \"{1}\", subject: \"{2}\"", outboxMessage.ID, outboxMessage.Email, outboxMessage.Subject);

                    if (string.IsNullOrWhiteSpace(outboxMessage.Email)
                        || !FormattingHelper.EmailRegex.IsMatch(outboxMessage.Email))
                    {
                        await this.LogProcessingErrorAsync(outboxMessage, "Incorrect e-mail format: \"" + outboxMessage.Email + "\"", cancellationToken);
                        continue;
                    }

                    // каждое сообщение обрабатывается со своим идентификатором
                    RuntimeHelper.ServerRequestID = Guid.NewGuid();

                    using (var senderMessage = new MailSenderMessage(outboxMessage))
                    {
                        await this.ProcessMessageAsync(senderMessage, cancellationToken);
                    }

                    if (this.StopRequested)
                    {
                        return;
                    }
                }
            }
        }

        protected async Task ProcessMessageAsync(MailSenderMessage senderMessage, CancellationToken cancellationToken = default)
        {
            IDisposable localizationScope = null;
            IDisposable sessionContext = null;
            try
            {
                CultureInfo uiCulture = CultureInfo.GetCultureInfo(senderMessage.Info.LanguageCode ?? LocalizationManager.EnglishLanguageCode);
                string formatName = senderMessage.Info.FormatName ?? LocalizationManager.EnglishLanguageCode;

                localizationScope = LocalizationManager.CreateScope(
                    uiCulture,
                    await this.formattingSettingsCache.TryGetCultureInfoAsync(formatName, cancellationToken)
                    ?? CultureInfo.GetCultureInfo(formatName));

                if (senderMessage.Info.UserID.HasValue)
                {
                    var token =
                        this.Context.Session.CreateNestedSessionToken(
                            senderMessage.Info.UserID.Value,
                            senderMessage.Info.UserName);

                    var offset =
                        TimeSpan.FromMinutes(Convert.ToDouble(senderMessage.Info.TimeZoneUtcOffsetMinutes));
                    token.UtcOffset = offset;
                    token.TimeZoneUtcOffset = offset;
                    token.CalendarID = senderMessage.Info.CalendarID!.Value;
                    token.Seal();

                    sessionContext = SessionContext.Create(token);
                }

                Logger.Trace("Getting files.");
                bool hasUnfinishedFiles = await this.FillFilesAndCheckHasUnfinishedFilesAsync(senderMessage, cancellationToken);

                if (!hasUnfinishedFiles)
                {
                    bool success = await this.SendMessageAsync(senderMessage, cancellationToken);

                    if (success)
                    {
                        Logger.Trace("Deleting message from database.");
                        await this.outboxManager.DeleteAsync(senderMessage.Message.ID, cancellationToken);

                        var request = new CardRequest
                        {
                            RequestType = CardRequestTypes.MailSent,
                            CardID = senderMessage.Message.ID,
                            Info =
                            {
                                ["MailInfo"] = senderMessage.Info.GetStorage(),
                                ["Email"] = senderMessage.Message.Email,
                                ["Body"] = senderMessage.Message.Body,
                                ["Subject"] = senderMessage.Message.Subject
                            }
                        };

                        CardResponse response = await this.extendedRepository.RequestAsync(request, cancellationToken);
                        Logger.LogResult(response.ValidationResult.Build());
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                await this.LogProcessingErrorAsync(senderMessage.Message, ex, cancellationToken);
            }
            finally
            {
                localizationScope?.Dispose();
                sessionContext?.Dispose();
            }
        }

        private async Task<bool> FillFilesAndCheckHasUnfinishedFilesAsync(
            MailSenderMessage mailSenderMessage,
            CancellationToken cancellationToken = default)
        {
            var tempFiles = new List<ITempFile>();
            List<string> oversizedFilesTemp = new List<string>();

            try
            {
                Guid? cardID = mailSenderMessage.Info.CardID;
                Guid? cardTypeID = mailSenderMessage.Info.CardTypeID;
                string cardTypeName = mailSenderMessage.Info.CardTypeName;

                long[] totalFileSize = { 0L }; // closure
                long maxFileSize = this.MaxFilesSizeEmail * 1000;

                ListStorage<MailFile> files = mailSenderMessage.Info.Files;
                if (files is { Count: > 0 })
                {
                    ITempFile[] lastTempFile = { null };

                    foreach (MailFile file in files)
                    {
                        string fileName = await LocalizationManager.LocalizeAsync(file.FileName, cancellationToken);
                        if (string.IsNullOrEmpty(fileName))
                        {
                            fileName = "file";
                        }

                        Guid fileID = file.FileID;
                        Guid versionID = file.VersionID;
                        string fileTypeName = file.FileTypeName;

                        Logger.Trace("Getting file '{0}'. File ID: '{1}'. Version ID: '{2}'. Card ID: '{3}'",
                            fileName, fileID, versionID, cardID);

                        string missedFile = string.Format(
                            await LocalizationManager.GetStringAsync("Common_Approval_MissedFileMessage", cancellationToken),
                            fileName,
                            fileID,
                            versionID);

                        string oversizeFile = string.Format(
                            await LocalizationManager.GetStringAsync("Common_Approval_FileOversizeMessage", cancellationToken),
                            fileName,
                            fileID,
                            versionID);

                        try
                        {
                            if (file.IsVirtual)
                            {
                                var request = new CardGetFileContentRequest
                                {
                                    CardID = cardID,
                                    CardTypeID = cardTypeID,
                                    CardTypeName = cardTypeName,
                                    FileID = fileID,
                                    VersionRowID = versionID,
                                    FileName = fileName,
                                    FileTypeName = fileTypeName,
                                    Info = file.Info,
                                };

                                request.SetForbidStoringHistory(true);

                                this.permissionsProvider.SetFullPermissions(request);

                                CardGetFileContentResponse response =
                                    await this.clientRepository.GetFileContentAsync(
                                        request,
                                        async (stream, ct) =>
                                        {
                                            ITempFile tempFile = TempFile.Acquire(fileName);

                                            try
                                            {
                                                long size;
                                                await using (FileStream tempFileStream = FileHelper.Create(tempFile.Path))
                                                {
                                                    await stream.CopyToAsync(tempFileStream, ct);
                                                    size = tempFileStream.Position;
                                                }

                                                totalFileSize[0] += size;

                                                if (totalFileSize[0] <= maxFileSize)
                                                {
                                                    lastTempFile[0] = tempFile;
                                                    tempFiles.Add(tempFile);
                                                }
                                                else
                                                {
                                                    lastTempFile[0] = null;
                                                    oversizedFilesTemp.Add(oversizeFile);
                                                    tempFile.Dispose();
                                                }
                                            }
                                            catch (Exception)
                                            {
                                                tempFile.Dispose();
                                                throw;
                                            }
                                        },
                                        cancellationToken: cancellationToken);

                                // кастомную инфу из расширений прокидываем обратно в MailFile.Info,
                                // чтобы можно было достучаться в расширениях после успешной отправки письма CardRequestTypes.MailSent
                                StorageHelper.Merge(response.Info, file.Info);

                                ValidationResult validationResult = response.ValidationResult.Build();
                                Logger.LogResult(validationResult);

                                if (validationResult.IsSuccessful && lastTempFile[0] is not null)
                                {
                                    string suggestedFileName = response.TryGetSuggestedFileName();
                                    if (!string.IsNullOrEmpty(suggestedFileName))
                                    {
                                        // если имена одинаковые без учёта регистра, то Rename ничего не сделает
                                        lastTempFile[0].Rename(suggestedFileName);
                                    }
                                }
                            }
                            else
                            {
                                bool fileIsInProgress = await this.IsFileVersionInProgressAsync(versionID, cancellationToken);
                                if (fileIsInProgress)
                                {
                                    // если есть ещё недозагруженный контент, то отменяем отправку письма, но не помечаем письмо как удалённое
                                    Logger.Trace(
                                        "There are at least one attached file versionID='{0}' with uploading still in progress. Mail is postponed: \"{1}\", ID='{2}'.",
                                        versionID,
                                        mailSenderMessage.Message.Subject,
                                        mailSenderMessage.Message.ID);

                                    return true;
                                }

                                CardFileSourceType source = file.Source.GetValueOrDefault();

                                var context = new CardContentContext(cardID.Value, fileID, versionID, source,
                                    new ValidationResultBuilder());

                                Stream contentStream = null;

                                try
                                {
                                    contentStream = await this.contentStrategy.GetAsync(context, cancellationToken);

                                    ValidationResult validationResult = context.ValidationResult.Build();
                                    Logger.LogResult(validationResult);

                                    if (!validationResult.IsSuccessful)
                                    {
                                        mailSenderMessage.MissedFiles.Add(missedFile);
                                        continue;
                                    }

                                    string modifiedByName = (FileHelper.RemoveInvalidFileNameChars(file.ModifiedByName) ?? string.Empty).Trim();
                                    string validFileName = FileHelper.RemoveInvalidFileNameChars(fileName, FileHelper.InvalidCharReplacement);

                                    string actualFileName = string.IsNullOrEmpty(modifiedByName)
                                        ? validFileName
                                        : Path.GetFileNameWithoutExtension(validFileName)
                                        + " (" + modifiedByName + ")"
                                        + Path.GetExtension(validFileName);

                                    ITempFile tempFile = TempFile.Acquire(actualFileName);

                                    try
                                    {
                                        long size;
                                        await using (FileStream tempFileStream = FileHelper.Create(tempFile.Path))
                                        {
                                            await contentStream.CopyToAsync(tempFileStream, cancellationToken);
                                            size = tempFileStream.Position;
                                        }

                                        totalFileSize[0] += size;

                                        if (totalFileSize[0] <= maxFileSize)
                                        {
                                            lastTempFile[0] = tempFile;
                                            tempFiles.Add(tempFile);
                                        }
                                        else
                                        {
                                            lastTempFile[0] = null;
                                            oversizedFilesTemp.Add(oversizeFile);
                                            tempFile.Dispose();
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        tempFile.Dispose();
                                        throw;
                                    }
                                }
                                catch (OperationCanceledException)
                                {
                                    throw;
                                }
                                catch (Exception ex)
                                {
                                    Logger.LogException(ex, LogLevel.Error);
                                    mailSenderMessage.MissedFiles.Add(missedFile);
                                }
                                finally
                                {
                                    if (contentStream != null)
                                    {
                                        await contentStream.DisposeAsync();
                                    }
                                }
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            Logger.LogException(ex, LogLevel.Error);
                        }
                    }
                }

                mailSenderMessage.MessageFiles.AddRange(tempFiles);
                mailSenderMessage.OversizedFiles.AddRange(oversizedFilesTemp);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, LogLevel.Error);
            }

            return false;
        }

        private async Task<bool> IsFileVersionInProgressAsync(Guid versionID, CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.Create())
            {
                var db = this.dbScope.Db;

                return await db
                    .SetCommand(
                        this.dbScope.BuilderFactory
                            .Select().V(true)
                            .From("FileVersions").NoLock()
                            .Where().C("RowID").Equals().P("VersionID")
                            .And().C("StateID").Equals().V((int) CardFileVersionState.Uploading)
                            .Build(),
                        db.Parameter("VersionID", versionID))
                    .LogCommand()
                    .ExecuteAsync<bool>(cancellationToken);
            }
        }

        #endregion
    }
}
