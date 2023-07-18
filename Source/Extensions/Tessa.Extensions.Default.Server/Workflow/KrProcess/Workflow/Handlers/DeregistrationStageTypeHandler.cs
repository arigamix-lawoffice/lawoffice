#nullable enable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Cards.Numbers;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Events;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.DeregistrationDescriptor"/>.
    /// </summary>
    public class DeregistrationStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DeregistrationStageTypeHandler"/>.
        /// </summary>
        /// <param name="numberDirectorContainer">Объект, выполняющий регистрацию и предоставляющий доступ к подсистеме номеров для типов карточек, включая объекты <see cref="INumberDirector"/>, <see cref="INumberComposer"/> и <see cref="INumberQueueProcessor"/>.</param>
        /// <param name="krScope">Объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="calendarService">Интерфейс API бизнес календаря.</param>
        /// <param name="cardMetadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="serializer">Объект предоставляющий методы для сериализации параметров этапов.</param>
        /// <param name="eventManager">Объект предоставляющий методы для отправки событий маршрутов документов.</param>
        public DeregistrationStageTypeHandler(
            INumberDirectorContainer numberDirectorContainer,
            IKrScope krScope,
            ISession session,
            IBusinessCalendarService calendarService,
            ICardMetadata cardMetadata,
            IKrStageSerializer serializer,
            IKrEventManager eventManager)
        {
            this.NumberDirectorContainer = numberDirectorContainer;
            this.KrScope = krScope;
            this.Session = session;
            this.CalendarService = calendarService;
            this.CardMetadata = cardMetadata;
            this.Serializer = serializer;
            this.EventManager = eventManager;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Возвращает или задаёт объект, выполняющий регистрацию и предоставляющий доступ к подсистеме номеров для типов карточек, включая объекты <see cref="INumberDirector"/>, <see cref="INumberComposer"/> и <see cref="INumberQueueProcessor"/>.
        /// </summary>
        protected INumberDirectorContainer NumberDirectorContainer { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для работы с текущим контекстом расширений типового расширения и использования разделяемых объектов карточек.
        /// </summary>
        protected IKrScope KrScope { get; set; }

        /// <summary>
        /// Возвращает или задаёт интерфейс API бизнес календаря.
        /// </summary>
        protected IBusinessCalendarService CalendarService { get; set; }

        /// <summary>
        /// Возвращает или задаёт сессию пользователя.
        /// </summary>
        protected ISession Session { get; set; }

        /// <summary>
        /// Возвращает или задаёт метаинформацию, необходимую для использования типов карточек совместно с пакетом карточек.
        /// </summary>
        protected ICardMetadata CardMetadata { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для сериализации параметров этапов.
        /// </summary>
        protected IKrStageSerializer Serializer { get; set; }

        /// <summary>
        /// Возвращает или задаёт объект предоставляющий методы для отправки событий маршрутов документов.
        /// </summary>
        protected IKrEventManager EventManager { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Создаёт элемент истории заданий информирующий о регистрации документа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Элемент истории заданий информирующий о регистрации документа или значение <see langword="null"/>, если произошла ошибка.</returns>
        protected virtual async Task<CardTaskHistoryItem?> CreateRegistrationTaskHistoryItemAsync(IStageTypeHandlerContext context)
        {
            const string result = "$ApprovalHistory_DocumentDeregistered";
            var optionID = DefaultCompletionOptions.DeregisterDocument;
            var userID = this.Session.User.ID;
            var userName = this.Session.User.Name;
            var utcNow = DateTime.UtcNow;
            var offsetNow = utcNow;

            var option = (await this.CardMetadata.GetEnumerationsAsync(context.CancellationToken)).CompletionOptions[optionID];
            var groupID = await HandlerHelper.GetTaskHistoryGroupAsync(context, this.KrScope);
            // Временная зона текущего сотрудника и календарь, для записи в историю заданий
            var userZoneInfo = await this.CalendarService.GetRoleTimeZoneInfoAsync(userID, context.CancellationToken);
            var userCalendarInfo = await this.CalendarService.GetRoleCalendarInfoAsync(userID, context.CancellationToken);
            if (userCalendarInfo is null)
            {
                context.ValidationResult.AddError(this, await LocalizationManager.FormatAsync("$KrMessages_NoRoleCalendar", userID, context.CancellationToken));
                return null;
            }
            var settings = new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                [TaskHistorySettingsKeys.PerformerID] = userID,
                [TaskHistorySettingsKeys.PerformerName] = userName
            };

            var item = new CardTaskHistoryItem
            {
                State = CardTaskHistoryState.Inserted,
                RowID = Guid.NewGuid(),
                TypeID = KrConstants.KrDeregistrationTypeID,
                TypeName = KrConstants.KrDeregistrationTypeName,
                TypeCaption = "$CardTypes_TypesNames_KrDeregistration",
                Created = offsetNow,
                Planned = offsetNow,
                InProgress = offsetNow,
                Completed = offsetNow,
                UserID = userID,
                UserName = userName,
                AuthorID = userID,
                AuthorName = userName,
                Result = result,
                OptionID = optionID,
                OptionCaption = option.Caption,
                OptionName = option.Name,
                ParentRowID = null,
                CompletedByID = userID,
                CompletedByName = userName,
                CompletedByRole = userName,
                GroupRowID = groupID,
                TimeZoneID = userZoneInfo.TimeZoneID,
                TimeZoneUtcOffsetMinutes = (int?) userZoneInfo.TimeZoneUtcOffset.TotalMinutes,
                CalendarID = userCalendarInfo.CalendarID,
                Settings = settings,
                AssignedOnRole = userName
            };

            return item;
        }

        /// <summary>
        /// Возвращает номер цикла согласования.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Номер цикла согласования.</returns>
        protected virtual int GetCycle(
            IStageTypeHandlerContext context)
        {
            if (context.RunnerMode == KrProcessRunnerMode.Async
                && context.ProcessInfo.ProcessTypeName == KrConstants.KrProcessName)
            {
                // Для основного процесса цикл лежит в его инфо.
                return context.WorkflowProcess.InfoStorage.TryGet<int?>(KrConstants.Keys.Cycle) ?? 1;
            }

            return ProcessInfoCacheHelper.Get(this.Serializer, context.ContextualSatellite)?.TryGet<int?>(KrConstants.Keys.Cycle)
                ?? 0;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            if (context.MainCardID.HasValue
                && context.MainCardType is not null)
            {
                var mainCard = await this.KrScope.GetMainCardAsync(
                    context.MainCardID.Value,
                    cancellationToken: context.CancellationToken);

                if (mainCard is null)
                {
                    return StageHandlerResult.EmptyResult;
                }

                // выделение номера при регистрации
                var numberProvider = this.NumberDirectorContainer.GetProvider(context.MainCardType.ID);
                var numberDirector = numberProvider.GetDirector();
                var numberComposer = numberProvider.GetComposer();
                var numberContext = await numberDirector.CreateContextAsync(
                    numberComposer,
                    mainCard,
                    context.MainCardType,
                    context.CardExtensionContext is ICardStoreExtensionContext storeContext
                        ? storeContext.Request.Info
                        : null,
                    context.CardExtensionContext,
                    transactionMode: NumberTransactionMode.SeparateTransaction,
                    context.CancellationToken);

                await numberDirector.NotifyOnDeregisteringCardAsync(numberContext, context.CancellationToken);
                context.ValidationResult.Add(numberContext.ValidationResult);

                var info = ProcessInfoCacheHelper.Get(this.Serializer, context.ContextualSatellite);
                var previousState = info?.TryGet<int?>(KrConstants.Keys.StateBeforeRegistration) ?? 0;
                info?.Remove(KrConstants.Keys.StateBeforeRegistration);

                context.WorkflowProcess.State = previousState == KrState.Approved.ID || previousState == KrState.Signed.ID
                    ? (KrState) previousState
                    : KrState.Draft;

                var cycle = this.GetCycle(context);
                var fakeHistoryRecord = await this.CreateRegistrationTaskHistoryItemAsync(context);

                if (fakeHistoryRecord is null)
                {
                    return StageHandlerResult.EmptyResult;
                }

                mainCard.TaskHistory.Add(fakeHistoryRecord);
                context.ContextualSatellite.AddToHistory(fakeHistoryRecord.RowID, cycle);

                await this.EventManager.RaiseAsync(DefaultEventTypes.DeregistrationEvent, context, cancellationToken: context.CancellationToken);
            }

            return StageHandlerResult.CompleteResult;
        }

        #endregion
    }
}
