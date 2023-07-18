#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Actions.Descriptors;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Helpful;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Базовый класс обработчиков действий для типового решения.
    /// </summary>
    public abstract class KrWorkflowActionBase : WorkflowActionBase
    {
        #region Fields And Constants

        /// <summary>
        /// Имя ключа, по которому в параметрах текущего действия содержится значение флага управляющего принудительным увеличением версии основной карточки, даже если других изменений в карточке не было. Если не найден в параметрах действия, то считается равным <see langword="true"/>. Тип значения: <see cref="bool"/>.
        /// </summary>
        protected const string AffectMainCardVersionWhenStateChangedKey = "AffectMainCardVersionWhenStateChanged";

        /// <summary>
        /// Имя ключа, по которому в параметрах текущего процесса содержится идентификатор предыдущего состояния карточки. Тип значения: <see cref="int"/>.
        /// </summary>
        protected const string PreviousStateKey = "KrPreviousState";

        protected ICardRepository CardRepository { get; }
        protected IWorkflowEngineCardRequestExtender RequestExtender { get; }
        protected IBusinessCalendarService CalendarService { get; }
        protected IKrDocumentStateManager KrDocumentStateManager { get; }

        #endregion

        #region Constructors

        protected KrWorkflowActionBase(
            WorkflowActionDescriptor actionDescriptor,
            ICardRepository cardRepository,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IKrDocumentStateManager krDocumentStateManager)
            : base(actionDescriptor)
        {
            this.CardRepository = cardRepository;
            this.RequestExtender = requestExtender;
            this.CalendarService = calendarService;
            this.KrDocumentStateManager = krDocumentStateManager;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override Task ExecuteAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject)
        {
            context.CardsScope.CardStorePriorityComparer = WorkflowConstants.KrCardStorePriorityComparerDefault;

            return Task.CompletedTask;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Устанавливает состояние карточки.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="state">Устанавливаемое состояние.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>Предыдущее состояние сохраняется в параметрах процесса (см. <see cref="StorePreviousState(IWorkflowEngineContext, int)"/>).</remarks>
        protected async Task SetStateIDAsync(
            IWorkflowEngineContext context,
            KrState state,
            CancellationToken cancellationToken = default)
        {
            var mainCard = await context.GetMainCardAsync(cancellationToken);

            if (mainCard is null)
            {
                return;
            }

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return;
            }

            var (_, hasMainSatelliteChanges, oldState) = await this.KrDocumentStateManager.SetStateAsync(
                mainCard,
                sCard,
                state,
                cancellationToken);

            if (hasMainSatelliteChanges)
            {
                this.StorePreviousState(context, oldState.HasValue ? oldState.Value.ID : KrState.Draft.ID);

                var approvalCommonInfoFields = sCard.GetApprovalInfoSection().Fields;
                approvalCommonInfoFields[KrApprovalCommonInfo.StateChangedDateTimeUTC] = DateTime.UtcNow;

                if (await context.GetAsync<bool?>(AffectMainCardVersionWhenStateChangedKey) ?? true)
                {
                    context.ModifyStoreRequest(static request => request.AffectVersion = true);
                }
            }
        }

        protected async Task AddTaskHistoryByTaskAsync(
            IWorkflowEngineContext context,
            Guid taskTypeID,
            Guid optionID,
            string result,
            Action<CardTaskHistoryItem>? modifyAction = default)
        {
            if (!(await context.CardMetadata.GetCardTypesAsync(context.CancellationToken)).TryGetValue(taskTypeID, out var taskType))
            {
                return;
            }

            await this.AddTaskHistoryAsync(
                context,
                taskTypeID,
                taskType.Name,
                taskType.Caption,
                optionID,
                result,
                modifyAction);
        }

        protected async Task AddTaskHistoryAsync(
            IWorkflowEngineContext context,
            Guid taskTypeID,
            string? taskTypeName,
            string? taskTypeCaption,
            Guid optionID,
            string result,
            Action<CardTaskHistoryItem>? modifyAction = default)
        {
            var userID = context.Session.User.ID;
            var userName = context.Session.User.Name;

            var option = (await context.CardMetadata.GetEnumerationsAsync(context.CancellationToken)).CompletionOptions[optionID];
            var groupID = context.ProcessInstance.GetHistoryGroup();
            // Временная зона текущего сотрудника и календарь, для записи в историю заданий
            var userZoneInfo = await this.CalendarService.GetRoleTimeZoneInfoAsync(userID, context.CancellationToken);
            var userCalendarInfo = await this.CalendarService.GetRoleCalendarInfoAsync(userID, context.CancellationToken);
            if (userCalendarInfo is null)
            {
                context.ValidationResult.AddError(this, await LocalizationManager.FormatAsync("$KrMessages_NoRoleCalendar", userID, context.CancellationToken));
                return;
            }
            var settings = new Dictionary<string, object?>(StringComparer.Ordinal)
            {
                [TaskHistorySettingsKeys.PerformerID] = userID,
                [TaskHistorySettingsKeys.PerformerName] = userName
            };

            var newItem = new CardTaskHistoryItem
            {
                State = CardTaskHistoryState.Inserted,
                RowID = Guid.NewGuid(),
                TypeID = taskTypeID,
                TypeName = taskTypeName,
                TypeCaption = taskTypeCaption,
                Created = context.StoreDateTime,
                Planned = context.StoreDateTime,
                InProgress = context.StoreDateTime,
                Completed = context.StoreDateTime,
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

            modifyAction?.Invoke(newItem);

            var mainCard = await context.GetMainCardAsync(context.CancellationToken);
            if (mainCard is not null)
            {
                mainCard.TaskHistory.Add(newItem);
            }
        }

        /// <summary>
        /// Сохраняет идентификатор предыдущего состояния карточки в параметрах процесса.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="previousState">Идентификатор сохраняемого состояния.</param>
        /// <seealso cref="PreviousStateKey"/>
        protected void StorePreviousState(IWorkflowEngineContext context, int previousState)
        {
            context.ProcessInstance.Hash[PreviousStateKey] = Int32Boxes.Box(previousState);
        }

        /// <summary>
        /// Возвращает идентификатор предыдущего состояния карточки из параметров процесса.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <returns>Идентификатор предыдущего состояния карточки из параметров процесса. Если не найден в параметрах действия, то считается равным идентификатору состояния <see cref="KrState.Draft"/>.</returns>
        /// <seealso cref="PreviousStateKey"/>
        protected int TryGetPreviousState(IWorkflowEngineContext context)
        {
            return context.ProcessInstance.Hash.TryGet<int?>(PreviousStateKey) ?? KrState.Draft.ID;
        }

        #endregion
    }
}
