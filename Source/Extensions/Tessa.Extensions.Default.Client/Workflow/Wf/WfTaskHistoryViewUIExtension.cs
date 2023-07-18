#nullable enable

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Localization;
using Tessa.Platform.Licensing;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Controls;
using Tessa.UI.Menu;
using Tessa.UI.Views.Content;
using Tessa.UI.WorkflowViewer.Factories;
using Tessa.UI.WorkflowViewer.Helpful;
using Tessa.UI.WorkflowViewer.Layouts;
using Tessa.UI.WorkflowViewer.Processors;
using Unity;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Реализация расширения типа карточки для добавления представлению функционала Истории заданий
    /// Добавляет тэги для открытия сателлита с файлами и контекстное меню для визуализации процесса и открытия сателлита задания.
    /// </summary>
    public sealed class WfTaskHistoryViewUIExtension :
        CardUIExtension
    {
        #region Constructors

        public WfTaskHistoryViewUIExtension(
            ILicenseManager licenseManager,
            ICardDialogManager dialogManager,
            IDialogService dialogService,
            ISession session,
            ICardRepository extendedRepository,
            IExtensionContainer extensionContainer,
            Func<IWfResolutionVisualizationGenerator> getGeneratorFunc,
            [Dependency("ResolutionsNodeProcessor")] Func<INodeProcessor> createNodeProcessorFunc)
        {
            licenseManager.ValidateTypeOnClient();

            this.licenseManager = licenseManager; // Значение было проверено в ValidateTypeOnClient.
            this.dialogManager = NotNullOrThrow(dialogManager);
            this.dialogService = NotNullOrThrow(dialogService);
            this.extendedRepository = NotNullOrThrow(extendedRepository);
            this.session = NotNullOrThrow(session);
            this.extensionContainer = NotNullOrThrow(extensionContainer);
            this.getGeneratorFunc = NotNullOrThrow(getGeneratorFunc);
            this.createNodeProcessorFunc = NotNullOrThrow(createNodeProcessorFunc);
        }

        #endregion

        #region Fields

        private const string WfResolutionCardToVisualizeKey = "WfResolutionCardToVisualize";

        private readonly ILicenseManager licenseManager;

        private readonly ICardDialogManager dialogManager;

        private readonly IDialogService dialogService;

        private readonly ICardRepository extendedRepository;

        private readonly ISession session;

        private readonly IExtensionContainer extensionContainer;

        private readonly Func<IWfResolutionVisualizationGenerator> getGeneratorFunc;

        private readonly Func<INodeProcessor> createNodeProcessorFunc;

        private static bool? hasLicensedWorkflowViewer;

        #endregion

        #region Base overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var card = context.Card;

            // выполняем метод расширения
            var result = await CardHelper
                .ExecuteTypeExtensionsAsync(
                    CardTypeExtensionTypes.MakeViewTaskHistory,
                    card,
                    context.Model.CardMetadata,
                    this.ExecuteActionAsync,
                    context,
                    cancellationToken: context.CancellationToken);

            context.ValidationResult.Add(result);
        }

        #endregion

        #region Type extension methods

        /// <summary>
        /// функция, реализующая метод расширения, если он есть
        /// </summary>
        /// <param name="typeContext"></param>
        /// <returns></returns>
        private async Task ExecuteActionAsync(ITypeExtensionContext typeContext)
        {
            var context = (ICardUIExtensionContext) NotNullOrThrow(typeContext.ExternalContext);
            var settings = typeContext.Settings;
            var viewControlAlias = NotNullOrThrow(settings).TryGet<string>(CardTypeExtensionSettings.ViewControlAlias);

            if (!context.Model.Controls.TryGet(viewControlAlias, out var controlViewModel))
            {
                context.ValidationResult.AddException(this,
                    new ArgumentException($"Control ViewModel with Name='{viewControlAlias}' not found.", nameof(viewControlAlias)));
                return;
            }

            var isTaskCard = context.Model.CardType.ID == DefaultCardTypes.WfTaskCardTypeID;

            // если это сателлит задания, то в него не нужно добавлять визуализацию
            if (isTaskCard)
            {
                return;
            }

            var specialMode = context.Model.InSpecialMode();
            var addVisualizationItems = !specialMode && await this.HasLicensedWorkflowViewerAsync();
            var modelClosure = context.Model;

            var taskHistoryView = (CardViewControlViewModel) controlViewModel;

            // добавляем тэги
            taskHistoryView.ModifyRowActions.Add(row =>
            {
                if (row is null)
                {
                    return;
                }
                if (row.Data is null)
                {
                    return;
                }
                long filesCount = 0;
                if (row.Data.TryGetValue("FilesCount", out var value))
                {
                    filesCount = (long) value;
                }
                if (filesCount > 0
                    && row.Data["TypeID"] is not null
                    && WfHelper.TaskTypeIsResolution((Guid) row.Data["TypeID"]))
                {

                    row.CellsByColumnName["TypeCaption"].AddLeftTag(
                        "AttachedFiles",
                        new IconViewModel("Thin43", row.MenuContext.Icons),
                        visible: true,
                        toolTip: string.Format(LocalizationManager.GetString(
                            specialMode
                                ? "WfTaskFiles_FilesTagCount_ToolTip"
                                : "WfTaskFiles_ShowFilesTagCount_ToolTip"), filesCount),
                        command: specialMode
                            ? DelegateCommand.Empty
                            : new DelegateCommand(async _ => await TaskCardNavigateActionAsync(
                                (Guid) row.Data["RowID"],
                                context.UIContext))
                        );
                }
            });

            // добавляем генератор контекстного меню
            taskHistoryView.RowContextMenuGenerators.Add(ctx =>
            {
                if (ctx.RowViewModel.Data["TypeID"] is not null
                    && WfHelper.TaskTypeIsResolution(ctx.RowViewModel.Data.Get<Guid>("TypeID")))
                {
                    if (addVisualizationItems)
                    {
                        ctx.MenuActions.Insert(
                            0,
                            new MenuAction(
                                WfUIHelper.VisualizeResolutionBranchMenuAction,
                                "$WfResolution_VisualizeBranch",
                                Icon.Empty,
                                new DelegateCommand(async _ => await this.VisualizeAsync(
                                    modelClosure,
                                    ctx.RowViewModel.Data.Get<Guid>("RowID")))));

                        ctx.MenuActions.Insert(
                            1,
                            new MenuAction(
                                WfUIHelper.VisualizeResolutionProcessMenuAction,
                                "$WfResolution_VisualizeProcess",
                                Icon.Empty,
                                new DelegateCommand(async _ => await this.VisualizeProcessAsync(
                                    modelClosure,
                                    ctx.RowViewModel))));
                    }

                    if (!specialMode)
                    {
                        long filesCount = 0;
                        if (ctx.RowViewModel.Data.TryGetValue("FilesCount", out var value))
                        {
                            filesCount = (long) value;
                        }

                        ctx.MenuActions.Insert(
                            2,
                            new MenuAction(
                                WfUIHelper.NavigateTaskCardResolutionProcessMenuAction,
                                filesCount > 0
                                    ? string.Format(LocalizationManager.GetString("WfTaskFiles_ShowFilesTagCount_ContextMenu"), filesCount)
                                    : "$WfTaskFiles_ShowFilesTag_ContextMenu",
                                Icon.Empty,

                                new DelegateCommand(async _ => await TaskCardNavigateActionAsync(
                                    ctx.RowViewModel.Data.Get<Guid>("RowID"),
                                    context.UIContext)),
                                isCollapsed: filesCount == 0));

                        if (ctx.MenuActions.Count > 3)
                        {
                            ctx.MenuActions.Insert(
                                3,
                                new MenuSeparatorAction(WfUIHelper.VisualizeResolutionSeparatorMenuAction));
                        }
                    }
                }

                return ValueTask.CompletedTask;
            });

        }

        #endregion

        #region WF visualization private methods

        private async Task VisualizeProcessAsync(ICardModel modelClosure, TableRowViewModel row)
        {
            var currenRow = row;
            while (currenRow.Parent is not null)
            {
                currenRow = currenRow.Parent;
            }

            await this.VisualizeAsync(modelClosure, currenRow.Data.Get<Guid>("RowID"));
        }

        private async Task VisualizeAsync(ICardModel model, Guid rootResolutionRowID)
        {
            var cachedCardToVisualize = model.Info.TryGet<Card>(WfResolutionCardToVisualizeKey);
            if (cachedCardToVisualize is not null)
            {
                await this.VisualizeLoadedCardAsync(cachedCardToVisualize, rootResolutionRowID);
                return;
            }

            var card = model.Card;
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

            var responseResult = response.ValidationResult.Build();
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
            CardTaskHistoryItem? rootHistoryItem = null;
            var taskHistory = cardToVisualize.TryGetTaskHistory();
            if (taskHistory is not null && taskHistory.Count > 0)
            {
                foreach (var historyItem in taskHistory)
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
            var tasksToVisualize = cardToVisualize.TryGetTasks();
            CardTask? rootResolution = null;
            if (tasksToVisualize is not null && tasksToVisualize.Count > 0)
            {
                foreach (var taskToVisualize in tasksToVisualize)
                {
                    if (taskToVisualize.RowID == rootResolutionRowID)
                    {
                        rootResolution = taskToVisualize;
                        break;
                    }
                }
            }

            INodeLayout? nodeLayout = new NodeLayout(this.dialogService);
            INodeProcessor? nodeProcessor = null;
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

        #endregion

        #region Private methods

        private async ValueTask<bool> HasLicensedWorkflowViewerAsync(CancellationToken cancellationToken = default)
        {
            var hasViewer = hasLicensedWorkflowViewer;
            if (hasViewer.HasValue)
            {
                return hasViewer.Value;
            }

            var computedHasViewer = NodeProcessorHelper.CheckLicense(
                await this.licenseManager.GetLicenseAsync(cancellationToken),
                out _);

            hasLicensedWorkflowViewer = computedHasViewer;

            return computedHasViewer;
        }

        private static async Task TaskCardNavigateActionAsync(Guid? taskRowID, IUIContext context)
        {
            if (context.CardEditor is not { } editor
                || editor.CardModel is not { } model)
            {
                return;
            }

            if (await model.HasChangesAsync())
            {
                var saveCard = TessaDialog.ConfirmWithCancel("$WfTaskFiles_SaveChangesConfirmation");
                if (!saveCard.HasValue)
                {
                    return;
                }

                if (saveCard.Value && !editor.OperationInProgress)
                {
                    var success = await editor.SaveCardAsync(context, request: new CardSavingRequest(CardSavingMode.KeepPreviousCard));

                    if (success)
                    {
                        await BeginTaskCardNavigateActionCoreAsync(taskRowID, model, editor, context);
                    }

                    return;
                }
            }

            await BeginTaskCardNavigateActionCoreAsync(taskRowID, model, editor, context);
        }

        private static async Task BeginTaskCardNavigateActionCoreAsync(
            Guid? taskRowID,
            ICardModel model,
            ICardEditorModel editor,
            IUIContext context)
        {
            if (model.CardType.ID == DefaultCardTypes.WfTaskCardTypeID)
            {
                var mainCardID = model.Card
                    .Sections[CardSatelliteHelper.SatellitesSectionName]
                    .RawFields
                    .Get<Guid?>(CardSatelliteHelper.MainCardIDColumn);

                if (mainCardID.HasValue)
                {
                    var info = new Dictionary<string, object>(StringComparer.Ordinal);

                    var permissionsCalculated = model.Card.Info.TryGet<object>(KrPermissionsHelper.PermissionsCalculatedMark);
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
                var info = new Dictionary<string, object?>(StringComparer.Ordinal);
                info.SetDigest(model.Digest);

                var permissionsCalculated = model.Card.Info.TryGet<object>(KrPermissionsHelper.PermissionsCalculatedMark);
                if (permissionsCalculated is not null)
                {
                    info[KrPermissionsHelper.PermissionsCalculatedMark] = permissionsCalculated;
                }

                await editor.OpenCardAsync(
                    taskRowID.Value,
                    DefaultCardTypes.WfTaskCardTypeID,
                    DefaultCardTypes.WfTaskCardTypeName,
                    context,
                    info);
            }
        }

        #endregion
    }
}
