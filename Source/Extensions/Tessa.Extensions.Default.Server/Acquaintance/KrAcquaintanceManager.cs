using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Acquaintance;
using Tessa.Localization;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Server.Acquaintance
{
    public sealed class KrAcquaintanceManager : IKrAcquaintanceManager
    {
        #region Fields

        private readonly ITransactionStrategy transactionStrategy;
        private readonly INotificationManager notificationManager;
        private readonly IRoleGetStrategy roleGetStrategy;
        private readonly INotificationRoleAggregator roleAggregator;
        private readonly IDbScope dbScope;
        private readonly ISession session;
        private readonly IPlaceholderManager plachHolderManager;
        private readonly INotificationDefaultLanguagePicker languagePicker;
        private readonly IFormattingSettingsCache formattingSettingsCache;
        private readonly IUnityContainer container;

        #endregion

        #region Consts

        public const int CommentMaxLength = 440;

        private readonly Regex commentBlockRegex = new Regex("<comment_block>.+</comment_block>", RegexOptions.Singleline);

        #endregion

        #region Constructors

        public KrAcquaintanceManager(
            ITransactionStrategy transactionStrategy,
            INotificationManager notificationManager,
            IRoleGetStrategy roleGetStrategy,
            INotificationRoleAggregator roleAggregator,
            IDbScope dbScope,
            ISession session,
            IPlaceholderManager plachHolderManager,
            INotificationDefaultLanguagePicker languagePicker,
            IFormattingSettingsCache formattingSettingsCache,
            IUnityContainer container)
        {
            this.transactionStrategy = transactionStrategy;
            this.notificationManager = notificationManager;
            this.roleGetStrategy = roleGetStrategy;
            this.roleAggregator = roleAggregator;
            this.dbScope = dbScope;
            this.session = session;
            this.plachHolderManager = plachHolderManager;
            this.languagePicker = languagePicker;
            this.formattingSettingsCache = formattingSettingsCache;
            this.container = container;
        }

        #endregion

        #region IKrAcquaintanceManager Implementation

        public async Task<ValidationResult> SendAsync(
            Guid mainCardID,
            IReadOnlyList<Guid> roleIDList,
            bool excludeDeputies = false,
            string comment = null,
            string placeholderAliases = null,
            Dictionary<string, object> info = null,
            Guid? notificationCardID = null,
            Guid? senderID = null,
            bool addSuccessMessage = false,
            CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResultBuilder();

            await this.transactionStrategy.ExecuteInTransactionAsync(
                validationResult,
                async p =>
                {
                    (string defaultLanguageCode, string defaultFormatName) = await this.languagePicker.GetDefaultLanguageAsync(p.CancellationToken);
                    comment ??= string.Empty;

                    var aliasPlaceholderContext = new AliasPlaceholderContext().ParseFromMetadata(placeholderAliases);
                    NotificationRecipient[] users =
                        (await this.roleAggregator.AggregateRolesAsync(
                            roleIDList,
                            mainCardID,
                            excludeDeputies,
                            p.CancellationToken))
                        .ToArray();

                    var userCount = users.Length;
                    var usersByLanguage = users
                        .GroupBy(x =>
                        {
                            string languageCode = string.IsNullOrEmpty(x.LanguageCode) ? defaultLanguageCode : x.LanguageCode;
                            string formatName = string.IsNullOrEmpty(x.FormatName) ? defaultFormatName : x.FormatName;
                            return (languageCode, formatName);
                        })
                        .ToDictionary(
                            x => x.Key,
                            x => x.Select(y => y.UserID));

                    string senderName = this.session.User.Name;
                    if (senderID.HasValue)
                    {
                        var role = await this.roleGetStrategy.GetRoleParamsAsync(senderID.Value, p.CancellationToken);
                        if (role.Type is not null)
                        {
                            senderName = role.Name;
                        }
                        else
                        {
                            senderID = this.session.User.ID;
                        }
                    }
                    else
                    {
                        senderID = this.session.User.ID;
                    }

                    foreach (var languageUsersPair in usersByLanguage)
                    {
                        (string languageCode, string formatName) = languageUsersPair.Key;
                        using var _ = LocalizationManager.CreateScope(
                            CultureInfo.GetCultureInfo(languageCode),
                            await this.formattingSettingsCache.TryGetCultureInfoAsync(formatName, cancellationToken)
                            ?? CultureInfo.GetCultureInfo(formatName));

                        var generatedComment = await this.ReplaceAsync(comment, mainCardID, aliasPlaceholderContext, info, validationResult, p.CancellationToken);
                        var dbComment = generatedComment.ReplaceLineEndingsAndTrim().NormalizeSpaces().Limit(CommentMaxLength);
                        var db = p.DbScope.Db;
                        var builder = p.DbScope.BuilderFactory;

                        // добавляем комментарий в БД
                        Guid? commentID = null;
                        if (!string.IsNullOrEmpty(dbComment))
                        {
                            commentID = Guid.NewGuid();

                            await db
                                .SetCommand(
                                    builder
                                        .InsertInto("AcquaintanceComments", "ID", "Comment")
                                        .Values(v => v.P("ID").P("Comment"))
                                        .Build(),
                                    db.Parameter("ID", commentID.Value),
                                    db.Parameter("Comment", dbComment))
                                .LogCommand()
                                .ExecuteNonQueryAsync(p.CancellationToken);
                        }

                        DataParameter? userIds = null;
                        await db
                            .SetCommand(
                                builder
                                    .InsertInto("AcquaintanceRows",
                                        "ID", "CardID", "SenderID", "SenderName", "UserID", "UserName",
                                        "IsReceived", "Sent", "Received", "CommentID")
                                    .Select()
                                    .NewGuid().P("CardID", "SenderID", "SenderName").C("r", "ID", "Name")
                                    .V(false).P("Sent").V(null).P("CommentID")
                                    .From(b => b
                                            .Select().C("r", "ID", "Name")
                                            .From("Roles", "r").NoLock()
                                            .Where().C("r", "ID").InArray(languageUsersPair.Value, nameof(userIds), out userIds),
                                        "r")
                                    .Build(),
                                new DataParameters(db)
                                {
                                    ("CardID", mainCardID),
                                    ("SenderID", senderID),
                                    ("SenderName", senderName),
                                    ("Sent", DateTime.UtcNow),
                                    ("CommentID", commentID),
                                    userIds
                                }.ToArray())
                            .LogCommand()
                            .ExecuteNonQueryAsync(p.CancellationToken);

                        if (!p.ValidationResult.IsSuccessful())
                        {
                            p.ReportError = true;
                            return;
                        }
                    }

                    p.ValidationResult.Add(
                        await this.notificationManager.SendUsersAsync(
                            notificationCardID ?? GetDefaultNotificationID(),
                            users,
                            new NotificationSendContext()
                            {
                                MainCardID = mainCardID,
                                IgnoreUserSessions = true,
                                Info = info,
                                ModifyEmailActionAsync = (email, ct) =>
                                {
                                    if (email.BodyTemplate != null)
                                    {
                                        string newBody = string.IsNullOrWhiteSpace(comment)
                                            ? this.commentBlockRegex.Replace(email.BodyTemplate, string.Empty)
                                            : email.BodyTemplate.Replace("{{comment}}", comment.Trim(), StringComparison.Ordinal);

                                        email.BodyTemplate = newBody.Replace("{{sender}}", senderName, StringComparison.Ordinal);
                                    }

                                    email.PlaceholderAliases.ParseFromMetadata(placeholderAliases);
                                    return Task.CompletedTask;
                                }
                            },
                            p.CancellationToken));

                    if (p.ValidationResult.IsSuccessful())
                    {
                        if (addSuccessMessage)
                        {
                            p.ValidationResult.AddInfo(this, "$KrMessages_Acquaintance_Completed", userCount);
                        }
                    }
                    else
                    {
                        p.ReportError = true;
                    }
                },
                cancellationToken);

            return validationResult.Build();
        }

        #endregion

        #region Private Methods

        private static Guid GetDefaultNotificationID() => DefaultNotifications.AcquaintanceID;

        private async Task<string> ReplaceAsync(
            string text,
            Guid mainCardID,
            IAliasPlaceholderContext aliasPlaceholderContext,
            Dictionary<string, object> info,
            IValidationResultBuilder validationResult,
            CancellationToken cancellationToken = default)
        {
            var placeholderInfo = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                { PlaceholderHelper.ContextKey, null },
                { PlaceholderHelper.SessionKey, this.session },
                { PlaceholderHelper.UnityContainerKey, this.container },
                { PlaceholderHelper.DbScopeKey, this.dbScope },
                { PlaceholderHelper.CardIDKey, mainCardID },
            };

            if (info != null)
            {
                StorageHelper.Merge(info, placeholderInfo);
            }

            var document = new StringPlaceholderDocument(text);
            var result = await this.plachHolderManager.FindAndReplaceAsync(
                document,
                placeholderInfo,
                createAliasContextFuncAsync: (ctx, ct) => new ValueTask<IAliasPlaceholderContext>(aliasPlaceholderContext),
                cancellationToken: cancellationToken);

            validationResult.Add(result);
            return document.Text;
        }

        #endregion
    }
}
