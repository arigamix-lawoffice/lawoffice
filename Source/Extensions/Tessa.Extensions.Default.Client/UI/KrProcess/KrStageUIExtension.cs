#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers;
using Tessa.Extensions.Default.Client.Views.StageSelector;
using Tessa.Extensions.Default.Shared.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Menu;
using Tessa.UI.Windows;
using Tessa.Views;
using Tessa.Views.Metadata;
using Tessa.Views.Metadata.Criteria;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public sealed class KrStageUIExtension :
        CardUIExtension
    {
        #region Nested Types

        private sealed class VisibilityViaTagsVisitor :
            BreadthFirstControlVisitor
        {
            private readonly Guid typeID;

            public VisibilityViaTagsVisitor(
                Guid typeID) => this.typeID = typeID;

            /// <inheritdoc />
            protected override void VisitControl(
                IControlViewModel controlViewModel) => SetControlSettings(this.typeID, controlViewModel);

            /// <inheritdoc />
            protected override void VisitBlock(
                IBlockViewModel blockViewModel) => SetBlockControlsSettings(this.typeID, blockViewModel);
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        private readonly IStageTypeFormatterContainer formatterContainer;

        private readonly ISession session;

        private readonly IViewService viewService;

        private readonly IKrProcessUIContainer uiContainer;

        private readonly ICardMetadata cardMetadata;

        private readonly Dictionary<Guid, List<IStageTypeUIHandler>> handlersCache;

        private readonly List<IDisposable> finalizedObjects;

        #endregion

        #region Constructors

        public KrStageUIExtension(
            IKrTypesCache typesCache,
            IStageTypeFormatterContainer formatterContainer,
            ISession session,
            IViewService viewService,
            IKrProcessUIContainer uiContainer,
            ICardMetadata cardMetadata)
        {
            this.typesCache = NotNullOrThrow(typesCache);
            this.formatterContainer = NotNullOrThrow(formatterContainer);
            this.session = NotNullOrThrow(session);
            this.uiContainer = NotNullOrThrow(uiContainer);
            this.cardMetadata = NotNullOrThrow(cardMetadata);
            this.viewService = NotNullOrThrow(viewService);
            this.handlersCache = new Dictionary<Guid, List<IStageTypeUIHandler>>();
            this.finalizedObjects = new List<IDisposable>();
        }

        #endregion

        #region Private Methods

        private async void AddStageDialogHandlerAsync(object? sender, GridRowAddingEventArgs e)
        {
            var card = e.CardModel.Card;
            StageGroupViewModel? group = null; //фильтруем если диалог вызван из шаблона этапа

            CardSection? krSecondaryProcess = null;
            var hasGroup = card.Sections.TryGetValue(KrConstants.KrStageTemplates.Name, out var krStageTemplates)
                || card.Sections.TryGetValue(KrConstants.KrSecondaryProcesses.Name, out krSecondaryProcess);

            if (hasGroup)
            {
                if (krStageTemplates is not null)
                {
                    var groupID = krStageTemplates.RawFields.TryGet<Guid?>(KrConstants.KrStageTemplates.StageGroupID);
                    if (groupID.HasValue)
                    {
                        group = new StageGroupViewModel(
                            groupID.Value,
                            krStageTemplates.RawFields.TryGet<string>(KrConstants.KrStageTemplates.StageGroupName),
                            order: 0);
                    }
                }
                else if (krSecondaryProcess is not null)
                {
                    group = new StageGroupViewModel(
                        card.ID,
                        krSecondaryProcess.RawFields.TryGet<string>(KrConstants.KrSecondaryProcesses.NameField),
                        order: 0);
                }
            }

            var type = (card.Sections.TryGetValue(KrConstants.DocumentCommonInfo.Name, out var commonInfo)
                ? commonInfo.RawFields.TryGet<Guid?>(KrConstants.DocumentCommonInfo.DocTypeID)
                : null) ?? card.TypeID;

            var viewModel = new StageSelectorViewModel(
                group,
                card.ID,
                type,
                async (groupVm, cardId, typeId, ct) =>
                {
                    if (hasGroup && groupVm is null)
                    {
                        // группа не выбрана в шаблоне этапов
                        return Array.Empty<StageGroupViewModel>();
                    }

                    if (groupVm is not null)
                    {
                        return new List<StageGroupViewModel> { groupVm };
                    }

                    var stageGroupsView = await this.viewService.GetByNameAsync(KrConstants.Views.KrStageGroups, ct);
                    if (stageGroupsView is null)
                    {
                        return Array.Empty<StageGroupViewModel>();
                    }

                    var stageGroupsViewMetadata = await stageGroupsView.GetMetadataAsync(ct);
                    var request = new TessaViewRequest(stageGroupsViewMetadata);

                    var cardIdParam = new RequestParameterBuilder()
                        .WithMetadata(stageGroupsViewMetadata.Parameters.FindByName("CardId"))
                        .AddCriteria(new EqualsCriteriaOperator(), string.Empty, cardId)
                        .AsRequestParameter();

                    request.Values.Add(cardIdParam);

                    var typeIdParam = new RequestParameterBuilder()
                        .WithMetadata(stageGroupsViewMetadata.Parameters.FindByName("TypeId"))
                        .AddCriteria(new EqualsCriteriaOperator(), string.Empty, typeId)
                        .AsRequestParameter();

                    request.Values.Add(typeIdParam);

                    var result = await stageGroupsView.GetDataAsync(request, ct).ConfigureAwait(false);

                    return result.Rows is null
                        ? Array.Empty<StageGroupViewModel>()
                        : result.Rows
                            .Cast<IList<object>>()
                            .Select(row =>
                                new StageGroupViewModel(
                                    (Guid) row[0],
                                    (string) row[1],
                                    (int) row[4]))
                            .ToList();
                },
                async (groupType, cardId, typeId, ct) =>
                {
                    if (hasGroup && group is null)
                    {
                        // группа не выбрана в шаблоне этапов
                        return Array.Empty<StageTypeViewModel>();
                    }

                    var stageTypesView = await this.viewService.GetByNameAsync(KrConstants.Views.KrProcessStageTypes, ct);
                    if (stageTypesView is null)
                    {
                        return Array.Empty<StageTypeViewModel>();
                    }

                    var stageTypesViewMetadata = await stageTypesView.GetMetadataAsync(ct);
                    var request = new TessaViewRequest(stageTypesViewMetadata);

                    if (KrProcessSharedHelper.DesignTimeCard(typeId))
                    {
                        var isTemplateParam = new RequestParameterBuilder()
                            .WithMetadata(stageTypesViewMetadata.Parameters.FindByName("IsTemplate"))
                            .AddCriteria(new EqualsCriteriaOperator(), string.Empty, true)
                            .AsRequestParameter();

                        request.Values.Add(isTemplateParam);
                    }

                    var param = new RequestParameterBuilder()
                        .WithMetadata(stageTypesViewMetadata.Parameters.FindByName("StageGroupIDParam"))
                        .AddCriteria(new EqualsCriteriaOperator(), string.Empty, groupType)
                        .AsRequestParameter();

                    request.Values.Add(param);

                    var cardIdParam = new RequestParameterBuilder()
                        .WithMetadata(stageTypesViewMetadata.Parameters.FindByName("CardId"))
                        .AddCriteria(new EqualsCriteriaOperator(), string.Empty, cardId)
                        .AsRequestParameter();

                    request.Values.Add(cardIdParam);

                    var typeIdMetadata = stageTypesViewMetadata.Parameters.FindByName("TypeId");
                    RequestParameter? typeIdParam;
                    if (KrProcessSharedHelper.DesignTimeCard(typeId))
                    {
                        typeIdParam = null;

                        var typeRows = card.Sections[KrConstants.KrStageTypes.Name].Rows;
                        if (typeRows.Count > 0)
                        {
                            HashSet<Guid>? typeIdSet = null;

                            foreach (var row in typeRows)
                            {
                                if (row.State != CardRowState.Deleted)
                                {
                                    var templateTypeId = row.TryGet<Guid?>("TypeID");
                                    if (templateTypeId.HasValue)
                                    {
                                        typeIdSet ??= new HashSet<Guid>();
                                        typeIdSet.Add(templateTypeId.Value);
                                    }
                                }
                            }

                            if (typeIdSet is not null)
                            {
                                var typeIdBuilder = new RequestParameterBuilder()
                                    .WithMetadata(typeIdMetadata);

                                foreach (var id in typeIdSet)
                                {
                                    typeIdBuilder
                                        .AddCriteria(new EqualsCriteriaOperator(), string.Empty, id);
                                }

                                typeIdParam = typeIdBuilder.AsRequestParameter();
                            }
                        }
                    }
                    else
                    {
                        typeIdParam = new RequestParameterBuilder()
                            .WithMetadata(typeIdMetadata)
                            .AddCriteria(new EqualsCriteriaOperator(), string.Empty, typeId)
                            .AsRequestParameter();
                    }

                    if (typeIdParam is not null)
                    {
                        request.Values.Add(typeIdParam);
                    }

                    var result = await stageTypesView.GetDataAsync(request, ct).ConfigureAwait(false);

                    return result.Rows is null
                        ? Array.Empty<StageTypeViewModel>()
                        : result.Rows
                            .Cast<IList<object>>()
                            .Select(static row =>
                                new StageTypeViewModel(
                                    (Guid) row[0],
                                    (string) row[1],
                                    (string) row[2]))
                            .ToList();
                });

            var deferral = e.Defer();
            try
            {
                await viewModel.RefreshAsync(e.CancellationToken);

                if (viewModel.Groups.Count == 0)
                {
                    TessaDialog.ShowNotEmpty(ValidationResult.FromText("$UI_Error_NoAvailableStageGroups", ValidationResultType.Warning));
                    e.Cancel = true;
                    return;
                }

                // Для выбора доступна одна группа и один этап.
                if (viewModel.Groups.Count == 1 && viewModel.Types.Count == 1)
                {
                    viewModel.SelectedType = viewModel.Types.First();
                }
                else
                {
                    var window = new TessaWindow
                    {
                        Content = viewModel,
                        Title = await LocalizeNameAsync("UI_Cards_SelectGroupAndType"),
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        CloseKey = new KeyGesture(Key.Escape),
                        Width = 700.0,
                        Height = 470.0,
                    };

                    // поскольку типов групп и этапов может быть несколько сотен, надо ограничить вертикальный размер диалога
                    window.ResolveOwnerAsActiveWindow();
                    window.RestrictSizeToWorkingArea(restrictWidth: false);
                    window.CloseOnPreviewMiddleButtonDown();

                    window.ShowDialog();
                }

                if (viewModel.SelectedType is null || viewModel.SelectedGroup is null)
                {
                    e.Cancel = true;
                    return;
                }

                e.Row[KrConstants.KrStages.StageGroupID] = viewModel.SelectedGroup.ID;
                e.Row[KrConstants.KrStages.StageGroupName] = viewModel.SelectedGroup.Name;
                e.Row[KrConstants.KrStages.StageGroupOrder] = viewModel.SelectedGroup.Order;
                e.Row[KrConstants.KrStages.StageTypeID] = viewModel.SelectedType.ID;
                e.Row[KrConstants.KrStages.StageTypeCaption] = viewModel.SelectedType.Caption;
                e.Row[KrConstants.KrStages.NameField] = await LocalizeAsync(viewModel.SelectedType.DefaultStageName);

                if (!KrProcessSharedHelper.DesignTimeCard(e.CardModel.CardType.ID))
                {
                    var order = KrProcessSharedHelper.ComputeStageOrder(
                        viewModel.SelectedGroup.ID,
                        viewModel.SelectedGroup.Order,
                        e.Rows);

                    e.RowIndex = order;
                    e.Row[KrConstants.KrStages.OriginalOrder] = Int32Boxes.Box(order);
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

        private static void SetStageTypeTitle(
            object? sender,
            GridRowEventArgs e)
        {
            var row = e.Row;
            var stageTypeCaption = row.TryGet<string>(KrConstants.KrStages.StageTypeCaption);

            e.SetWindowTitle(row.State == CardRowState.Inserted && KrProcessSharedHelper.RuntimeCard(e.CardModel.Card.TypeID)
                ? LocalizationManager.Format("$UI_StageRowWindowTitle", stageTypeCaption, row.TryGet<string>(KrConstants.KrStages.StageGroupName))
                : LocalizationManager.Localize(stageTypeCaption));
        }

        private static Visibility? SetOptionalControlVisibility(
            IBlockViewModel block,
            string controlAlias,
            GridRowEventArgs e)
        {
            var inputControl = block.Controls.FirstOrDefault(p => p.Name == controlAlias);
            if (inputControl is null)
            {
                return null;
            }

            var controlSettings = inputControl.CardTypeControl.ControlSettings;
            if (!controlSettings.TryGetValue(KrConstants.Ui.VisibleForTypesSetting, out var obj) ||
                obj is not IList list)
            {
                return null;
            }

            var stageTypeID = e.Row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

            inputControl.ControlVisibility = Visibility.Collapsed;
            foreach (Guid id in list)
            {
                if (id == stageTypeID)
                {
                    inputControl.ControlVisibility = Visibility.Visible;
                    break;
                }
            }

            inputControl.IsRequired = false;
            if (controlSettings.TryGetValue(KrConstants.Ui.RequiredForTypesSetting, out var reqObj)
                && reqObj is IList requiredList)
            {
                foreach (Guid id in requiredList)
                {
                    if (id == stageTypeID)
                    {
                        inputControl.IsRequired = true;
                        break;
                    }
                }
            }

            SetControlSettings(e.CardModel.CardType.ID, inputControl);
            var visitor = new VisibilityViaTagsVisitor(e.CardModel.CardType.ID);
            visitor.Visit(inputControl);
            return inputControl.ControlVisibility;
        }

        private static void SetControlCaption(
            IBlockViewModel block,
            string controlAlias,
            GridRowEventArgs e)
        {
            var inputControl = block.Controls.FirstOrDefault(p => p.Name == controlAlias);
            if (inputControl is null)
            {
                return;
            }

            var controlSettings = inputControl.CardTypeControl.ControlSettings;
            if (!controlSettings.TryGetValue(KrConstants.Ui.ControlCaptionsSetting, out var obj) ||
                obj is not Dictionary<string, object> captions)
            {
                return;
            }

            var stageTypeID = e.Row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);
            if (stageTypeID.HasValue
                && captions.TryGetValue(stageTypeID.Value.ToString("D"), out var caption))
            {
                inputControl.Caption = caption.ToString();
            }
        }

        private static void ShowSettingsBlock(
            object? sender,
            GridRowEventArgs e)
        {
            if (e.RowModel.Blocks.TryGet(KrConstants.Ui.KrStageCommonInfoBlock, out var commonBlock))
            {
                SetOptionalControlVisibility(commonBlock, KrConstants.Ui.KrTimeLimitInputAlias, e);
                SetOptionalControlVisibility(commonBlock, KrConstants.Ui.KrPlannedInputAlias, e);
                SetOptionalControlVisibility(commonBlock, KrConstants.Ui.KrHiddenStageCheckboxAlias, e);
                SetOptionalControlVisibility(commonBlock, KrConstants.Ui.KrCanBeSkippedCheckboxAlias, e);
            }

            if (e.RowModel.Blocks.TryGet(KrConstants.Ui.KrPerformersBlockAlias, out var performersBlock))
            {
                var singleVisibility = SetOptionalControlVisibility(performersBlock, KrConstants.Ui.KrSinglePerformerEntryAcAlias, e);
                var multipleVisibility = SetOptionalControlVisibility(performersBlock, KrConstants.Ui.KrMultiplePerformersTableAcAlias, e);

                if (e.RowModel.Blocks.TryGet(KrConstants.Ui.KrSqlPerformersLinkBlock, out var sqlPerformersLinkBlock)
                    && multipleVisibility == Visibility.Visible)
                {
                    sqlPerformersLinkBlock.BlockVisibility = Visibility.Visible;
                }

                if (singleVisibility == Visibility.Visible)
                {
                    SetControlCaption(performersBlock, KrConstants.Ui.KrSinglePerformerEntryAcAlias, e);
                }

                if (multipleVisibility == Visibility.Visible)
                {
                    SetControlCaption(performersBlock, KrConstants.Ui.KrMultiplePerformersTableAcAlias, e);
                }
            }

            if (e.RowModel.Blocks.TryGet(KrConstants.Ui.AuthorBlockAlias, out var authorBlock))
            {
                SetOptionalControlVisibility(authorBlock, KrConstants.Ui.AuthorEntryAlias, e);
            }

            if (e.RowModel.Blocks.TryGet(KrConstants.Ui.TaskKindBlockAlias, out var taskKindBlock))
            {
                SetOptionalControlVisibility(taskKindBlock, KrConstants.Ui.TaskKindEntryAlias, e);
            }

            if (e.RowModel.Blocks.TryGet(KrConstants.Ui.KrTaskHistoryBlockAlias, out var historyBlock))
            {
                var v = SetOptionalControlVisibility(historyBlock, KrConstants.Ui.KrTaskHistoryGroupTypeControlAlias, e);
                SetOptionalControlVisibility(historyBlock, KrConstants.Ui.KrParentTaskHistoryGroupTypeControlAlias, e);
                SetOptionalControlVisibility(historyBlock, KrConstants.Ui.KrTaskHistoryGroupNewIterationControlAlias, e);
                // Если показывается контрол (а показываются они все по одному правилу), то показывается и заголовок
                historyBlock.CaptionVisibility = v ?? Visibility.Collapsed;
            }

            if (e.RowModel.Blocks.TryGet(KrConstants.Ui.KrStageSettingsBlockAlias, out var block))
            {
                var stageTypeID = e.Row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);
                var visitor = new VisibilityViaTagsVisitor(e.CardModel.CardType.ID);

                foreach (var control in block.Controls)
                {
                    var stageHandlerDescriptorID = control
                        .CardTypeControl
                        .ControlSettings
                        .TryGet<Guid?>(KrConstants.Ui.StageHandlerDescriptorIDSetting);

                    if (stageHandlerDescriptorID.HasValue)
                    {
                        if (stageTypeID == stageHandlerDescriptorID)
                        {
                            control.ControlVisibility = Visibility.Visible;
                            visitor.Visit(control);
                        }
                        else
                        {
                            control.ControlVisibility = Visibility.Collapsed;
                        }
                    }
                }
            }

            e.RowModel.MainFormWithBlocks.Rearrange();
        }

        private static void SetControlSettings(
            Guid typeID,
            IControlViewModel controlViewModel)
        {
            if (controlViewModel.ControlVisibility != Visibility.Visible)
            {
                // Если контрол скрыт, нет смысла в нем копаться.
                return;
            }

            var controlSettings = controlViewModel.CardTypeControl.ControlSettings;
            if (!controlSettings.TryGetValue(KrConstants.Ui.TagsListSetting, out var tagsObj)
                || tagsObj is not IList tags)
            {
                return;
            }

            var hasRuntime = false;
            var hasDesign = false;
            var hasRuntimeReadonly = false;
            var hasDesignReadonly = false;

            foreach (var tag in tags)
            {
                switch (tag)
                {
                    case "Runtime":
                        hasRuntime = true;
                        break;
                    case "DesignTime":
                        hasDesign = true;
                        break;
                    case "RuntimeReadonly":
                        hasRuntimeReadonly = true;
                        break;
                    case "DesignTimeReadonly":
                        hasDesignReadonly = true;
                        break;
                }
            }

            if (!hasDesign
                && !hasRuntime)
            {
                controlViewModel.ControlVisibility = Visibility.Visible;
            }
            else if (hasDesign
                     && KrProcessSharedHelper.DesignTimeCard(typeID))
            {
                controlViewModel.ControlVisibility = Visibility.Visible;
            }
            else if (hasRuntime
                     && KrProcessSharedHelper.RuntimeCard(typeID))
            {
                controlViewModel.ControlVisibility = Visibility.Visible;
            }
            else
            {
                controlViewModel.ControlVisibility = Visibility.Collapsed;
            }

            if (controlViewModel.ControlVisibility == Visibility.Visible)
            {
                if (hasDesignReadonly
                    && KrProcessSharedHelper.DesignTimeCard(typeID))
                {
                    controlViewModel.IsReadOnly = true;
                }
                else if (hasRuntimeReadonly
                         && KrProcessSharedHelper.RuntimeCard(typeID))
                {
                    controlViewModel.IsReadOnly = true;
                }
            }
        }

        private static void SetBlockControlsSettings(
            Guid typeID,
            IBlockViewModel blockViewModel)
        {
            if (blockViewModel.BlockVisibility != Visibility.Visible)
            {
                // Если блок скрыт, нет смысла в нем копаться.
                return;
            }

            var blockSettings = blockViewModel.CardTypeBlock.BlockSettings;
            if (!blockSettings.TryGetValue(KrConstants.Ui.TagsListSetting, out var tagsObj)
                || tagsObj is not IList tags)
            {
                return;
            }

            var hasRuntime = false;
            var hasDesign = false;

            foreach (var tag in tags)
            {
                switch (tag)
                {
                    case "Runtime":
                        hasRuntime = true;
                        break;
                    case "DesignTime":
                        hasDesign = true;
                        break;
                }
            }

            if (!hasDesign
                && !hasRuntime)
            {
                blockViewModel.BlockVisibility = Visibility.Visible;
            }
            else if (hasDesign
                     && KrProcessSharedHelper.DesignTimeCard(typeID))
            {
                blockViewModel.BlockVisibility = Visibility.Visible;
            }
            else if (hasRuntime
                     && KrProcessSharedHelper.RuntimeCard(typeID))
            {
                blockViewModel.BlockVisibility = Visibility.Visible;
            }
            else
            {
                blockViewModel.BlockVisibility = Visibility.Collapsed;
            }
        }

        private async void BindUIHandlers(
            object? sender,
            GridRowEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                await this.RunHandlersAsync(e, static (h, ctx) => h.Initialize(ctx));
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

        private async void ValidateViaHandlers(
            object? sender,
            GridRowValidationEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                await this.RunHandlersAsync(e, static (h, ctx) => h.Validate(ctx));
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

        private async void UnbindUIHandlers(
            object? sender,
            GridRowEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                await this.RunHandlersAsync(e, static (h, ctx) => h.Finalize(ctx));
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

        private async Task RunHandlersAsync(
            GridRowEventArgs e,
            Func<IStageTypeUIHandler, IKrStageTypeUIHandlerContext, Task> handleAsync)
        {
            if (e.Action == GridRowAction.Deleting)
            {
                return;
            }

            var row = e.Row;
            var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

            if (!stageTypeID.HasValue)
            {
                return;
            }

            var handlers = this.GetStageTypeUIHandlers(stageTypeID.Value);

            if (handlers.Count == 0)
            {
                return;
            }

            var context = new KrStageTypeUIHandlerContext(
                stageTypeID.Value,
                e.Action,
                e.Control,
                row,
                e.RowModel!,
                e.CardModel,
                new ValidationResultBuilder());

            await RunHandlersAsync(
                context,
                handlers,
                handleAsync);

            await TessaDialog.ShowNotEmptyAsync(context.ValidationResult);
        }

        private async Task RunHandlersAsync(
            GridRowValidationEventArgs e,
            Func<IStageTypeUIHandler, IKrStageTypeUIHandlerContext, Task> handleAsync)
        {
            var row = e.Row;
            var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

            if (!stageTypeID.HasValue)
            {
                return;
            }

            var handlers = this.GetStageTypeUIHandlers(stageTypeID.Value);

            if (handlers.Count == 0)
            {
                return;
            }

            var context = new KrStageTypeUIHandlerContext(
                stageTypeID.Value,
                null,
                null,
                row,
                e.RowModel,
                e.CardModel,
                e.ValidationResult,
                CancellationToken.None);

            await RunHandlersAsync(
                context,
                handlers,
                handleAsync);
        }

        private List<IStageTypeUIHandler> GetStageTypeUIHandlers(
            Guid stageTypeID)
        {
            if (!this.handlersCache.TryGetValue(stageTypeID, out var handlers))
            {
                handlers = this.uiContainer.ResolveUIHandlers(stageTypeID);
                this.handlersCache.Add(stageTypeID, handlers);
            }

            return handlers;
        }

        private static async Task RunHandlersAsync(
            KrStageTypeUIHandlerContext context,
            IReadOnlyCollection<IStageTypeUIHandler> handlers,
            Func<IStageTypeUIHandler, IKrStageTypeUIHandlerContext, Task> handleAsync)
        {
            foreach (var handler in handlers)
            {
                try
                {
                    await handleAsync(handler, context);
                }
                catch (OperationCanceledException)
                {
                    // Нельзя выбрасывать исключение.
                    return;
                }
                catch (Exception e)
                {
                    await TessaDialog.ShowExceptionAsync(e);
                }
            }
        }

        private async void FormatRowHandler(
            object? sender,
            GridRowEventArgs e)
        {
            var deferral = e.Defer();
            try
            {
                await this.FormatRowAsync(
                    e.Row,
                    e.CardModel,
                    true);
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

        private async Task FormatRowAsync(
            CardRow row,
            ICardModel cardModel,
            bool withChanges,
            CancellationToken cancellationToken = default)
        {
            var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);
            if (!stageTypeID.HasValue)
            {
                return;
            }

            var formatter = this.formatterContainer.ResolveFormatter(stageTypeID.Value);
            if (formatter is null)
            {
                if (withChanges)
                {
                    row.Fields[KrConstants.KrStages.DisplaySettings] = string.Empty;
                }
                else
                {
                    row[KrConstants.KrStages.DisplaySettings] = string.Empty;
                }

                return;
            }

            var info = new Dictionary<string, object>();
            var ctx = new StageTypeFormatterContext(
                this.session,
                info,
                cardModel.Card,
                row,
                null,
                cancellationToken)
            {
                DisplayTimeLimit = row.TryGet<string>(KrConstants.KrStages.DisplayTimeLimit) ?? string.Empty,
                DisplayParticipants = row.TryGet<string>(KrConstants.KrStages.DisplayParticipants) ?? string.Empty,
                DisplaySettings = row.TryGet<string>(KrConstants.KrStages.DisplaySettings) ?? string.Empty
            };

            await cardModel.ExecuteInContextAsync(
                (ui, ct) => formatter.FormatClientAsync(ctx),
                cancellationToken).ConfigureAwait(false);

            if (withChanges)
            {
                var rowFields = row.Fields;
                rowFields[KrConstants.KrStages.DisplayTimeLimit] = ctx.DisplayTimeLimit;
                rowFields[KrConstants.KrStages.DisplayParticipants] = ctx.DisplayParticipants;
                rowFields[KrConstants.KrStages.DisplaySettings] = ctx.DisplaySettings;
            }
            else
            {
                row[KrConstants.KrStages.DisplayTimeLimit] = ctx.DisplayTimeLimit;
                row[KrConstants.KrStages.DisplayParticipants] = ctx.DisplayParticipants;
                row[KrConstants.KrStages.DisplaySettings] = ctx.DisplaySettings;
            }
        }

        private static void BindTimeFieldsRadio(
            object? sender,
            GridRowEventArgs e) => e.Row.FieldChanged += OnTimeFieldChanged;

        private static void UnbindTimeFieldsRadio(
            object? sender,
            GridRowEventArgs e) => e.Row.FieldChanged -= OnTimeFieldChanged;

        private static void OnTimeFieldChanged(
            object? sender,
            CardFieldChangedEventArgs e)
        {
            if (e.FieldValue is null)
            {
                return;
            }

            switch (e.FieldName)
            {
                case KrConstants.KrStages.Planned:
                    ((CardRow) sender!).Fields[KrConstants.KrStages.TimeLimit] = null;
                    break;
                case KrConstants.KrStages.TimeLimit:
                    ((CardRow) sender!).Fields[KrConstants.KrStages.Planned] = null;
                    break;
            }
        }

        private void ValidateTimeLimit(
            object? sender,
            GridRowValidationEventArgs args)
        {
            var row = args.Row;
            var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);
            var timeLimit = row.TryGet<double?>(KrConstants.KrStages.TimeLimit);
            var planned = row.TryGet<DateTime?>(KrConstants.KrStages.Planned);
            var controls = args.RowModel.Controls;
            var checkTimeLimit = CheckField(KrConstants.Ui.KrTimeLimitInputAlias);
            var checkPlanned = CheckField(KrConstants.Ui.KrPlannedInputAlias);
            if (checkTimeLimit
                && checkPlanned)
            {
                if (timeLimit is null
                    && planned is null)
                {
                    args.ValidationResult.AddWarning(this, "$UI_Error_TimeLimitOrPlannedNotSpecifiedWarn");
                }
            }
            else if (checkTimeLimit
                     && timeLimit is null)
            {
                args.ValidationResult.AddWarning(this, "$UI_Error_TimeLimitNotSpecifiedWarn");
            }
            else if (checkPlanned
                     && planned is null)
            {
                args.ValidationResult.AddWarning(this, "$UI_Error_PlannedNotSpecifiedWarn");
            }

            bool CheckField(string inputAlias)
            {
                return controls.TryGet(inputAlias, out var control)
                    && control.CardTypeControl.ControlSettings.TryGetValue(KrConstants.Ui.VisibleForTypesSetting, out var obj)
                    && obj is IList list
                    && list.Cast<object>().Any(p => p.Equals(stageTypeID));
            }
        }

        private void ValidatePerformers(
            object? sender,
            GridRowValidationEventArgs args)
        {
            var controls = args.RowModel.Controls;
            PerformerUsageMode mode;
            if (controls.TryGet(KrConstants.Ui.KrSinglePerformerEntryAcAlias, out var control)
                && control.ControlVisibility == Visibility.Visible)
            {
                mode = PerformerUsageMode.Single;
            }
            else if (controls.TryGet(KrConstants.Ui.KrMultiplePerformersTableAcAlias, out control)
                     || control.ControlVisibility == Visibility.Visible)
            {
                mode = PerformerUsageMode.Multiple;
            }
            else
            {
                return;
            }

            var controlSettings = control.CardTypeControl.ControlSettings;
            if (!controlSettings.TryGetValue(KrConstants.Ui.RequiredForTypesSetting, out var obj) ||
                obj is not IList list)
            {
                return;
            }

            var row = args.Row;
            var stageTypeID = row.TryGet<Guid?>(KrConstants.KrStages.StageTypeID);

            foreach (Guid id in list)
            {
                if (id == stageTypeID)
                {
                    if (mode == PerformerUsageMode.Single
                        && row[KrConstants.KrSinglePerformerVirtual.PerformerID] is null
                        || mode == PerformerUsageMode.Multiple
                        && args.CardModel.Card.Sections.TryGetValue(KrConstants.KrPerformersVirtual.Synthetic, out var perfSec)
                        && !perfSec.Rows.Any(p => p.Get<Guid>(KrConstants.KrPerformersVirtual.StageRowID) == row.RowID && p.State != CardRowState.Deleted))
                    {
                        args.ValidationResult.AddWarning(this, "$UI_Error_PerformerNotSpecifiedWarn");
                    }

                    break;
                }
            }
        }

        private void FinalizeObjects()
        {
            foreach (var obj in this.finalizedObjects)
            {
                obj.Dispose();
            }

            this.finalizedObjects.Clear();
        }

        private async Task SetTabIndicationAsync(
            ICardModel cardModel,
            string sectionName,
            string controlName,
            CancellationToken cancellationToken = default)
        {
            if (!cardModel.Card.Sections.TryGetValue(sectionName, out var templatesSec))
            {
                return;
            }

            var sectionMeta = (await this.cardMetadata.GetSectionsAsync(cancellationToken))[sectionName];
            var fieldIDs = sectionMeta.Columns.ToDictionary(static k => k.ID, static v => v.Name);

            var tabControl = (TabControlViewModel) cardModel.Controls[controlName];
            var indicator = new TabContentIndicator(tabControl, templatesSec, fieldIDs, true);
            this.finalizedObjects.Add(indicator);
        }

        private async Task SetBlockIndicationAsync(
            ICardModel cardModel,
            string sectionName,
            string controlName,
            CancellationToken cancellationToken = default)
        {
            if (!cardModel.Card.Sections.TryGetValue(sectionName, out var templatesSec))
            {
                return;
            }

            if (!cardModel.Blocks.TryGet(controlName, out var control))
            {
                return;
            }

            var sectionMeta = (await this.cardMetadata.GetSectionsAsync(cancellationToken))[sectionName];
            var fieldIDs = sectionMeta.Columns.ToDictionary(static k => k.ID, static v => v.Name);

            var indicator = new BlockContentIndicator(control, templatesSec, fieldIDs);
            this.finalizedObjects.Add(indicator);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var cardModel = context.Model;

            await this.SetTabIndicationAsync(cardModel, KrConstants.KrStageTemplates.Name, KrConstants.Ui.CSharpSourceTable, context.CancellationToken);
            await this.SetTabIndicationAsync(cardModel, KrConstants.KrStageGroups.Name, KrConstants.Ui.CSharpSourceTableDesign, context.CancellationToken);
            await this.SetTabIndicationAsync(cardModel, KrConstants.KrStageGroups.Name, KrConstants.Ui.CSharpSourceTableRuntime, context.CancellationToken);
            await this.SetBlockIndicationAsync(cardModel, KrConstants.KrSecondaryProcesses.Name, KrConstants.Ui.VisibilityScriptsBlock, context.CancellationToken);
            await this.SetBlockIndicationAsync(cardModel, KrConstants.KrSecondaryProcesses.Name, KrConstants.Ui.ExecutionScriptsBlock, context.CancellationToken);

            // Для шаблона этапов и вторички тоже выполняем расширение без проверки компонентов
            if (!KrProcessSharedHelper.DesignTimeCard(cardModel.CardType.ID))
            {
                var usedComponents = await KrComponentsHelper.GetKrComponentsAsync(cardModel.Card, this.typesCache, context.CancellationToken);
                // Выходим если нет согласования
                if (usedComponents.HasNot(KrComponents.Routes))
                {
                    return;
                }
            }

            if (!cardModel.Controls.TryGet(KrConstants.Ui.KrApprovalStagesControlAlias, out var control) ||
                control is not GridViewModel approvalStagesTable)
            {
                return;
            }

            if (!KrProcessSharedHelper.DesignTimeCard(context.Model.CardType.ID))
            {
                var selectedApprovalStages = approvalStagesTable.SelectedRows;
                approvalStagesTable.LeftButtons.Add(
                    new UIButton("$CardTypes_Buttons_ActivateStage",
                        async _ => ActivateStagesHandler(selectedApprovalStages),
                        () => HasEnableActivateStageButton(cardModel.Card, selectedApprovalStages)));

                approvalStagesTable.ContextMenuGenerators.Add(
                    ctx =>
                    {
                        ctx.MenuActions.Add(
                            new MenuAction(
                                "ActivateStage",
                                "$CardTypes_Buttons_ActivateStage",
                                Icon.Empty,
                                new DelegateCommand(_ => ActivateStagesHandler(selectedApprovalStages)),
                                isEnabled: HasEnableActivateStageButton(cardModel.Card, selectedApprovalStages)));

                        return ValueTask.CompletedTask;
                    });

                foreach (var approvalStageRow in approvalStagesTable.Rows.Where(static i =>
                             i.Model.TryGet<bool>(KrConstants.KrStages.Hidden)
                             || i.Model.TryGet<bool>(KrConstants.KrStages.Skip)))
                {
                    approvalStageRow.Background = KrProcessBrushes.Gainsboro;
                }
            }

            approvalStagesTable.RowAdding += this.AddStageDialogHandlerAsync;
            approvalStagesTable.RowInitializing += SetStageTypeTitle;
            approvalStagesTable.RowInitializing += ShowSettingsBlock;
            approvalStagesTable.RowInvoked += this.BindUIHandlers;
            approvalStagesTable.RowInvoked += BindTimeFieldsRadio;
            approvalStagesTable.RowInvoked += RowInvokedSkipStage;
            approvalStagesTable.RowEditorClosing += this.FormatRowHandler;
            approvalStagesTable.RowValidating += this.ValidateViaHandlers;
            approvalStagesTable.RowValidating += this.ValidateTimeLimit;
            approvalStagesTable.RowValidating += this.ValidatePerformers;
            approvalStagesTable.RowEditorClosed += this.UnbindUIHandlers;
            approvalStagesTable.RowEditorClosed += UnbindTimeFieldsRadio;

            if (context.Card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var stagesSec))
            {
                foreach (var row in stagesSec.Rows)
                {
                    await this.FormatRowAsync(
                        row,
                        cardModel,
                        withChanges: false,
                        cancellationToken: context.CancellationToken);
                }
            }
        }

        /// <inheritdoc/>
        public override async Task Finalized(ICardUIExtensionContext context)
        {
            this.FinalizeObjects();

            if (!KrProcessSharedHelper.DesignTimeCard(context.Model.CardType.ID))
            {
                var usedComponents = await KrComponentsHelper.GetKrComponentsAsync(
                    context.Model.Card,
                    this.typesCache,
                    context.CancellationToken);

                if (usedComponents.HasNot(KrComponents.Routes))
                {
                    return;
                }
            }

            if (context.Model.Controls.TryGet(KrConstants.Ui.KrApprovalStagesControlAlias, out var control)
                && control is GridViewModel approvalStagesTable)
            {
                approvalStagesTable.RowAdding -= this.AddStageDialogHandlerAsync;
                approvalStagesTable.RowInitializing -= SetStageTypeTitle;
                approvalStagesTable.RowInitializing -= ShowSettingsBlock;
                approvalStagesTable.RowInvoked -= this.BindUIHandlers;
                approvalStagesTable.RowInvoked -= BindTimeFieldsRadio;
                approvalStagesTable.RowInvoked -= RowInvokedSkipStage;
                approvalStagesTable.RowEditorClosing -= this.FormatRowHandler;
                approvalStagesTable.RowValidating -= this.ValidateViaHandlers;
                approvalStagesTable.RowValidating -= this.ValidateTimeLimit;
                approvalStagesTable.RowValidating -= this.ValidatePerformers;
                approvalStagesTable.RowEditorClosed -= this.UnbindUIHandlers;
                approvalStagesTable.RowEditorClosed -= UnbindTimeFieldsRadio;
            }
        }

        /// <summary>
        /// Возвращает значение, показывающее, можно ли активировать выбранные пропущенные этапы.
        /// </summary>
        /// <param name="card">Карточка, содержащая контрол с выбранными этапами.</param>
        /// <param name="selectedRows">Коллекция выбранных строк.</param>
        /// <returns>Значение, показывающее, можно ли активировать выбранные пропущенные этапы.</returns>
        private static bool HasEnableActivateStageButton(Card card, IList<CardRowViewModel> selectedRows)
        {
            // Нет строк для обработки?
            if (selectedRows.Count == 0)
            {
                return false;
            }

            // Разрешено редактировать маршрут?
            var isEditRoute = KrToken.TryGet(card.Info) is { } krToken
                && krToken.HasPermission(KrPermissionFlagDescriptors.EditRoute);

            // Разрешено активировать пропущенные неактивные этапы,
            // если они были изменены до сохранения карточки
            // или если есть разрешение на редактирования маршрута.
            return selectedRows.All(i =>
                i.Model.Get<int>(KrConstants.KrStages.StateID) == KrStageState.Inactive.ID
                && i.Model.TryGet<bool>(KrConstants.KrStages.Skip)
                && (isEditRoute || i.Model.IsChanged(KrConstants.KrStages.Skip)));
        }

        /// <summary>
        /// Активирует указанные этапы.
        /// </summary>
        /// <param name="selectedRowStages">Коллекция строк - этапов, которые необходимо активировать.</param>
        private static void ActivateStagesHandler(IList<CardRowViewModel> selectedRowStages)
        {
            foreach (var selectedRowStage in selectedRowStages)
            {
                selectedRowStage.Model.Fields[KrConstants.KrStages.Skip] = BooleanBoxes.False;
                selectedRowStage.Background = KrProcessBrushes.Transparent;
            }
        }

        /// <summary>
        /// Обрабатывает пропуск этапа.
        /// </summary>
        /// <param name="sender">Источник событий.</param>
        /// <param name="e">Аргументы события.</param>
        private static void RowInvokedSkipStage(
            object? sender,
            GridRowEventArgs e)
        {
            if (e.Action == GridRowAction.Deleting
                && KrProcessSharedHelper.SkipStage(e.Row))
            {
                var currentRowViewModel = e.Control.SelectedRows
                    .FirstOrDefault(i => i.Model == e.Row);

                if (currentRowViewModel is not null)
                {
                    currentRowViewModel.Background = KrProcessBrushes.Gainsboro;
                }

                e.Cancel = true;
            }
        }

        #endregion
    }
}
