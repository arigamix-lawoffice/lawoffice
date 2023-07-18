using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared;
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

using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Обработчик действия <see cref="KrDescriptors.KrUniversalTaskDescriptor"/>.
    /// </summary>
    public sealed class KrUniversalTaskAction : KrWorkflowTaskActionBase
    {
        #region Fields

        /// <summary>
        /// Имя ключа, по которому в параметрах действия содержится идентификатор обрабатываемого задания данным экземпляром действия. Тип значения: <see cref="Guid"/>.
        /// </summary>
        private const string taskParamKey = CardHelper.SystemKeyPrefix + "TaskID";

        private const string mainSectionName = KrUniversalTaskActionVirtual.SectionName;

        /// <summary>
        /// Идентификатор типа задания согласования.
        /// </summary>
        private static readonly Guid taskTypeID = DefaultTaskTypes.KrUniversalTaskTypeID;

        /// <summary>
        /// Значение по умолчанию для параметра "Длительность, рабочие дни". Данное значение используется только, если в схеме не указано значение по умолчанию для поля <see cref="KrUniversalTaskActionVirtual.Period"/>.
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

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrUniversalTaskAction"/>.
        /// </summary>
        public KrUniversalTaskAction(
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
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IWorkflowBindingParser bindingParser,
            IWorkflowBindingExecutor bindingExecutor,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  descriptor: KrDescriptors.KrUniversalTaskDescriptor,
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
            actionState.Hash.Remove(KrUniversalTaskActionButtonsVirtual.SectionName);
        }

        /// <summary>
        /// Схема выполнения действия задания:<para/>
        /// 1. Получаем ID привязанного к данному действию заданию<para/>
        /// 2. Если есть задание, то очищаем список переходов<para/>
        /// 3. Если тип сигнала - default<para/>
        /// 3.1. Если задания нет, создаем задание, создаем все необходимые подписки<para/>
        /// 3.2. Если задание есть, игнорируем создание задания<para/>
        /// 3.3. В любом случае очищаем список переходов<para/>
        /// 4. Если тип сигнала из списка обрабатываемых типов сигналов<para/>
        /// 4.1. Если задания нет, игнорируем<para/>
        /// 4.2. Если задание есть, обрабатываем сигнал<para/>
        /// 4.3. Если по окончанию обработки задания оно все еще есть, ставим KeepAlive = true.
        /// </summary>
        /// <param name="context"></param>
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
                    await context.GetAsync<string>(mainSectionName, KrUniversalTaskActionVirtual.Result),
                    task);
        }

        /// <inheritdoc/>
        protected override async Task CompleteTaskCoreAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid optionID,
            IWorkflowEngineCompiled scriptObject)
        {
            var resultOptionID = await this.CompleteTaskPredefinedActionProcessingAsync(context, task, optionID);

            // Вариант завершения был обработан?
            if (resultOptionID.HasValue)
            {
                optionID = resultOptionID.Value;

                var optionRows = await context.GetAllRowsAsync(context.ActionTemplate.Hash, KrUniversalTaskActionButtonsVirtual.SectionName);

                if (optionRows?.Any() == true)
                {
                    var processRows = optionRows
                        .Where(x => WorkflowEngineHelper.Get<Guid>(x, KrUniversalTaskActionButtonsVirtual.OptionID) == optionID);

                    var linksForPerforming = new HashSet<Guid>();

                    foreach (var processRow in processRows)
                    {
                        var info = new WorkflowTaskNotificationInfo(
                            context,
                            processRow,
                            KrUniversalTaskActionNotificationRolesVitrual.Button,
                            KrUniversalTaskActionNotificationRolesVitrual.SectionName);

                        if (scriptObject != null)
                        {
                            await scriptObject.ExecuteActionAsync(
                                KrWorkflowActionMethods.KrUniversalTaskOptionMethod.GetMethodName(processRow),
                                KrWorkflowActionMethods.KrUniversalTaskOptionMethod,
                                task,
                                task.Card.DynamicEntries, task.Card.DynamicTables,
                                info);
                        }

                        await this.SendCompleteTaskNotificationAsync(
                            context,
                            scriptObject,
                            task,
                            info);

                        if (!context.Cancel)
                        {
                            var linkRows = context.ActionTemplate.Hash.TryGet<IList>(KrUniversalTaskActionButtonLinksVirtual.SectionName);
                            var rowID = WorkflowEngineHelper.Get<Guid>(processRow, Names.Table_RowID);
                            var linkIDs =
                                linkRows?
                                    .Cast<Dictionary<string, object>>()
                                    .Where(x => WorkflowEngineHelper.Get<Guid?>(
                                        x, KrUniversalTaskActionButtonLinksVirtual.Button, Names.Table_RowID) == rowID)
                                    .Select(x => WorkflowEngineHelper.Get<Guid>(
                                        x, KrUniversalTaskActionButtonLinksVirtual.Link, Names.Table_ID))
                                ?? EmptyHolder<Guid>.Collection;

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
            }

            if (task.State == CardRowState.Deleted)
            {
                WorkflowEngineHelper.Set<object>(context.ActionInstance.Hash, null, taskParamKey);
            }
        }

        /// <inheritdoc/>
        protected override bool CheckActive(IWorkflowEngineContext context)
        {
            return context.ActionInstance.Hash.TryGet<Guid?>(taskParamKey).HasValue;
        }

        /// <inheritdoc/>
        public override ValueTask<ValidationResult> ValidateAsync(
            WorkflowActionStorage action,
            WorkflowNodeStorage node,
            WorkflowProcessStorage process,
            CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResultBuilder();
            if (WorkflowEngineHelper.Get<object>(action.Hash, mainSectionName, KrUniversalTaskActionVirtual.Role, Names.Table_ID) is null)
            {
                validationResult.AddWarning(
                    this,
                    WorkflowEngineHelper.GetValidateFieldMessage(action, node, "$CardTypes_Controls_Role"));
            }

            var list = WorkflowEngineHelper.Get<IList>(action.Hash, KrUniversalTaskActionButtonsVirtual.SectionName);
            if (list is null
                || list.Count == 0)
            {
                validationResult.AddWarning(
                    this,
                    WorkflowEngineHelper.GetValidateFieldMessage(
                        action,
                        node,
                        "$CardTypes_Controls_CompletionOptions",
                        template: "$WorkflowEngine_Actions_TableEmptyTemplate"));
            }

            return ValueTask.FromResult(validationResult.Build());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Обрабатывает завершение заданий в соответствии с предопределённой логикой обработки.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="optionID">Идентификатор варианта завершения с которым завершается задание. Может отличаться от <see cref="CardTask.OptionID"/> из <paramref name="task"/>, если он был изменён в скрипте, обрабатывающим завершение задания.</param>
        /// <returns>Идентификатор варианта завершения соответствующий выбранному настраиваемому варианту завершения или значение по умолчанию для типа, если задание не было завершено или произошла ошибка при обработке.</returns>
        private async Task<Guid?> CompleteTaskPredefinedActionProcessingAsync(
            IWorkflowEngineContext context,
            CardTask task,
            Guid? optionID)
        {
            if (task.TypeID == taskTypeID)
            {
                if (optionID == DefaultCompletionOptions.Approve)
                {
                    optionID = task.Info.TryGet<Guid?>(KrUniversalTaskStoreExtension.OptionIDKey);
                }

                var taskSections = task.Card.Sections;
                var completionResult = taskSections.GetOrAdd(KrTask.Name).Fields.TryGet<string>(KrTask.Comment);

                if (!string.IsNullOrWhiteSpace(completionResult))
                {
                    if (this.bindingParser.IsBinding(completionResult))
                    {
                        completionResult = await this.bindingExecutor.GetAsync<string>(context, completionResult);
                    }

                    task.Result = await this.GetWithPlaceholdersAsync(
                        context,
                        completionResult,
                        task);
                }

                return optionID;
            }

            return default;
        }

        /// <summary>
        /// Асинхронно отправляет задание.
        /// </summary>
        /// <param name="context">Контекст обработчика процесса.</param>
        /// <param name="scriptObject">Объект, предоставляющий доступ к скриптам действия.</param>
        /// <returns>Асинхронная задача.</returns>
        private async Task SendTaskAsync(IWorkflowEngineContext context, IWorkflowEngineCompiled scriptObject)
        {
            var buttonRows = await context.GetAllRowsAsync(context.ActionTemplate.Hash, KrUniversalTaskActionButtonsVirtual.SectionName);
            var roleID = await context.GetAsync<Guid?>(mainSectionName, KrUniversalTaskActionVirtual.Role, Names.Table_ID) ?? Guid.Empty;

            if (buttonRows?.Any() != true)
            {
                context.ValidationResult.AddError(
                    this,
                    WorkflowEngineHelper.GetValidateFieldMessage(
                        context.ActionTemplate,
                        context.NodeTemplate,
                        "$CardTypes_Controls_CompletionOptions",
                        template: "$WorkflowEngine_Actions_TableEmptyTemplate"));
            }

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            var digest = await context.GetAsync<string>(mainSectionName, KrUniversalTaskActionVirtual.Digest);

            var periodInDays = (double?) (await context.GetAsync<object>(mainSectionName, KrUniversalTaskActionVirtual.Period)
                ?? (await context.CardMetadata.GetSectionsAsync(context.CancellationToken))[mainSectionName]
                    .Columns[KrUniversalTaskActionVirtual.Period]
                    .DefaultValue)
                ?? periodInDaysDefaultValue;
            var planned = await context.GetAsync<DateTime?>(mainSectionName, KrUniversalTaskActionVirtual.Planned);

            var parentTaskRowID = context.Signal.As<WorkflowEngineTaskSignal>().ParentTaskRowID;
            var authorID = await context.GetAsync<Guid?>(mainSectionName, KrUniversalTaskActionVirtual.Author, Names.Table_ID);
            var roleName = await context.GetAsync<string>(mainSectionName, KrUniversalTaskActionVirtual.Role, Table_Field_Name);

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

            var kindID = await context.GetAsync<Guid?>(mainSectionName, KrUniversalTaskActionVirtual.Kind, Names.Table_ID);
            var kindCaption = await context.GetAsync<string>(mainSectionName, KrUniversalTaskActionVirtual.Kind, Table_Field_Caption);

            context.ValidationResult.Add(WorkflowHelper.SetTaskKind(cardTask, kindID, kindCaption, this));

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            if (await context.GetAsync<bool?>(mainSectionName, KrUniversalTaskActionVirtual.CanEditCard) == true)
            {
                cardTask.Settings[NamesKeys.CanEditCard] = BooleanBoxes.True;
            }

            if (await context.GetAsync<bool?>(mainSectionName, KrUniversalTaskActionVirtual.CanEditAnyFiles) == true)
            {
                cardTask.Settings[NamesKeys.CanEditAnyFiles] = BooleanBoxes.True;
            }

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

            var optionsSection = cardTask.Card.Sections.GetOrAddTable(KrUniversalTaskOptions.Name);

            foreach (var buttonRow in buttonRows)
            {
                var newRow = optionsSection.Rows.Add();
                newRow.RowID = Guid.NewGuid();
                newRow[KrUniversalTaskOptions.OptionID] = WorkflowEngineHelper.Get<object>(buttonRow, KrUniversalTaskActionButtonsVirtual.OptionID);
                newRow[KrUniversalTaskOptions.Caption] = WorkflowEngineHelper.Get<object>(buttonRow, KrUniversalTaskActionButtonsVirtual.Caption);
                newRow[KrUniversalTaskOptions.ShowComment] = BooleanBoxes.Box(WorkflowEngineHelper.Get<bool?>(buttonRow, KrUniversalTaskActionButtonsVirtual.IsShowComment) ?? default);
                newRow[KrUniversalTaskOptions.Additional] = BooleanBoxes.Box(WorkflowEngineHelper.Get<bool?>(buttonRow, KrUniversalTaskActionButtonsVirtual.IsAdditionalOption) ?? default);
                newRow[KrUniversalTaskOptions.Order] = Int32Boxes.Box(WorkflowEngineHelper.Get<int?>(buttonRow, KrUniversalTaskActionButtonsVirtual.Order) ?? default);
                newRow[KrUniversalTaskOptions.Message] = WorkflowEngineHelper.Get<string>(buttonRow, KrUniversalTaskActionButtonsVirtual.Digest);
                newRow.State = CardRowState.Inserted;
            }

            this.AddTaskToNextContextTasks(context, cardTask);
            context.ActionInstance.Hash[taskParamKey] = cardTask.RowID;
            await context.AddActiveTaskAsync(cardTask.RowID);
            await context.AddToHistoryAsync(cardTask.RowID, WorkflowHelper.GetProcessCycle(context.ProcessInstance.Hash));

            if (scriptObject != null)
            {
                await scriptObject.ExecuteActionAsync(
                    KrWorkflowActionMethods.KrUniversalTaskInitMethod.MethodName,
                    KrWorkflowActionMethods.KrUniversalTaskInitMethod,
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
                methodDescriptor: KrWorkflowActionMethods.KrUniversalTaskStartNotificationMethod);
        }

        #endregion
    }
}
