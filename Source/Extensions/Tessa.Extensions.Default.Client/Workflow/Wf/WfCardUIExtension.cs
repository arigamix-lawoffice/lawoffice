using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Cards.Metadata;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Settings;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Licensing;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Themes;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Tasks;
using Tessa.UI.Controls;
using Tessa.UI.Files;
using Tessa.UI.Menu;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Helpful;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Processors;
using Unity;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Расширение для модификации UI карточки и заданий в соответствии с бизнес-процессами Workflow.
    /// </summary>
    public class WfCardUIExtension :
        CardUIExtension
    {
        #region Constructors

        public WfCardUIExtension(
            ILicenseManager licenseManager,
            ICardDialogManager dialogManager,
            IDialogService dialogService,
            ICardRepository extendedRepository,
            ISession session,
            IExtensionContainer extensionContainer,
            KrSettingsLazy settingsLazy,
            Func<IWfResolutionVisualizationGenerator> getGeneratorFunc,
            [Dependency("ResolutionsNodeProcessor")] Func<INodeProcessor> createNodeProcessorFunc)
        {
            licenseManager.ValidateTypeOnClient();

            this.licenseManager = licenseManager;
            this.dialogManager = dialogManager ?? throw new ArgumentNullException(nameof(dialogManager));
            this.dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            this.extendedRepository = extendedRepository ?? throw new ArgumentNullException(nameof(extendedRepository));
            this.session = session ?? throw new ArgumentNullException(nameof(session));
            this.extensionContainer = extensionContainer ?? throw new ArgumentNullException(nameof(extensionContainer));
            this.settingsLazy = settingsLazy ?? throw new ArgumentNullException(nameof(settingsLazy));
            this.getGeneratorFunc = getGeneratorFunc ?? throw new ArgumentNullException(nameof(getGeneratorFunc));
            this.createNodeProcessorFunc = createNodeProcessorFunc ?? throw new ArgumentNullException(nameof(createNodeProcessorFunc));
        }

        #endregion

        #region Fields

        private readonly ILicenseManager licenseManager;

        private readonly ICardDialogManager dialogManager;

        private readonly IDialogService dialogService;

        private readonly ICardRepository extendedRepository;

        private readonly IExtensionContainer extensionContainer;

        private readonly ISession session;

        private readonly KrSettingsLazy settingsLazy;

        private readonly Func<IWfResolutionVisualizationGenerator> getGeneratorFunc;

        private readonly Func<INodeProcessor> createNodeProcessorFunc;

        private static bool? hasLicensedWorkflowViewer;

        #endregion

        #region Constants

        public const string NavigateMainCardControl = "NavigateMainCard";

        #endregion

        #region Private Methods

        private async ValueTask<bool> HasLicensedWorkflowViewerAsync(CancellationToken cancellationToken = default)
        {
            bool? hasViewer = hasLicensedWorkflowViewer;
            if (hasViewer.HasValue)
            {
                return hasViewer.Value;
            }

            bool computedHasViewer = NodeProcessorHelper.CheckLicense(
                await this.licenseManager.GetLicenseAsync(cancellationToken),
                out _);

            hasLicensedWorkflowViewer = computedHasViewer;

            return computedHasViewer;
        }


        private static async void PostponeMetadataInitializingAsync(object sender, TaskViewModelEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                ICardMetadata targetMetadata = e.Task.PostponeMetadata;
                CardType targetType = (await targetMetadata.GetCardTypesAsync())[0];
                CardTypeForm targetForm = targetType.Forms[0];
                SealableObjectList<CardTypeBlock> targetBlocks = targetForm.Blocks;

                // удаляем блок с информацией по заданию, т.к. он будет скопирован ниже из основной формы
                if (targetBlocks.Count > 0)
                {
                    targetBlocks.RemoveAt(0);
                }

                // копируем все блоки из основной формы задания в начало формы откладывания, потом сортируем по Order
                CardType sourceType = await e.Task.TaskModel.CardType.DeepCloneAsync().ConfigureAwait(false);
                CardTypeNamedForm sourceForm = sourceType.Forms[0];
                await sourceForm.Blocks.CopyToTheBeginningOfAsync(targetBlocks).ConfigureAwait(false);

                // копируем настройки формы из основной формы задания в форму откладывания
                targetForm.FormSettings.Clear();
                StorageHelper.Merge(sourceForm.FormSettings, targetForm.FormSettings);

                // копируем метаинформацию по виртуальной таблице для формы откладывания
                if ((await e.Task.TaskModel.CardMetadata.GetSectionsAsync())
                    .TryGetValue(WfHelper.ResolutionChildrenVirtualSection, out CardMetadataSection metadataSection))
                {
                    (await targetMetadata.GetSectionsAsync()).Add(await metadataSection.DeepCloneAsync().ConfigureAwait(false));

                    CardTypeSchemeItem sourceItem = sourceType.SchemeItems
                        .FirstOrDefault(x => x.SectionID == metadataSection.ID);
                    if (sourceItem is not null)
                    {
                        targetType.SchemeItems.Add(sourceItem);
                    }
                }
            }
            catch (Exception ex)
            {
                deferral.SetException(ex);
            }
            finally
            {
                deferral.Dispose();
            }
        }


        private static void PostponeContentInitializing(object sender, TaskFormContentViewModelEventArgs e)
        {
            if (e.Task.TaskModel.Card.Sections.TryGetValue(WfHelper.ResolutionChildrenVirtualSection, out CardSection sourceSection)
                && e.Card.Sections.TryGetValue(WfHelper.ResolutionChildrenVirtualSection, out CardSection targetSection))
            {
                targetSection.Set(sourceSection);
            }
        }


        private static bool CanShowAdditionalActionsInTaskState(TaskWorkspaceState state) =>
            state switch
            {
                TaskWorkspaceState.Initial => true,
                TaskWorkspaceState.DefaultForm => true,
                TaskWorkspaceState.ReturnFromPostponed => true,
                TaskWorkspaceState.Locked => true,
                TaskWorkspaceState.LockedForPerformer => true,
                TaskWorkspaceState.UnlockedForPerformer => true,
                _ => false
            };


        private async ValueTask ModifyResolutionTaskAsync(
            WfResolutionTaskInfo taskInfo,
            ICardModel model,
            bool isTaskCard,
            bool subscribeToTaskModel,
            CancellationToken cancellationToken = default)
        {
            TaskViewModel taskViewModel = taskInfo.Control;

            if (!model.InSpecialMode())
            {
                // показываем кнопку "Визуализировать", если визуализатор присутствует в лицензии
                TaskWorkspaceState workspaceState = taskViewModel.Workspace.State;

                if (!isTaskCard
                    && CanShowAdditionalActionsInTaskState(workspaceState)
                    && await this.HasLicensedWorkflowViewerAsync(cancellationToken))
                {
                    ObservableCollection<ITaskAction> additionalActions = taskViewModel.Workspace.AdditionalActions;

                    additionalActions.Insert(
                        0, // index
                        new TaskActionViewModel(
                            "$WfResolution_VisualizeBranch", // $WfResolution_Visualize
                            async () => this.VisualizeBranch(taskViewModel),
                            model: model));

                    additionalActions.Insert(
                        1, // index
                        new TaskActionViewModel(
                            "$WfResolution_VisualizeProcess",
                            async () => this.VisualizeProcess(taskViewModel),
                            model: model));

                    if (additionalActions.Count > 2)
                    {
                        additionalActions.Insert(2, new TaskSeparatorActionViewModel());
                    }
                }

                // иконку-тег отображаем для любых состояний задания
                taskViewModel.Workspace.SetTag(
                    "Thin43",
                    "$WfTaskFiles_ShowFilesTag_ToolTip",
                    new DelegateCommand(async p => { await TaskCardNavigateActionAsync(taskViewModel.TaskModel.CardTask.RowID); }));

                // ссылку отображаем для заданий, не взятых в работу, или для формы по умолчанию
                if (isTaskCard
                    || workspaceState == TaskWorkspaceState.Locked
                    || workspaceState == TaskWorkspaceState.LockedForPerformer
                    || workspaceState == TaskWorkspaceState.UnlockedForPerformer
                    || workspaceState == TaskWorkspaceState.Initial
                    || workspaceState == TaskWorkspaceState.DefaultForm)
                {
                    string message;
                    string toolTip;

                    if (isTaskCard)
                    {
                        message = "$WfTaskFiles_ReturnFromFilesLink";
                        toolTip = "$WfTaskFiles_ReturnFromFilesLink_ToolTip";
                    }
                    else
                    {
                        int fileCount = taskViewModel.TaskModel.CardTask.Info.TryGet<int>(WfHelper.FileCountTaskKey);

                        message = fileCount > 0
                            ? string.Format(await LocalizationManager.GetStringAsync("WfTaskFiles_ShowFilesLinkTemplate", cancellationToken), fileCount)
                            : null;

                        toolTip = "$WfTaskFiles_ShowFilesTag_ToolTip";
                    }

                    if (message is not null)
                    {
                        taskViewModel.Workspace.SetLink(
                            message,
                            toolTip,
                            new DelegateCommand(async p => { await TaskCardNavigateActionAsync(taskViewModel.TaskModel.CardTask.RowID); }));
                    }
                }
            }

            IFormWithBlocksViewModel form = taskViewModel.Workspace.Form;

            // скрываем таблицу с дочерними резолюциями, если таких резолюций нет
            IBlockViewModel childResolutions;
            if (form is not null
                && !taskInfo.HasChildren
                && (childResolutions = form.Blocks.FirstOrDefault(x => x.Name == WfUIHelper.ChildResolutionsBlockName)) is not null)
            {
                childResolutions.BlockVisibility = Visibility.Collapsed;
                form.RearrangeSelf();
            }

            if (subscribeToTaskModel)
            {
                Card taskCard = taskViewModel.TaskModel.CardTask.TryGetCard();
                StringDictionaryStorage<CardSection> taskSections;
                if (taskCard is not null
                    && (taskSections = taskCard.TryGetSections()) is not null)
                {
                    if (taskSections.TryGetValue(WfHelper.ResolutionSection, out CardSection resolutionSection))
                    {
                        taskInfo.SubscribeToResolutionSectionAndUpdate(resolutionSection);
                    }

                    if (taskSections.TryGetValue(WfHelper.ResolutionPerformersSection, out CardSection performersSection))
                    {
                        taskInfo.SubscribeToPerformersAndUpdate(performersSection.Rows);
                    }
                }
            }
            else
            {
                taskInfo.Update();
            }
        }


        private static async ValueTask TaskCardNavigateActionAsync(Guid? taskRowID)
        {
            IUIContext context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;
            ICardModel model;

            if (editor is null
                || (model = editor.CardModel) is null)
            {
                return;
            }

            if (await model.HasChangesAsync())
            {
                bool? saveCard = TessaDialog.ConfirmWithCancel("$WfTaskFiles_SaveChangesConfirmation");
                if (!saveCard.HasValue)
                {
                    return;
                }

                if (saveCard.Value && !editor.OperationInProgress)
                {
                    bool success = await editor.SaveCardAsync(context, request: new CardSavingRequest(CardSavingMode.KeepPreviousCard));

                    if (success)
                    {
                        await BeginTaskCardNavigateActionCoreAsync(taskRowID, model, editor, context);
                    }

                    return;
                }
            }

            await BeginTaskCardNavigateActionCoreAsync(taskRowID, model, editor, context);
        }


        private static async ValueTask BeginTaskCardNavigateActionCoreAsync(
            Guid? taskRowID,
            ICardModel model,
            ICardEditorModel editor,
            IUIContext context)
        {
            if (model.CardType.ID == DefaultCardTypes.WfTaskCardTypeID)
            {
                Guid? mainCardID = model.Card
                    .Sections[CardSatelliteHelper.SatellitesSectionName]
                    .RawFields
                    .Get<Guid?>(CardSatelliteHelper.MainCardIDColumn);

                if (mainCardID.HasValue)
                {
                    var info = new Dictionary<string, object>();

                    object permissionsCalculated = model.Card.Info.TryGet<object>(KrPermissionsHelper.PermissionsCalculatedMark);
                    if (permissionsCalculated is not null)
                    {
                        info[KrPermissionsHelper.PermissionsCalculatedMark] = permissionsCalculated;
                        info[KrPermissionsHelper.CalculatePermissionsMark] = permissionsCalculated;
                    }

                    await editor.OpenCardAsync(
                        mainCardID.Value,
                        cardTypeID: null,
                        cardTypeName: null,
                        context: context,
                        info: info);
                }
            }
            else if (taskRowID.HasValue)
            {
                var info = new Dictionary<string, object>();
                info.SetDigest(model.Digest);

                object permissionsCalculated = model.Card.Info.TryGet<object>(KrPermissionsHelper.PermissionsCalculatedMark);
                if (permissionsCalculated is not null)
                {
                    info[KrPermissionsHelper.PermissionsCalculatedMark] = permissionsCalculated;
                }

                KrToken.TryGet(model.Card.Info)?.Set(info);

                await editor.OpenCardAsync(
                    taskRowID.Value,
                    DefaultCardTypes.WfTaskCardTypeID,
                    DefaultCardTypes.WfTaskCardTypeName,
                    context,
                    info);
            }
        }


        private async ValueTask<WfResolutionTaskInfo> CreateTaskInfoAsync(
            TaskViewModel taskViewModel,
            CancellationToken cancellationToken = default)
        {
            Card taskCard = taskViewModel.TaskModel.CardTask.TryGetCard();
            StringDictionaryStorage<CardSection> taskSections;
            ListStorage<CardRow> childrenRows = null;

            bool hasChildren =
                taskCard is not null
                && (taskSections = taskCard.TryGetSections()) is not null
                && taskSections.TryGetValue(WfHelper.ResolutionChildrenVirtualSection, out CardSection childrenSection)
                && (childrenRows = childrenSection.TryGetRows()) is not null
                && childrenRows.Count > 0;

            bool hasIncompleteChildren =
                hasChildren
                && childrenRows.Any(x =>
                    x.State != CardRowState.Deleted
                    && x.TryGet<object>(WfHelper.ResolutionChildrenCompletedField) is null);

            return new WfResolutionTaskInfo(
                taskViewModel,
                await this.settingsLazy.GetValueAsync(cancellationToken).ConfigureAwait(false),
                hasChildren,
                hasIncompleteChildren);
        }


        private void VisualizeBranch(TaskViewModel taskViewModel)
        {
            ICardModel taskModel = taskViewModel.TaskModel;

            ICardModel cardModel = taskModel.ParentModel;
            Guid rootResolutionRowID = taskModel.CardTask.RowID;

            this.VisualizeAsync(cardModel, rootResolutionRowID);
        }


        private void VisualizeProcess(TaskViewModel taskViewModel)
        {
            ICardModel taskModel = taskViewModel.TaskModel;

            ICardModel cardModel = taskModel.ParentModel;
            Guid rootResolutionRowID = taskModel.CardTask.RowID;

            this.VisualizeProcess(cardModel, rootResolutionRowID);
        }


        private void VisualizeProcess(ICardModel cardModel, Guid resolutionRowID)
        {
            Guid rootResolutionRowID = resolutionRowID;
            Guid? currentResolutionRowID = resolutionRowID;
            ListStorage<CardTaskHistoryItem> taskHistory = cardModel.Card.TryGetTaskHistory();
            if (taskHistory is not null && taskHistory.Count > 0)
            {
                while (currentResolutionRowID.HasValue)
                {
                    foreach (CardTaskHistoryItem historyItem in taskHistory)
                    {
                        if (historyItem.RowID == rootResolutionRowID)
                        {
                            currentResolutionRowID = historyItem.ParentRowID;
                            if (currentResolutionRowID.HasValue)
                            {
                                rootResolutionRowID = currentResolutionRowID.Value;
                            }

                            break;
                        }
                    }
                }
            }

            this.VisualizeAsync(cardModel, rootResolutionRowID);
        }


        private const string WfResolutionCardToVisualizeKey = "WfResolutionCardToVisualize";

        private async void VisualizeAsync(ICardModel model, Guid rootResolutionRowID)
        {
            Card cachedCardToVisualize = model.Info.TryGet<Card>(WfResolutionCardToVisualizeKey);
            if (cachedCardToVisualize is not null)
            {
                await this.VisualizeLoadedCardAsync(cachedCardToVisualize, rootResolutionRowID);
                return;
            }

            Card card = model.Card;
            if (card.StoreMode == CardStoreMode.Insert)
            {
                return;
            }

            // загружаем данные карточки, используемые при визуализации
            var request = new CardRequest
            {
                CardID = card.ID,
                RequestType = DefaultRequestTypes.GetResolutionVisualizationData,
            };

            CardResponse response;
            using (TessaSplash.Create(TessaSplashMessage.OpeningCard))
            {
                response = await this.extendedRepository.RequestAsync(request);
            }

            ValidationResult responseResult = response.ValidationResult.Build();
            TessaDialog.ShowNotEmpty(responseResult);

            Card cardToVisualize;
            if (!responseResult.IsSuccessful
                || (cardToVisualize = WfHelper.TryGetResponseCard(response)) is null)
            {
                return;
            }

            model.Info[WfResolutionCardToVisualizeKey] = cardToVisualize;
            await this.VisualizeLoadedCardAsync(cardToVisualize, rootResolutionRowID);
        }


        private async Task VisualizeLoadedCardAsync(Card cardToVisualize, Guid rootResolutionRowID)
        {
            // ищем запись в истории заданий, начиная с которой выполняется визуализация
            CardTaskHistoryItem rootHistoryItem = null;
            ListStorage<CardTaskHistoryItem> taskHistory = cardToVisualize.TryGetTaskHistory();
            if (taskHistory is not null && taskHistory.Count > 0)
            {
                foreach (CardTaskHistoryItem historyItem in taskHistory)
                {
                    if (historyItem.RowID == rootResolutionRowID)
                    {
                        rootHistoryItem = historyItem;
                        break;
                    }
                }
            }

            // если записи в истории нет, то кто-то карточку поломал; выходим, ругнувшись
            if (rootHistoryItem is null)
            {
                TessaDialog.ShowError("$WfResolution_Error_TaskNotFoundInVisualization");
                return;
            }

            // ищем задание, начиная с которого выполняется визуализация
            ListStorage<CardTask> tasksToVisualize = cardToVisualize.TryGetTasks();
            CardTask rootResolution = null;
            if (tasksToVisualize is not null && tasksToVisualize.Count > 0)
            {
                foreach (CardTask taskToVisualize in tasksToVisualize)
                {
                    if (taskToVisualize.RowID == rootResolutionRowID)
                    {
                        rootResolution = taskToVisualize;
                        break;
                    }
                }
            }

            INodeLayout nodeLayout = new NodeLayout(dialogService);
            INodeProcessor nodeProcessor = null;
            try
            {
                // строим узлы визуализации по заданиям и записям в истории
                INodeFactory nodeFactory = new NodeFactory();
                await WfUIHelper
                    .VisualizeResolutionsAsync(
                        this.getGeneratorFunc(),
                        nodeLayout,
                        nodeFactory,
                        cardToVisualize,
                        rootHistoryItem,
                        rootResolution,
                        this.session,
                        this.extensionContainer);

                if (nodeLayout.Nodes.Count == 0)
                {
                    return;
                }

                // упорядочиваем узлы визуализации и выводим их в окне
                nodeProcessor = this.createNodeProcessorFunc();
                nodeProcessor.Layout = nodeLayout;
                this.dialogManager.ShowVisualizer(nodeLayout, nodeProcessor);

                // nodeLayout и nodeProcessor будут освобождены окном, когда оно закроется
                nodeProcessor = null;
                nodeLayout = null;
            }
            finally
            {
                nodeProcessor?.Dispose();
                nodeLayout?.Dispose();
            }
        }


        private static void SetTagForFileFromThisTask(IFileViewModel fileViewModel, IIconContainer icons)
        {
            var background = ThemeManager.Current.Theme.GetColor(ThemeProperty.FileCurrentTaskTagBackground);
            fileViewModel.Tag = new FileTagViewModel("Thin20", icons, background);
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            // если нет ни одного задания резолюции в истории, то выходим
            bool isTaskCard = context.Model.CardType.ID == DefaultCardTypes.WfTaskCardTypeID;

            TaskHistoryViewModel taskHistory = context.Model.TryGetTaskHistory();
            if (taskHistory is null
                || !isTaskCard && taskHistory.Items.All(x => x.Model.ParentRowID is not null || !WfHelper.TaskTypeIsResolution(x.Model.TypeID)))
            {
                return;
            }

            if (isTaskCard)
            {
                // если предыдущая карточка отличалась от карточки-сателлита задачи (т.е. была основной карточкой), то не переносим состояние контролов
                // (например, группировку файлов) в открываемую карточку
                ICardEditorModel editor = context.UIContext.CardEditor;
                if (editor?.CardModel?.CardType.ID != DefaultCardTypes.WfTaskCardTypeID)
                {
                    context.Model.StateIsInitialized = true;
                }

                // заголовок вкладки устанавливаем таким же, как тип (или вид) задания
                string typeCaption;

                CardTask task = context.Card.Tasks.FirstOrDefault();
                if (task is not null)
                {
                    typeCaption = 
                        new CardTaskInfoModel(
                                task, 
                                (await context.Model.GeneralMetadata.GetEnumerationsAsync(context.CancellationToken)).FunctionRoles)
                            .TypeCaption;
                }
                else
                {
                    CardTaskHistoryItem historyItem = context.Card.TaskHistory.FirstOrDefault();
                    typeCaption = historyItem?.TypeCaption;
                }

                if (!string.IsNullOrWhiteSpace(typeCaption)
                    && context.Model.Forms.Count > 0)
                {
                    context.Model.Forms[0].TabCaption = typeCaption;
                }

                // ссылка "вернуться в карточку"
                if (context.Model.Controls.TryGet(NavigateMainCardControl, out var control)
                    && control is HyperlinkViewModel hyperlink)
                {
                    hyperlink.ControlVisibility = Visibility.Collapsed;
                    hyperlink.Block.Form.RearrangeSelf();

                    var toolbarAction = new CardToolbarAction(
                        NavigateMainCardControl,
                        hyperlink.Text,
                        tooltip: hyperlink.ToolTip,
                        icon: null,
                        command: new DelegateCommand(async p => await TaskCardNavigateActionAsync(null))
                    );
                    context.ToolbarActions.Clear();
                    context.ToolbarActions.Add(toolbarAction);
                }

                if (context.Model.Controls.TryGet("TaskFiles", out control)
                    && control is FileListViewModel fileList)
                {
                    IFileControl fileControl = fileList.FileControl;

                    // фильтр категорий файлов, откуда удаляется категория "Файлы карточки"
                    if (fileControl.IsCategoriesEnabled)
                    {
                        var prevFilter = fileControl.CategoryFilterAsync;
                        if (prevFilter is null)
                        {
                            fileControl.CategoryFilterAsync = async (context) =>
                                context.Categories
                                    .Where(x => x?.ID != WfHelper.MainCardCategoryID);
                        }
                        else
                        {
                            fileControl.CategoryFilterAsync = async (context) =>
                                (await prevFilter(context) ?? Enumerable.Empty<IFileCategory>())
                                .Where(x => x?.ID != WfHelper.MainCardCategoryID);
                        }
                    }

                    // теги-иконки для файлов от этой задачи
                    IIconContainer icons = context.Icons;
                    foreach (IFileViewModel fileViewModel in fileControl.Items)
                    {
                        IFile file = fileViewModel.Model;
                        var cardFile = context.Card.Files.FirstOrDefault(x => x.RowID == file.ID);
                        if (cardFile is null
                            || cardFile.ExternalSource is null)
                        {
                            SetTagForFileFromThisTask(fileViewModel, icons);
                        }
                    }

                    fileControl.Items.CollectionChanged += async (s, e) =>
                    {
                        if (e.NewItems is not null)
                        {
                            foreach (IFileViewModel fileViewModel in e.NewItems)
                            {
                                // если создаётся копия файла из виртуальных файлов карточки, то надо сбросить категорию файла на "Без категории";
                                // если выполняется копирование файла в основную карточку, то меняется категория, но Origin остаётся как null

                                IFile file = fileViewModel.Model;
                                bool categoryIsMainCard = file.Category?.ID == WfHelper.MainCardCategoryID;

                                if (categoryIsMainCard && file.Origin is not null)
                                {
                                    await file.SetCategoryAsync(null);
                                    await file.SetOriginAsync(null);

                                    file.Info[WfHelper.CopiedToMainCardKey] = BooleanBoxes.True;
                                    categoryIsMainCard = false;
                                }

                                if (!categoryIsMainCard)
                                {
                                    SetTagForFileFromThisTask(fileViewModel, icons);
                                }
                            }
                        }
                    };
                }
            }
            else
            {
                // открывается карточка, отличная от сателлита, причём в Info присутствует сохранённый State от основной карточки
                // (до того, как мы ушли в сателлит), поэтому восстанавливаем состояние
                ICardEditorModel editor = context.UIContext.CardEditor;
                IFormState mainFormState = editor?.Info.TryGet<IFormState>(WfHelper.MainCardStateKey);
                if (mainFormState is not null)
                {
                    if (context.Model.MainForm is not null)
                    {
                        await context.Model.MainForm.SetStateAsync(mainFormState, context.CancellationToken);
                    }

                    context.Model.StateIsInitialized = true;

                    // восстанавливаем его только один раз
                    editor.Info.Remove(WfHelper.MainCardStateKey);
                }

                context.ToolbarActions.RemoveAll(p => p.Name == NavigateMainCardControl);
            }

            // добавляем обработчики, связанные с отображением задания резолюции и его кнопок
            if (!context.Model.InSpecialMode())
            {
                await context.Model.ModifyTasksAsync(async (task, model) =>
                {
                    if (WfHelper.TaskTypeIsResolution(task.TaskModel.CardType.ID))
                    {
                        WfResolutionTaskInfo taskInfo = await this.CreateTaskInfoAsync(task);

                        await task.ModifyWorkspaceAsync(async (t, subscribeToTaskModel) =>
                            await this.ModifyResolutionTaskAsync(taskInfo, model, isTaskCard, subscribeToTaskModel));

                        task.PostponeMetadataInitializing += PostponeMetadataInitializingAsync;
                        task.PostponeContentInitializing += PostponeContentInitializing;
                    }
                });
            }

            if (!isTaskCard)
            {
                // добавляем генератор контекстного меню, если визуализатор есть в лицензии
                bool specialMode = context.Model.InSpecialMode();
                bool addVisualizationItems = !specialMode && await this.HasLicensedWorkflowViewerAsync();

                ICardModel modelClosure = context.Model;

                taskHistory.ContextMenuGenerators.Add(ctx =>
                {
                    if (WfHelper.TaskTypeIsResolution(ctx.HistoryItem.Model.TypeID))
                    {
                        if (addVisualizationItems)
                        {
                            ctx.MenuActions.Insert(
                                0,
                                new MenuAction(
                                    WfUIHelper.VisualizeResolutionBranchMenuAction,
                                    "$WfResolution_VisualizeBranch",
                                    Icon.Empty,
                                    new DelegateCommand(p => this.VisualizeAsync(modelClosure, ctx.HistoryItem.Model.RowID))));

                            ctx.MenuActions.Insert(
                                1,
                                new MenuAction(
                                    WfUIHelper.VisualizeResolutionProcessMenuAction,
                                    "$WfResolution_VisualizeProcess",
                                    Icon.Empty,
                                    new DelegateCommand(p => this.VisualizeProcess(modelClosure, ctx.HistoryItem.Model.RowID))));
                        }

                        if (!specialMode)
                        {
                            int fileCount = ctx.HistoryItem.Model.Info.TryGet<int>(WfHelper.FileCountTaskKey);

                            ctx.MenuActions.Insert(
                                2,
                                new MenuAction(
                                    WfUIHelper.NavigateTaskCardResolutionProcessMenuAction,
                                    fileCount > 0
                                        ? string.Format(LocalizationManager.GetString("WfTaskFiles_ShowFilesTagCount_ContextMenu"), fileCount)
                                        : "$WfTaskFiles_ShowFilesTag_ContextMenu",
                                    Icon.Empty,
                                    new DelegateCommand(async p => await TaskCardNavigateActionAsync(ctx.HistoryItem.Model.RowID))));

                            if (ctx.MenuActions.Count > 3)
                            {
                                ctx.MenuActions.Insert(
                                    3,
                                    new MenuSeparatorAction(WfUIHelper.VisualizeResolutionSeparatorMenuAction));
                            }
                        }
                    }

                    return new ValueTask();
                });

                foreach (TaskHistoryItemViewModel item in taskHistory.EnumerateHierarchy())
                {
                    int fileCount;

                    if (WfHelper.TaskTypeIsResolution(item.Model.TypeID)
                        && (fileCount = item.Model.Info.TryGet<int>(WfHelper.FileCountTaskKey)) > 0)
                    {
                        item.SetTag(
                            "Thin43",
                            string.Format(
                                await LocalizationManager.GetStringAsync(
                                    specialMode
                                        ? "WfTaskFiles_FilesTagCount_ToolTip"
                                        : "WfTaskFiles_ShowFilesTagCount_ToolTip"),
                                fileCount),
                            specialMode
                                ? DelegateCommand.Empty
                                : new DelegateCommand(async p => await TaskCardNavigateActionAsync(item.Model.RowID)));
                    }
                }
            }
        }


        public override async Task Reopening(ICardUIExtensionContext context)
        {
            ICardEditorModel editor = context.UIContext.CardEditor;
            if (editor is null || context.GetRequest is null)
            {
                return;
            }

            if (context.Model.CardType.ID != DefaultCardTypes.WfTaskCardTypeID)
            {
                // если была открыта не карточка-сателлит, а другая карточка, и сейчас открывается карточка-сателлит,
                // то сохраним информацию по выбранной вкладке и прочим параметрам для основной карточки, чтобы потом мы смогли всё восстановить
                if (context.GetRequest.CardTypeID == DefaultCardTypes.WfTaskCardTypeID)
                {
                    editor.Info[WfHelper.MainCardStateKey] = context.Model.MainForm.GetState();
                }
            }
            else if (editor.CurrentOperationType == CardEditorOperationType.SaveAndRefresh)
            {
                // если была открыта карточка-сателлит, то при её рефреше сразу после сохранения с завершением задания
                // мы будем загружать основную карточку, а не сателлит

                CardStoreResponse storeResponse = editor.LastData.StoreResponse;
                Guid? responseCardID;

                if (storeResponse is not null
                    && (responseCardID = storeResponse.Info.TryGet<Guid?>(WfHelper.NextCardIDKey)).HasValue)
                {
                    // открываем основную карточку вместо карточки задания, если было завершено задание (про это знает серверное расширение)
                    context.GetRequest.CardID = responseCardID;
                    context.GetRequest.CardTypeID = storeResponse.Info.TryGet<Guid?>(WfHelper.NextCardTypeIDKey);
                    context.GetRequest.CardTypeName = null;
                }
            }
        }

        #endregion
    }
}
