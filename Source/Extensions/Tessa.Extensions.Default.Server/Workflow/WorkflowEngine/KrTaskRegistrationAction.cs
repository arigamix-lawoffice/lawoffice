using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Numbers;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Tessa.Scheme;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Bindings;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Signals;
using Tessa.Workflow.Storage;

using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Обработчик действия <see cref="KrDescriptors.KrTaskRegistrationDescriptor"/>.
    /// </summary>
    public sealed class KrTaskRegistrationAction : KrWorkflowTaskActionBase
    {
        #region Fields

        /// <summary>
        /// Имя ключа, по которому в параметрах действия содержится идентификатор обрабатываемого задания данным экземпляром действия. Тип значения: <see cref="Guid"/>.
        /// </summary>
        private const string taskParamKey = CardHelper.SystemKeyPrefix + "TaskID";

        private const string mainSectionName = KrTaskRegistrationActionVirtual.SectionName;

        private static readonly Guid taskTypeID = DefaultTaskTypes.KrRegistrationTypeID;

        /// <summary>
        /// Значение по умолчанию для параметра "Длительность, рабочие дни". Данное значение используется только, если в схеме не указано значение по умолчанию для поля <see cref="KrTaskRegistrationActionVirtual.Period"/>.
        /// </summary>
        private const double periodInDaysDefaultValue = 1d;

        /// <summary>
        /// Массив типов обрабатываемых сигналов.
        /// </summary>
        private static readonly string[] signalTypes = new string[]
        {
            WorkflowSignalTypes.CompleteTask,
            WorkflowSignalTypes.DeleteTask,
            WorkflowSignalTypes.UpdateTask,
        };

        private readonly IRoleGetStrategy roleGetStrategy;
        private readonly IContextRoleManager contextRoleManager;
        private readonly IWorkflowBindingParser bindingParser;
        private readonly IWorkflowBindingExecutor bindingExecutor;
        private readonly INumberDirectorContainer numberDirectorContainer;
        private readonly IKrStageSerializer krStageSerializer;

        #endregion

        #region Constructors

        public KrTaskRegistrationAction(
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            [Dependency(NotificationManagerNames.DeferredWithoutTransaction)] INotificationManager notificationManager,
            IWorkflowEngineCardsScope cardsScope,
            ICardRepository cardRepository,
            [Dependency(CardRepositoryNames.Default)] ICardRepository cardRepositoryDef,
            ICardServerPermissionsProvider serverPermissionsProvider,
            ISignatureProvider signatureProvider,
            Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory,
            ICardFileManager cardFileManager,
            IWorkflowBindingParser bindingParser,
            IWorkflowBindingExecutor bindingExecutor,
            INumberDirectorContainer numberDirectorContainer,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IKrStageSerializer krStageSerializer,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  descriptor: KrDescriptors.KrTaskRegistrationDescriptor,
                  notificationManager: notificationManager,
                  cardsScope: cardsScope,
                  cardRepository: cardRepository,
                  cardRepositoryDef: cardRepositoryDef,
                  serverPermissionsProvider: serverPermissionsProvider,
                  signatureProvider: signatureProvider,
                  ctcBuilderFactory: ctcBuilderFactory,
                  cardFileManager: cardFileManager,
                  requestExtender: requestExtender,
                  calendarService: calendarService,
                  krDocumentStateManager: krDocumentStateManager)
        {
            this.roleGetStrategy = roleGetStrategy;
            this.contextRoleManager = contextRoleManager;
            this.bindingParser = bindingParser;
            this.bindingExecutor = bindingExecutor;
            this.numberDirectorContainer = numberDirectorContainer;
            this.krStageSerializer = krStageSerializer;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override bool Compile(
            IWorkflowCompilationSyntaxTreeBuilder builder,
            WorkflowActionStorage action)
        {
            if (base.Compile(builder, action))
            {
                this.CompileEvents(builder, action);
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override void PrepareForExecute(WorkflowActionStateStorage actionState, IWorkflowEngineContext context)
        {
            base.PrepareForExecute(actionState, context);
            actionState.Hash.Remove(KrTaskRegistrationActionOptionsVirtual.SectionName);
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Схема выполнения действия:<para/>
        /// 1. Получаем ID привязанного к данному действию задания;<para/>
        /// 2. Если есть задание, то очищаем список переходов;<para/>
        /// 3. Если тип сигнала - default;<para/>
        /// 3.1. Если задания нет, создаем задание, создаем все необходимые подписки;<para/>
        /// 3.2. Если задание есть, игнорируем создание задания;<para/>
        /// 3.3. В любом случае очищаем список переходов;<para/>
        /// 4. Если тип сигнала из списка обрабатываемых типов сигналов;<para/>
        /// 4.1. Если задания нет, игнорируем;<para/>
        /// 4.2. Если задание есть, обрабатываем сигнал.<para/>
        /// </remarks>
        protected override async Task ExecuteAsync(IWorkflowEngineContext context, IWorkflowEngineCompiled scriptObject)
        {
            await base.ExecuteAsync(context, scriptObject);

            var currentTaskID = context.ActionInstance.Hash.TryGet<Guid?>(taskParamKey);
            var signalType = context.Signal.Type;

            if (currentTaskID.HasValue)
            {
                context.Links.Clear();
            }

            using (this.CreateTasksContext(context))
            {
                if (signalType == WorkflowSignalTypes.Default)
                {
                    if (!currentTaskID.HasValue)
                    {
                        await this.SetStateIDAsync(
                            context,
                            KrState.Registration,
                            cancellationToken: context.CancellationToken);

                        await this.SendTaskAsync(context, scriptObject);

                        foreach (var newSignalType in signalTypes)
                        {
                            this.CreateSubscription(context, newSignalType);
                        }

                        this.SubscribeOnEvents(context);
                    }
                    context.Links.Clear();
                }
                else if (currentTaskID.HasValue)
                {
                    switch (signalType)
                    {
                        case WorkflowSignalTypes.CompleteTask:
                            await this.CompleteTaskAsync(context, scriptObject, currentTaskID.Value);
                            break;

                        case WorkflowSignalTypes.DeleteTask:
                            if (await this.DeleteTaskAsync(context, currentTaskID.Value))
                            {
                                WorkflowEngineHelper.Set<object>(context.ActionInstance.Hash, null, taskParamKey);
                            }
                            break;

                        case WorkflowSignalTypes.UpdateTask:
                            await this.UpdateTaskAsync(context, currentTaskID.Value);
                            break;

                        default:
                            await this.PerformEvent(context, scriptObject, currentTaskID.Value);
                            break;
                    }
                }
            }
        }

        /// <inheritdoc/>
        protected override bool CheckActive(IWorkflowEngineContext context)
        {
            return context.ActionInstance.Hash.TryGet<Guid?>(taskParamKey).HasValue;
        }

        #endregion

        #region WorkflowTaskActionBase Overrides

        /// <inheritdoc/>
        protected override async Task<string> GetResultAsync(
            IWorkflowEngineContext context,
            CardTask task)
        {
            return
                await this.GetWithPlaceholdersAsync(
                    context,
                    await context.GetAsync<string>(mainSectionName, KrTaskRegistrationActionVirtual.Result),
                    task);
        }

        /// <inheritdoc/>
        protected override async Task CompleteTaskCoreAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid optionID,
            IWorkflowEngineCompiled scriptObject)
        {
            if (optionID == DefaultCompletionOptions.RegisterDocument)
            {
                await this.RegisterAsync(context);
            }

            var optionRows = await context.GetAllRowsAsync(context.ActionTemplate.Hash, KrTaskRegistrationActionOptionsVirtual.SectionName);

            if (optionRows?.Any() == true)
            {
                var processRows = optionRows
                    .Where(x => WorkflowEngineHelper.Get<Guid?>(x, ActionOptionsBase.Option, Names.Table_ID) == optionID);

                var linksForPerforming = new HashSet<Guid>();

                foreach (var row in processRows)
                {
                    var completionResult = row.TryGet<string>(KrTaskRegistrationActionOptionsVirtual.Result);
                    var info = new WorkflowTaskNotificationInfo(
                        context,
                        row,
                        KrTaskRegistrationActionNotificationRolesVitrual.Option,
                        KrTaskRegistrationActionNotificationRolesVitrual.SectionName);

                    if (!string.IsNullOrWhiteSpace(completionResult))
                    {
                        if (bindingParser.IsBinding(completionResult))
                        {
                            completionResult = await bindingExecutor.GetAsync<string>(context, completionResult);
                        }

                        task.Result = await this.GetWithPlaceholdersAsync(
                            context,
                            completionResult,
                            task);
                    }

                    var rowID = WorkflowEngineHelper.Get<Guid>(row, Names.Table_RowID);
                    if (scriptObject != null)
                    {
                        await scriptObject.ExecuteActionAsync(
                            KrWorkflowActionMethods.KrTaskRegistrationTaskOptionMethod.GetMethodName(row),
                            KrWorkflowActionMethods.KrTaskRegistrationTaskOptionMethod,
                            task,
                            task.Card.DynamicEntries, task.Card.DynamicTables,
                            info);
                    }

                    var linkRows = context.ActionTemplate.Hash.TryGet<IList>(KrTaskRegistrationActionOptionLinksVirtual.SectionName);
                    var linkIDs =
                        linkRows?
                            .Cast<Dictionary<string, object>>()
                            .Where(x => WorkflowEngineHelper.Get<Guid?>(
                                x, ActionOptionLinksBase.Option, Names.Table_RowID) == rowID)
                            .Select(x => WorkflowEngineHelper.Get<Guid>(
                                x, ActionOptionLinksBase.Link, Names.Table_ID))
                        ?? EmptyHolder<Guid>.Collection;

                    await SendCompleteTaskNotificationAsync(
                        context,
                        scriptObject,
                        task,
                        info);

                    if (!context.Cancel)
                    {
                        foreach (var linkID in linkIDs)
                        {
                            linksForPerforming.Add(linkID);
                        }
                    }
                    context.Cancel = false;
                }

                foreach (var linkID in linksForPerforming)
                {
                    context.Links[linkID] = WorkflowEngineSignal.CreateDefaultSignal(StorageHelper.Clone(context.Signal.Hash));
                }
            }

            if (task.State == CardRowState.Deleted)
            {
                WorkflowEngineHelper.Set<object>(context.ActionInstance.Hash, null, taskParamKey);
            }
        }

        /// <inheritdoc/>
        public override ValueTask<ValidationResult> ValidateAsync(
            WorkflowActionStorage action,
            WorkflowNodeStorage node,
            WorkflowProcessStorage process,
            CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResultBuilder();
            if (WorkflowEngineHelper.Get<object>(action.Hash, mainSectionName, KrTaskRegistrationActionVirtual.Performer, Names.Table_ID) is null)
            {
                validationResult.AddWarning(
                    this,
                    WorkflowEngineHelper.GetValidateFieldMessage(action, node, "$CardTypes_Controls_Role"));
            }

            return ValueTask.FromResult(validationResult.Build());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Асинхронно отправляет задание.
        /// </summary>
        /// <param name="context">Контекст обработчика.</param>
        /// <param name="scriptObject">Контекст скриптов.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SendTaskAsync(IWorkflowEngineContext context, IWorkflowEngineCompiled scriptObject)
        {
            var roleID = await context.GetAsync<Guid?>(
                mainSectionName,
                KrTaskRegistrationActionVirtual.Performer,
                Names.Table_ID) ?? Guid.Empty;

            var roleName = await context.GetAsync<string>(
                mainSectionName,
                KrTaskRegistrationActionVirtual.Performer,
                Table_Field_Name);

            var digest = await context.GetAsync<string>(mainSectionName,
                KrTaskRegistrationActionVirtual.Digest);

            var periodInDays = (double?)(await context.GetAsync<object>(mainSectionName, KrTaskRegistrationActionVirtual.Period)
                ?? (await context.CardMetadata.GetSectionsAsync(context.CancellationToken))[mainSectionName]
                    .Columns[KrTaskRegistrationActionVirtual.Period]
                    .DefaultValue)
                ?? periodInDaysDefaultValue;
            var planned = await context.GetAsync<DateTime?>(mainSectionName, KrTaskRegistrationActionVirtual.Planned);

            var parentTaskRowID = context.Signal.As<WorkflowEngineTaskSignal>().ParentTaskRowID;
            var authorID = await context.GetAsync<Guid?>(mainSectionName, KrTaskRegistrationActionVirtual.Author, Names.Table_ID);

            var kindID = await context.GetAsync<Guid?>(mainSectionName, KrTaskRegistrationActionVirtual.Kind, Names.Table_ID);
            var kindCaption = await context.GetAsync<string>(mainSectionName, KrTaskRegistrationActionVirtual.Kind, Table_Field_Caption);

            digest = await this.GetWithPlaceholdersAsync(
                context,
                digest,
                context.Task);

            var cardTask =
                await context.SendTaskAsync(
                    taskTypeID,
                    digest,
                    planned,
                    null,
                    periodInDays,
                    roleID,
                    roleName,
                    parentRowID: parentTaskRowID,
                    cancellationToken: context.CancellationToken);

            if (cardTask is null
                || !context.ValidationResult.IsSuccessful())
            {
                return;
            }

            context.ValidationResult.Add(WorkflowHelper.SetTaskKind(cardTask, kindID, kindCaption, this));

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            if (await context.GetAsync<bool?>(mainSectionName, KrTaskRegistrationActionVirtual.CanEditCard) == true)
            {
                cardTask.Settings[NamesKeys.CanEditCard] = BooleanBoxes.True;
            }

            if (await context.GetAsync<bool?>(mainSectionName, KrTaskRegistrationActionVirtual.CanEditAnyFiles) == true)
            {
                cardTask.Settings[NamesKeys.CanEditAnyFiles] = BooleanBoxes.True;
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            this.AddTaskToNextContextTasks(context, cardTask);

            authorID = await context.GetAuthorIDAsync(roleGetStrategy, this.contextRoleManager, authorID);
            if (!authorID.HasValue)
            {
                return;
            }

            cardTask.AddRole(
                authorID.Value,
                null,
                CardFunctionRoles.AuthorID,
                showInTaskDetails: true);

            if (scriptObject != null)
            {
                await scriptObject.ExecuteActionAsync(
                    KrWorkflowActionMethods.KrTaskRegistrationTaskInitMethod.MethodName,
                    KrWorkflowActionMethods.KrTaskRegistrationTaskInitMethod,
                    cardTask,
                    cardTask.Card.DynamicEntries,
                    cardTask.Card.DynamicTables);
            }

            if (cardTask.TaskAssignedRoles.All(p => p.TaskRoleID != CardFunctionRoles.PerformerID))
            {
                context.ValidationResult.AddError(
                    this,
                    WorkflowEngineHelper.GetValidateFieldMessage(context.ActionTemplate, context.NodeTemplate, "$CardTypes_Controls_Role"));

                return;
            }

            await this.SendStartTaskNotificationAsync(
                context,
                scriptObject,
                cardTask,
                mainSectionName,
                methodDescriptor: KrWorkflowActionMethods.KrTaskRegistrationTaskStartNotificationMethod);

            context.ActionInstance.Hash[taskParamKey] = cardTask.RowID;

            await context.AddActiveTaskAsync(cardTask.RowID);
            await context.AddToHistoryAsync(cardTask.RowID, WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));
        }

        /// <summary>
        /// Регистрирует документ.
        /// </summary>
        /// <param name="context">Контекст обработчика.</param>
        /// <returns>Асинхронная операция.</returns>
        private async Task RegisterAsync(IWorkflowEngineContext context)
        {
            var mainCard = await context.GetMainCardAsync(context.CancellationToken);
            if (mainCard == null)
            {
                return;
            }

            var cardType = (await context.CardMetadata.GetCardTypesAsync(context.CancellationToken))[mainCard.TypeID];

            // Выделение номера при регистрации.
            var numberProvider = this.numberDirectorContainer.GetProvider(cardType.ID);
            var numberDirector = numberProvider.GetDirector();
            var numberComposer = numberProvider.GetComposer();
            var numberContext = await numberDirector.CreateContextAsync(
                numberComposer,
                mainCard,
                cardType,
                transactionMode: NumberTransactionMode.SeparateTransaction,
                cancellationToken: context.CancellationToken);

            await numberDirector.NotifyOnRegisteringCardAsync(numberContext, context.CancellationToken);
            context.ValidationResult.Add(numberContext.ValidationResult);

            if (context.ValidationResult.IsSuccessful())
            {
                // Состояние документа до изменения его этим действием.
                var oldStateID = this.TryGetPreviousState(context);

                var sCard = await context.GetKrSatelliteAsync();

                if (sCard is null)
                {
                    return;
                }

                var info = ProcessInfoCacheHelper.Get(this.krStageSerializer, sCard);
                info[KrConstants.Keys.StateBeforeRegistration] = Int32Boxes.Box(oldStateID);
                ProcessInfoCacheHelper.Update(this.krStageSerializer, sCard);

                await this.SetStateIDAsync(
                    context,
                    KrState.Registered,
                    cancellationToken: context.CancellationToken);

                // Сохранение состояния документа до его изменения этим действием.
                this.StorePreviousState(context, oldStateID);
            }
        }

        #endregion
    }
}
