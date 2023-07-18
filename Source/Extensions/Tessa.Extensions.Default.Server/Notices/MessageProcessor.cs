using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Unity;

namespace Tessa.Extensions.Default.Server.Notices
{
    public class MessageProcessor :
        IMessageProcessor
    {
        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private readonly Func<IEnumerable<IMessageHandler>> getMessageHandlersFunc;

        private IMessageHandler[] messageHandlers;

        #endregion

        #region Constructors

        public MessageProcessor(
            [Dependency(CardRepositoryNames.ExtendedWithoutTransactionAndLocking)]
            ICardRepository extendedRepositoryWithoutTransaction,
            ICardTransactionStrategy cardTransactionStrategy,
            ICardGetStrategy cardGetStrategy,
            ICardMetadata cardMetadata,
            IDbScope dbScope,
            ISession session,
            ICardServerPermissionsProvider permissionsProvider,
            IFormattingSettingsCache formattingSettingsCache,
            SetProcessorTokenAction setSessionToken,
            Func<IEnumerable<IMessageHandler>> getMessageHandlersFunc)
        {
            this.ExtendedRepositoryWithoutTransaction = extendedRepositoryWithoutTransaction;
            this.CardTransactionStrategy = cardTransactionStrategy;
            this.CardGetStrategy = cardGetStrategy;
            this.CardMetadata = cardMetadata;
            this.DbScope = dbScope;
            this.Session = session;
            this.PermissionsProvider = permissionsProvider;
            this.FormattingSettingsCache = formattingSettingsCache;
            this.SetSessionToken = setSessionToken;
            this.getMessageHandlersFunc = getMessageHandlersFunc;
        }

        #endregion

        #region Protected Properties

        protected IDbScope DbScope { get; }

        protected ISession Session { get; }

        protected ICardRepository ExtendedRepositoryWithoutTransaction { get; }

        protected ICardMetadata CardMetadata { get; }

        protected ICardGetStrategy CardGetStrategy { get; }

        protected ICardServerPermissionsProvider PermissionsProvider { get; }

        protected IFormattingSettingsCache FormattingSettingsCache { get; }

        protected ICardTransactionStrategy CardTransactionStrategy { get; }

        protected SetProcessorTokenAction SetSessionToken { get; }

        #endregion

        #region Protected Methods

        protected virtual IEnumerable<IMessageHandler> GetMessageHandlers() =>
            this.messageHandlers ??= this.getMessageHandlersFunc()
                .OrderByAttributeAndType()
                .ToArray();

        protected virtual async Task<ISessionToken> TryFindUserByEmailAsync(
            NoticeMessage message,
            CancellationToken cancellationToken = default)
        {
            var db = this.DbScope.Db;
            db
                .SetCommand(
                    this.DbScope.BuilderFactory
                        .Select().Top(1)
                        .C("pr", "ID", "Name")
                        .Coalesce(b => b.C("s", "LanguageCode").C("kr", "NotificationsDefaultLanguageCode"))
                        .Coalesce(b => b.C("s", "FormatName").C("kr", "NotificationsDefaultFormatName"))
                        .From("PersonalRoles", "pr").NoLock()
                        .LeftJoin(CardSatelliteHelper.SatellitesSectionName, "sat").NoLock()
                        .On().C("sat", CardSatelliteHelper.MainCardIDColumn).Equals().C("pr", "ID")
                        .And().C("sat", "TypeID").Equals().V(RoleHelper.PersonalRoleSatelliteTypeID)
                        .LeftJoin("PersonalRoleSatellite", "s").NoLock()
                        .On().C("s", "ID").Equals().C("sat", "ID")
                        .CrossJoin("KrSettings", "kr").NoLock()
                        .Where().LowerC("pr", "Email").Equals().LowerP("Email")
                        .Limit(1)
                        .Build(),
                    db.Parameter("Email", SqlHelper.LimitString(message.From, RoleHelper.UserEmailMaxLength)))
                .LogCommand();

            await using DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken);
            if (!await reader.ReadAsync(cancellationToken))
            {
                logger.Error("User isn't found. Email: \"{0}\"", message.From);
                return null;
            }

            Guid userID = reader.GetGuid(0);
            string userName = reader.GetString(1);

            string languageCode = reader.GetNullableString(2);
            if (string.IsNullOrEmpty(languageCode))
            {
                languageCode = LocalizationManager.EnglishLanguageCode;
            }

            string formatName = reader.GetNullableString(3);
            if (string.IsNullOrEmpty(formatName))
            {
                formatName = LocalizationManager.EnglishLanguageCode;
            }

            CultureInfo culture = CultureInfo.GetCultureInfo(formatName);
            CultureInfo uiCulture = CultureInfo.GetCultureInfo(languageCode);

            return new SessionToken(
                userID,
                userName,
                UserAccessLevel.Regular,
                culture: culture,
                uiCulture: uiCulture);
        }


        protected virtual Task<Guid?> GetCardIDAsync(
            Guid taskID,
            CancellationToken cancellationToken = default) =>
            this.DbScope.Db
                .SetCommand(
                    this.DbScope.BuilderFactory
                        .Select().C("ID")
                        .From("Tasks").NoLock()
                        .Where().C("RowID").Equals().P("RowID")
                        .Build(),
                    this.DbScope.Db.Parameter("RowID", taskID))
                .ExecuteAsync<Guid?>(cancellationToken);


        protected virtual async Task CompleteTaskAsync(
            Guid taskRowID,
            IMessageInfo info,
            NoticeMessage message,
            IMessageHandler handler,
            CancellationToken cancellationToken = default)
        {
            Guid? cardID = await this.GetCardIDAsync(taskRowID, cancellationToken);
            if (!cardID.HasValue)
            {
                logger.Error("Could not find task by identifier. TaskID={0:B}", taskRowID.ToString());
                return;
            }

            var validationResult = new ValidationResultBuilder();

            await this.CardTransactionStrategy
                .ExecuteInWriterLockAsync(
                    (Guid) cardID,
                    CardComponentHelper.DoNotCheckVersion,
                    validationResult,
                    async p =>
                    {
                        // загружаем карточку с нужным заданием
                        // потом помечаем задание как завершённое с вариантом "Согласовать" или "Не согласовать"
                        // сохраняем карточку, не забыв вызвать card.RemoveAllButChanged()

                        // здесь нельзя делать RestrictTasks, т.к. от наличия видимых заданий могут зависеть права на карточку;
                        // также нельзя делать RestrictTaskSections, т.к. некоторые расширения требуют секций у заданий (такие как WfTasksServerGetExtension)

                        var getRequest = new CardGetRequest
                        {
                            CardID = cardID,
                            GetMode = CardGetMode.ReadOnly,
                            RestrictionFlags =
                                CardGetRestrictionFlags.RestrictFiles
                                | CardGetRestrictionFlags.RestrictTaskCalendar
                                | CardGetRestrictionFlags.RestrictTaskHistory
                        };

                        getRequest.IgnoreButtons();

                        CardGetResponse getResponse = await this.ExtendedRepositoryWithoutTransaction.GetAsync(getRequest, p.CancellationToken);
                        ValidationResult getResult = getResponse.ValidationResult.Build();
                        logger.LogResult(getResult);

                        if (!getResult.IsSuccessful)
                        {
                            p.ReportError = true;
                            return;
                        }

                        Card card = getResponse.Card;
                        card.Tasks.Clear();

                        IList<CardGetContext> taskContextList =
                            await this.CardGetStrategy.TryLoadTaskInstancesAsync(
                                (Guid) cardID,
                                card,
                                p.DbScope.Db,
                                this.CardMetadata,
                                p.ValidationResult,
                                this.Session,
                                loadCalendarInfo: false,
                                taskRowIDList: new[] { taskRowID },
                                cancellationToken: p.CancellationToken);

                        if (taskContextList.Count == 0)
                        {
                            logger.Error(
                                "User UserName=\"{0}\", Email=\"{1}\", UserID={2:B}, doesn't have permissions to complete task"
                                + " TaskID={3:B} for card CardID={4:B}, or task has been completed already.",
                                this.Session.User.Name,
                                message.From,
                                this.Session.User.ID,
                                taskRowID,
                                cardID);

                            p.ReportError = true;
                            return;
                        }

                        CardTask taskToComplete = card.Tasks[0];

                        // Получаем секции задания
                        CardGetContext taskContext = taskContextList[0];
                        await this.CardGetStrategy.LoadSectionsAsync(taskContext, p.CancellationToken);

                        try
                        {
                            var context = new MessageHandlerContext(
                                info,
                                message,
                                this.Session,
                                p.DbScope.Db,
                                p.DbScope.BuilderFactory,
                                card,
                                taskToComplete,
                                p.CancellationToken);

                            await handler.HandleAsync(context);

                            if (context.Cancel)
                            {
                                p.ReportError = true;
                                return;
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception ex)
                        {
                            logger.LogException($"Message handling has failed. From: \"{message.From}\", Subject: \"{message.Subject}\".", ex);
                            p.ReportError = true;
                            return;
                        }

                        string cardDigest = await this.ExtendedRepositoryWithoutTransaction.GetDigestAsync(
                            card, CardDigestEventNames.ActionHistoryMobileApproval, p.CancellationToken);

                        card.RemoveAllButChanged();

                        CardStoreRequest storeRequest = new CardStoreRequest { Card = card };
                        storeRequest.SetDigest(cardDigest);
                        this.PermissionsProvider.SetFullPermissions(storeRequest);

                        CardStoreResponse storeResponse = await this.ExtendedRepositoryWithoutTransaction.StoreAsync(storeRequest, p.CancellationToken);
                        ValidationResult storeResult = storeResponse.ValidationResult.Build();
                        logger.LogResult(storeResult);

                        if (!storeResult.IsSuccessful)
                        {
                            p.ReportError = true;
                        }
                    },
                    cancellationToken: cancellationToken);

            logger.LogResult(validationResult);
        }

        #endregion

        #region IMessageProcessor Methods

        public virtual async Task ProcessMessageAsync(NoticeMessage message, CancellationToken cancellationToken = default)
        {
            // каждое сообщение обрабатывается со своим идентификатором
            RuntimeHelper.ServerRequestID = Guid.NewGuid();

            await using var _ = this.DbScope.Create();

            ISessionToken user = await this.TryFindUserByEmailAsync(message, cancellationToken);
            if (user is null)
            {
                logger.Error(
                    "Message ignored, user isn't found by email. From: \"{0}\", Subject: \"{1}\".",
                    message.From,
                    message.Subject);

                return;
            }

            LocalizationManager.CurrentUICulture = user.UICulture;

            var culture = await this.FormattingSettingsCache.GetCultureInfoAsync(user.Culture, cancellationToken);
            LocalizationManager.CurrentCulture = culture;
            CultureInfo.CurrentCulture = culture;

            this.SetSessionToken(user);

            var messageHandlers = this.GetMessageHandlers();
            IMessageInfo info = null;
            IMessageHandler selectedHandler = null;

            try
            {
                foreach (IMessageHandler handler in messageHandlers)
                {
                    info = await handler.TryParseAsync(message, cancellationToken);
                    if (info is not null)
                    {
                        selectedHandler = handler;
                        break;
                    }
                }

                if (info is null)
                {
                    logger.Error(
                        "Can't find parser for the message. From: \"{0}\", Subject: \"{1}\".",
                        message.From,
                        message.Subject);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                info = null;
                logger.LogException($"Message parsing has failed. From: \"{message.From}\", Subject: \"{message.Subject}\".", ex);
            }

            if (info is null)
            {
                return;
            }

            try
            {
                Guid? taskRowID = info.TaskRowID;
                if (taskRowID.HasValue)
                {
                    logger.Trace(
                        "Completing task. From: \"{0}\", Subject: \"{1}\", TaskID={2:B}, Handler: \"{3}\".",
                        message.From,
                        message.Subject,
                        taskRowID.Value,
                        selectedHandler.GetType().FullName);

                    await this.CompleteTaskAsync(taskRowID.Value, info, message, selectedHandler, cancellationToken);
                }
                else
                {
                    logger.Trace(
                        "Processing message. From: \"{0}\", Subject: \"{1}\", Handler: \"{2}\".",
                        message.From,
                        message.Subject,
                        selectedHandler.GetType().FullName);

                    var context = new MessageHandlerContext(
                        info,
                        message,
                        this.Session,
                        this.DbScope.Db,
                        this.DbScope.BuilderFactory,
                        cancellationToken: cancellationToken);

                    await selectedHandler.HandleAsync(context);
                }

                logger.Trace(
                    "Message is processed. From: \"{0}\", Subject: \"{1}\", Handler: \"{2}\".",
                    message.From,
                    message.Subject,
                    selectedHandler.GetType().FullName);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogException($"Message handling has failed. From: \"{message.From}\", Subject: \"{message.Subject}\".", ex);
            }
        }

        #endregion
    }
}
