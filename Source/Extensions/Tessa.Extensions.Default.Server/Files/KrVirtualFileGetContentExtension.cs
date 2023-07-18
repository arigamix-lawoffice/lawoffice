using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Files.VirtualFiles;
using Tessa.Extensions.Platform.Shared.Cards;
using Tessa.FileConverters;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение на загрузку конткнта виртуальных файлов из справочника "Виртуальные файлы".
    /// </summary>
    public sealed class KrVirtualFileGetContentExtension :
        CardGetFileContentExtension
    {
        #region Fields

        private readonly IKrVirtualFileManager krVirtualFileManager;
        private readonly IKrVirtualFileCache krVirtualFileCache;
        private readonly ICardStreamServerRepository cardStreamRepository;
        private readonly ISignedSessionTokenProvider sessionTokenProvider;

        #endregion

        #region Constructors

        public KrVirtualFileGetContentExtension(
            IKrVirtualFileManager krVirtualFileManager,
            IKrVirtualFileCache krVirtualFileCache,
            ICardStreamServerRepository cardStreamRepository,
            ISignedSessionTokenProvider sessionTokenProvider)
        {
            this.krVirtualFileManager = krVirtualFileManager;
            this.krVirtualFileCache = krVirtualFileCache;
            this.cardStreamRepository = cardStreamRepository;
            this.sessionTokenProvider = sessionTokenProvider;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            if (context.Response is not null
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var cardID = context.Request.CardID;
            var fileID = context.Request.FileID;
            if (!cardID.HasValue || !fileID.HasValue)
            {
                return;
            }

            if (!context.Request.VersionRowID.HasValue)
            {
                await using (context.DbScope.Create())
                {
                    context.Request.VersionRowID = await context.DbScope.Db
                    .SetCommand(
                        context.DbScope.BuilderFactory
                            .Select()
                            .C("FileVersionID")
                            .From("KrVirtualFiles").NoLock()
                            .Where()
                            .C("FileID").Equals().P("FileID")
                            .Build(),
                        context.DbScope.Db.Parameter("FileID", context.Request.FileID.Value))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(context.CancellationToken);
                }
            }

            var versionID = context.Request.VersionRowID;
            if (!versionID.HasValue)
            {
                return;
            }

            var userToken = SignedSessionToken.TryGet(context.Request.Info);

            ISessionToken sessionToken = null;
            CultureInfo clientCulture = null;
            CultureInfo clientUICulture = null;

            if (userToken is not null)
            {
                var userTokenValidationResult = await this.sessionTokenProvider.ValidateTokenAsync(userToken, context.CancellationToken);
                if (!userTokenValidationResult.IsSuccessful)
                {
                    context.ValidationResult.Add(userTokenValidationResult);
                    return;
                }

                sessionToken = userToken.SessionToken;
                clientCulture = sessionToken.Culture;
                clientUICulture = sessionToken.UICulture;
            }
            else if (context.Request.ServiceType == CardServiceType.Default)
            {
                // Временное решение для поддержки зависших операций
                // TODO: remove
                var userID = context.Request.Info.TryGet<Guid?>("UserID");
                var userName = context.Request.Info.TryGet<string>("UserName");
                TimeSpan? clientUtcOffset = context.Request.Info.TryGet<long?>("ClientUtcOffsetTicks") is long ticks
                    ? new TimeSpan(ticks)
                    : null;
                clientCulture = context.Request.Info.TryGet<string>("ClientCulture") is { } cultureName
                    ? CultureInfo.GetCultureInfo(cultureName)
                    : null;
                clientUICulture = context.Request.Info.TryGet<string>("ClientUICulture") is { } uiCultureName
                    ? CultureInfo.GetCultureInfo(uiCultureName)
                    : null;
                if (userID.HasValue)
                {
                    sessionToken =
                        new SessionToken(
                            userID.Value,
                            userName,
                            serverCode: context.Session.ServerCode,
                            instanceName: context.Session.InstanceName,
                            utcOffset: clientUtcOffset,
                            timeZoneUtcOffset: clientUtcOffset,
                            seal: true);
                }
            }

            // Если с сервера передана информация о сотруднике или языке, для кого генерируется файл, то используем эту информацию.
            await FileTemplateHelper.ExecuteInSessionContextAsync(context, sessionToken, clientUICulture, clientCulture, async ctx =>
            {
                // Проверяем доступ к контенту файлов только при клиентских запросах или если есть подписанный токен сессии
                if (ctx.Request.ServiceType != CardServiceType.Default || userToken is not null)
                {
                    ctx.ValidationResult.Add(
                        await this.krVirtualFileManager.CheckAccessForFileAsync(cardID.Value, fileID.Value, ctx.CancellationToken));

                    if (!ctx.ValidationResult.IsSuccessful())
                    {
                        return;
                    }
                }

                var virtualFile = await this.krVirtualFileCache.TryGetAsync(fileID.Value, ctx.CancellationToken);
                var version = virtualFile?.Versions.FirstOrDefault(x => x.ID == versionID.Value);
                if (version is null)
                {
                    return;
                }

                var requestInfo = new Dictionary<string, object>(StringComparer.Ordinal);
                userToken?.Set(requestInfo);
                FileConverterFormat? converterFormat = context.Request.TryGetConverterFormat();
                if (converterFormat.HasValue)
                {
                    requestInfo.SetConverterFormat(converterFormat.Value);
                }

                var result = await this.cardStreamRepository.GenerateFileFromTemplateAsync(
                    version.FileTemplateID,
                    cardID,
                    info: ctx.Request.Info,
                    requestInfo: requestInfo,
                    cancellationToken: ctx.CancellationToken);

                result.Response.ValidationResult.Add(ctx.ValidationResult);
                ctx.Response = result.Response;

                string fileName = await this.krVirtualFileManager.GetSuggestedFileNameAsync(
                    version.Name,
                    cardID: ctx.Request.CardID,
                    cancellationToken: ctx.CancellationToken);

                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = ctx.Request.FileName;
                }

                if (!string.IsNullOrEmpty(fileName))
                {
                    ctx.Response.SetSuggestedFileName(fileName);
                }

                ctx.ContentFuncAsync = result.GetContentOrThrowAsync;
            });
        }

        #endregion
    }
}
