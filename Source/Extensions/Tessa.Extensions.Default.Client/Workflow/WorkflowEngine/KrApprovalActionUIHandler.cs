using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Controls.AutoComplete;
using Tessa.UI.Cards.Controls.Workflow;
using Tessa.UI.Controls.AutoCompleteCtrl;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Views;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Storage;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrApprovalDescriptor"/>.
    /// </summary>
    public sealed class KrApprovalActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Fields

        private readonly IViewService viewService;
        private readonly ICardMetadata cardMetadata;
        private readonly IActionCompletionOptionsProvider completionOptionsActionsProvider;
        private readonly TableRowContentIndicator[] tableIndicators = new TableRowContentIndicator[3];

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrApprovalActionUIHandler"/>.
        /// </summary>
        /// <param name="viewService">Сервис представлений.</param>
        /// <param name="cardMetadata">Репозиторий с метаинформацией.</param>
        /// <param name="completionOptionsActionsProvider">Объект предоставляющий доступ к вариантам завершения действий.</param>
        public KrApprovalActionUIHandler(
            IViewService viewService,
            ICardMetadata cardMetadata,
            IActionCompletionOptionsProvider completionOptionsActionsProvider)
            : base(KrDescriptors.KrApprovalDescriptor)
        {
            this.viewService = viewService ?? throw new ArgumentNullException(nameof(viewService));
            this.cardMetadata = cardMetadata ?? throw new ArgumentNullException(nameof(cardMetadata));
            this.completionOptionsActionsProvider = completionOptionsActionsProvider ?? throw new ArgumentNullException(nameof(completionOptionsActionsProvider));
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        protected override async Task AttachToCardCoreAsync(WorkflowEngineBindingContext bindingContext)
        {
            if (bindingContext.ActionTemplate is not null)
            {
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrApprovalActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrApprovalActionVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrApprovalActionVirtual.InitTaskScript,
                    typeof(string),
                    KrApprovalActionVirtual.SectionName,
                    KrApprovalActionVirtual.InitTaskScript);

                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrWeEditInterjectOptionsVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrWeEditInterjectOptionsVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrWeEditInterjectOptionsVirtual.InitTaskScript,
                    typeof(string),
                    KrWeEditInterjectOptionsVirtual.SectionName,
                    KrWeEditInterjectOptionsVirtual.InitTaskScript);

                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrWeAdditionalApprovalOptionsVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrWeAdditionalApprovalOptionsVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrWeAdditionalApprovalOptionsVirtual.InitTaskScript,
                    typeof(string),
                    KrWeAdditionalApprovalOptionsVirtual.SectionName,
                    KrWeAdditionalApprovalOptionsVirtual.InitTaskScript);

                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrWeRequestCommentOptionsVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrWeRequestCommentOptionsVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrWeRequestCommentOptionsVirtual.InitTaskScript,
                    typeof(string),
                    KrWeRequestCommentOptionsVirtual.SectionName,
                    KrWeRequestCommentOptionsVirtual.InitTaskScript);
            }

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrApprovalActionVirtual.SectionName,
                KrApprovalActionVirtual.SectionName);

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.SectionName);

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrWeAdditionalApprovalOptionsVirtual.SectionName,
                KrWeAdditionalApprovalOptionsVirtual.SectionName);

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrWeRequestCommentOptionsVirtual.SectionName,
                KrWeRequestCommentOptionsVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrApprovalActionOptionsVirtual.SectionName,
                KrApprovalActionOptionsVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrApprovalActionOptionsActionVirtual.SectionName,
                KrApprovalActionOptionsActionVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrApprovalActionOptionLinksVirtual.SectionName,
                KrApprovalActionOptionLinksVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrWeRolesVirtual.SectionName,
                KrWeRolesVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrApprovalActionAdditionalPerformersVirtual.SectionName,
                KrApprovalActionAdditionalPerformersVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrApprovalActionNotificationRolesVirtual.SectionName,
                KrApprovalActionNotificationRolesVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrApprovalActionNotificationActionRolesVirtual.SectionName,
                KrApprovalActionNotificationActionRolesVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                WorkflowTaskActionBase.EventsSectionName,
                WorkflowTaskActionBase.EventsSectionName);

            this.AddLinkBinding(
                bindingContext,
                new[] { KrApprovalActionOptionLinksVirtual.Link, Names.Table_ID },
                KrApprovalActionOptionLinksVirtual.SectionName);

            // Исправление старых списков согласующих.
            // Добавление порядкового номера.

            var roles = bindingContext.Card.Sections[KrWeRolesVirtual.SectionName].TryGetRows();

            if (roles is not null)
            {
                for (var i = 0; i < roles.Count; i++)
                {
                    var row = roles[i];
                    if (row.TryGet<int?>(KrWeRolesVirtual.Order).HasValue)
                    {
                        continue;
                    }

                    row.Fields[KrWeRolesVirtual.Order] = Int32Boxes.Box(i);
                } 
            }
        }

        /// <inheritdoc />
        protected override async Task UpdateFormCoreAsync(
            WorkflowStorageBase action,
            WorkflowStorageBase node,
            WorkflowStorageBase process,
            ICardModel cardModel,
            WorkflowActionStorage actionTemplate = null,
            CancellationToken cancellationToken = default)
        {
            var card = cardModel.Card;
            var isReadOnly = card.Permissions.CardPermissions.Has(CardPermissionFlags.ProhibitModify);
            var performersSection = card.Sections[KrWeRolesVirtual.SectionName];
            var additionalPerformersSection = card.Sections[KrApprovalActionAdditionalPerformersVirtual.SectionName];
            var additionalPerformersSettingsSection = card.Sections[KrApprovalActionAdditionalPerformersSettingsVirtual.SectionName];
            var additionalPerformersDisplayInfoSection = card.Sections[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.SectionName];

            var addComputedRoleLink = (HyperlinkViewModel) cardModel.Controls[WorkflowConstants.UI.AddComputedRoleLink];
            var performers = (AutoCompleteTableViewModel) ((ParametrizedControlViewModel) cardModel.Controls[WorkflowConstants.UI.Performers]).InnerViewModel;

            // Возвращает текущего выбранного согласующего.
            CardRow GetSelectedMainApproverRow()
            {
                return ((RowAutoCompleteItem) performers.ItemsSource.SelectedItem)?.Row;
            }

            if (!isReadOnly)
            {
                var mainSection = card.Sections[KrApprovalActionVirtual.SectionName];
                mainSection.FieldChanged += this.MainSection_FieldChanged;

                var editInterjectOptionsSection = card.Sections[KrWeEditInterjectOptionsVirtual.SectionName];
                editInterjectOptionsSection.FieldChanged += this.EditInterjectOptions_FieldChanged;

                additionalPerformersSettingsSection.FieldChanged += (_, e) =>
                {
                    switch (e.FieldName)
                    {
                        // Изменение состояния флага IsResponsible у первого доп. согласующего.
                        case KrApprovalActionAdditionalPerformersSettingsVirtual.IsAdditionalApprovalFirstResponsible when e.FieldValue is bool isAdditionalApprovalFirstResponsible:
                            var filteredAdditionalPerformerRows = GetAdditionalPerformerRows(
                                additionalPerformersSection.Rows,
                                GetSelectedMainApproverRow()?.RowID).ToArray();

                            if (filteredAdditionalPerformerRows.Any())
                            {
                                var minOrder = filteredAdditionalPerformerRows.Min(static i => i.Fields.Get<int>(KrApprovalActionAdditionalPerformersVirtual.Order));
                                filteredAdditionalPerformerRows.First(i => i.Fields.Get<int>(KrApprovalActionAdditionalPerformersVirtual.Order) == minOrder).Fields[KrApprovalActionAdditionalPerformersVirtual.IsResponsible] = BooleanBoxes.Box(isAdditionalApprovalFirstResponsible);
                            }
                            break;
                    }
                };

                var additionalApprovalContainer = (ContainerViewModel) cardModel.Controls[WorkflowConstants.UI.AdditionalApprovalContainer];
                var additionalPerformersBlock = additionalApprovalContainer.Form.Blocks.Single(static i => string.Equals(i.Name, WorkflowConstants.UI.AdditionalApprovalBlock, StringComparison.Ordinal));
                var additionalPerformers = (AutoCompleteTableViewModel) additionalPerformersBlock.Controls.Single(static i => string.Equals(i.Name, WorkflowConstants.UI.AdditionalApprovers, StringComparison.Ordinal));

                additionalPerformers.ValueSelected += (_, e) =>
                {
                    if (e.Row.State == CardRowState.Inserted)
                    {
                        // Добавление информации о новом доп. согласующем в секцию используемую для хранения.
                        var newOrder = Int32Boxes.Box(additionalPerformersSection.Rows.Count);

                        var newRow = additionalPerformersSection.Rows.Add();
                        var mainApproverRow = GetSelectedMainApproverRow();

                        if (mainApproverRow is null)
                        {
                            return;
                        }

                        var mainApproverRowFields = mainApproverRow.Fields;

                        newRow.Fields[Names.Table_RowID] = e.Row.Fields[Names.Table_RowID];
                        newRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.MainApproverRowID] = mainApproverRowFields[Names.Table_RowID];
                        newRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.RoleID] = e.Row.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.RoleID];
                        newRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.RoleName] = e.Row.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.RoleName];
                        newRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.IsResponsible] = e.Row.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.IsResponsible];
                        newRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.Order] = newOrder;
                        newRow.State = CardRowState.Inserted;

                        if (mainApproverRowFields.TryGetValue(KrWeRolesVirtual.Role + Table_Field_Name, out var mainApproverNameObj)
                            && mainApproverNameObj is string mainApproverName
                            && KrAdditionalApprovalMarker.TryMark(mainApproverName, out mainApproverName))
                        {
                            AutocompleteSetAndUpdateDisplayValue(
                                mainApproverRow,
                                KrWeRolesVirtual.Role + Table_Field_Name,
                                mainApproverName);
                        }
                    }
                };

                additionalPerformers.ValueDeleted += (_, e) =>
                {
                    var removedRowID = e.Row.RowID;
                    var removedAdditionalPerformerIndex = additionalPerformersSection.Rows.IndexOf(i => i.RowID == removedRowID);
                    var removedAdditionalPerformerIsResponsible = additionalPerformersSection.Rows[removedAdditionalPerformerIndex].Fields.Get<bool>(KrApprovalActionAdditionalPerformersVirtual.IsResponsible);
                    additionalPerformersSection.Rows.RemoveAt(removedAdditionalPerformerIndex);

                    var selectedMainApproverRow = GetSelectedMainApproverRow();

                    if (selectedMainApproverRow is null)
                    {
                        return;
                    }

                    var selectedMainApproverRowID = selectedMainApproverRow.RowID;

                    if (GetAdditionalPerformerRows(additionalPerformersSection.Rows, selectedMainApproverRowID).Any())
                    {
                        var orderedAdditionalPerformers = additionalPerformersSection.Rows.OrderBy(p => p.Fields.Get<int>(KrApprovalActionAdditionalPerformersVirtual.Order)).ToArray();

                        // Восстановление порядка строк.
                        for (var i = 0; i < orderedAdditionalPerformers.Length; i++)
                        {
                            orderedAdditionalPerformers[i].Fields[KrApprovalActionAdditionalPerformersVirtual.Order] = Int32Boxes.Box(i);
                        }

                        // Перенос флага IsResponsible, при удалении первого ответственного на второго, если он есть.
                        if (removedAdditionalPerformerIsResponsible)
                        {
                            var firstRow = GetAdditionalPerformerRows(
                                orderedAdditionalPerformers,
                                selectedMainApproverRowID).First();

                            firstRow.Fields[KrApprovalActionAdditionalPerformersVirtual.IsResponsible] = BooleanBoxes.True;
                        }
                    }
                    else
                    {
                        additionalPerformersSettingsSection.Fields[KrApprovalActionAdditionalPerformersSettingsVirtual.IsAdditionalApprovalFirstResponsible] = BooleanBoxes.False;

                        if (selectedMainApproverRow.Fields.TryGetValue(KrWeRolesVirtual.Role + Table_Field_Name, out var mainApproverNameObj)
                            && mainApproverNameObj is string mainApproverName
                            && KrAdditionalApprovalMarker.TryUnmark(mainApproverName, out mainApproverName))
                        {
                            AutocompleteSetAndUpdateDisplayValue(
                               selectedMainApproverRow,
                               KrWeRolesVirtual.Role + Table_Field_Name,
                               mainApproverName);
                        }
                    }
                };

                ((INotifyPropertyChanged) performers.ItemsSource).PropertyChanged += (_, e) =>
                 {
                     // Обработка изменения текущего выбранного согласующего.
                     if (string.Equals(e.PropertyName, nameof(IAutoCompleteDataSource.SelectedItem), StringComparison.Ordinal))
                     {
                         var isSelectedPerformer = GetSelectedMainApproverRow() is not null;
                         SelectedPerformerChanged(isSelectedPerformer);
                         if (ChangeVisibility(
                             additionalPerformersBlock,
                             isSelectedPerformer
                             ? Visibility.Visible
                             : Visibility.Collapsed))
                         {
                             cardModel.Forms[0].RearrangeChildren();
                         }
                     }
                 };

                performersSection.Rows.ItemChanged += (_, e) =>
                {
                    switch (e.Action)
                    {
                        case ListStorageAction.Remove:
                            if (IsComputedRole(e.Item))
                            {
                                ChangeVisibility(addComputedRoleLink, Visibility.Visible);
                            }
                            if (GetSelectedMainApproverRow() == e.Item)
                            {
                                performers.ItemsSource.SelectedItem = null;
                            }

                            RemoveAdditionalPerformers(e.Item.RowID);
                            break;
                        case ListStorageAction.Clear:
                            addComputedRoleLink.ControlVisibility = Visibility.Visible;
                            performers.ItemsSource.SelectedItem = null;

                            RemoveAdditionalPerformers(null);
                            break;
                    }

                    void RemoveAdditionalPerformers(
                        Guid? mainReformerRowID)
                    {
                        var additionalPerformers = additionalPerformersSection.TryGetRows();

                        if (additionalPerformers is null)
                        {
                            return;
                        }

                        for (var i = additionalPerformers.Count - 1; i >= 0; i--)
                        {
                            var row = additionalPerformers[i];
                            var rowState = row.State;

                            if (rowState == CardRowState.Deleted
                                || mainReformerRowID.HasValue
                                && row.Get<Guid>(KrApprovalActionAdditionalPerformersVirtual.MainApprover + Names.Table_RowID) != mainReformerRowID)
                            {
                                continue;
                            }

                            if (rowState == CardRowState.Inserted)
                            {
                                additionalPerformers.RemoveAt(i);
                            }
                            else
                            {
                                row.State = CardRowState.Deleted;
                            }
                        }
                    }
                };

                addComputedRoleLink.CommandClosure.Execute = _ =>
                {
                    var rows = performersSection.Rows;
                    var newRow = rows.Add();
                    newRow.RowID = Guid.NewGuid();
                    newRow.State = CardRowState.None;
                    newRow[KrWeRolesVirtual.Role + Names.Table_ID] = KrConstants.SqlApproverRoleID;
                    newRow[KrWeRolesVirtual.Role + Table_Field_Name] = KrConstants.SqlApproverRoleName;
                    newRow[KrWeRolesVirtual.Order] = Int32Boxes.Box(rows.Count - 1);
                    newRow.State = CardRowState.Inserted;

                    ChangeVisibility(
                        addComputedRoleLink,
                        Visibility.Collapsed);
                };

                var validationResult = new ValidationResultBuilder();
                await WorkflowHelper.InializeTaskCompletionOptionsAsync(
                    this.cardMetadata,
                    card.Sections[KrApprovalActionOptionsVirtual.SectionName].Rows,
                    cardModel.CreateEmptyRow(KrApprovalActionOptionsVirtual.SectionName),
                    new[]
                    {
                        DefaultTaskTypes.KrApproveTypeID,
                        DefaultTaskTypes.KrAdditionalApprovalTypeID,
                        DefaultTaskTypes.KrRequestCommentTypeID,
                        DefaultTaskTypes.KrEditInterjectTypeID,
                    },
                    validationResult,
                    this,
                    isSetTaskTypeInfo: true,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                await TessaDialog.ShowNotEmptyAsync(validationResult).ConfigureAwait(false);

                var actionCompletionOptions = await this.completionOptionsActionsProvider.GetActionCompletionOptionsAsync(cancellationToken).ConfigureAwait(false);
                WorkflowHelper.InializeActionCompletionOptions(
                    actionCompletionOptions,
                    card.Sections[KrApprovalActionOptionsActionVirtual.SectionName].Rows,
                    cardModel.CreateEmptyRow(KrApprovalActionOptionsActionVirtual.SectionName),
                    new[]
                    {
                        ActionCompletionOptions.Approved,
                        ActionCompletionOptions.Disapproved
                    });
            }

            ChangeVisibility(
                addComputedRoleLink,
                performersSection.Rows.Any(IsComputedRole) ? Visibility.Collapsed : Visibility.Visible);

            this.tableIndicators[0] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
                cardModel,
                this.cardMetadata,
                WorkflowConstants.UI.ActionCompletionOptionsTable,
                exceptSectionNames: ExceptSectionNames,
                exceptSectionFieldNames: new Dictionary<string, ICollection<string>>(StringComparer.Ordinal)
                {
                    {
                        KrApprovalActionOptionsActionVirtual.SectionName,
                        ExceptSectionFieldNames
                    }
                },
                cancellationToken: cancellationToken).ConfigureAwait(false);

            this.tableIndicators[1] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
                cardModel,
                this.cardMetadata,
                WorkflowConstants.UI.CompletionOptionsTable,
                exceptSectionFieldNames: new Dictionary<string, ICollection<string>>(StringComparer.Ordinal)
                {
                    {
                        KrApprovalActionOptionsVirtual.SectionName,
                        ExceptSectionFieldNames
                    }
                },
                cancellationToken: cancellationToken).ConfigureAwait(false);

            this.tableIndicators[2] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
                cardModel,
                this.cardMetadata,
                WorkflowConstants.UI.ActionEventsTable,
                exceptSectionFieldNames: new Dictionary<string, ICollection<string>>(StringComparer.Ordinal)
                {
                    {
                        WorkflowTaskActionBase.EventsSectionName,
                        ExceptSectionFieldNames
                    }
                },
                cancellationToken: cancellationToken).ConfigureAwait(false);

            void SelectedPerformerChanged(bool isSelectedPerformer)
            {
                if (!isSelectedPerformer)
                {
                    return;
                }

                if (additionalPerformersDisplayInfoSection.Rows.Any())
                {
                    additionalPerformersDisplayInfoSection.Rows.Clear();
                }

                var currentMainApproverRowID = GetSelectedMainApproverRow()?.RowID;
                var orderredAdditionalPerformerRows = GetAdditionalPerformerRows(additionalPerformersSection.Rows, currentMainApproverRowID)
                    .OrderBy(static i => i.Fields.Get<int>(KrApprovalActionAdditionalPerformersVirtual.Order))
                    .ToArray();

                int rowIndex = default;

                static void CreateDisplayInfo(
                    CardRow targetRow,
                    CardRow sourceRow,
                    int order)
                {
                    targetRow.Fields[Names.Table_RowID] = sourceRow[Names.Table_RowID];
                    targetRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.RoleID] = sourceRow[KrApprovalActionAdditionalPerformersVirtual.Role + Names.Table_ID];
                    targetRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.RoleName] = sourceRow[KrApprovalActionAdditionalPerformersVirtual.RoleName];
                    targetRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.MainApproverRowID] = sourceRow[KrApprovalActionAdditionalPerformersVirtual.MainApprover + Names.Table_RowID];
                    targetRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.IsResponsible] = sourceRow[KrApprovalActionAdditionalPerformersVirtual.IsResponsible];
                    targetRow.Fields[KrApprovalActionAdditionalPerformersDisplayInfoVirtual.Order] = Int32Boxes.Box(order);
                    targetRow.State = CardRowState.Inserted;
                }

                bool isAdditionalApprovalFirstResponsible = default;
                if (orderredAdditionalPerformerRows.Any())
                {
                    var firstRow = orderredAdditionalPerformerRows[rowIndex];
                    isAdditionalApprovalFirstResponsible = firstRow.Fields.Get<bool>(KrApprovalActionAdditionalPerformersVirtual.IsResponsible);
                    CreateDisplayInfo(
                        additionalPerformersDisplayInfoSection.Rows.Add(),
                        firstRow,
                        rowIndex);
                    rowIndex++;
                }
                additionalPerformersSettingsSection.Fields[KrApprovalActionAdditionalPerformersSettingsVirtual.IsAdditionalApprovalFirstResponsible] = BooleanBoxes.Box(isAdditionalApprovalFirstResponsible);

                for (; rowIndex < orderredAdditionalPerformerRows.Length; rowIndex++)
                {
                    CreateDisplayInfo(
                        additionalPerformersDisplayInfoSection.Rows.Add(),
                        orderredAdditionalPerformerRows[rowIndex],
                        rowIndex);
                }
            }
        }

        /// <inheritdoc/>
        protected override async Task InvalidateFormCore(
            ICardModel cardModel,
            CancellationToken cancellationToken = default)
        {
            await base.InvalidateFormCore(cardModel, cancellationToken);

            if (cardModel is null)
            {
                return;
            }

            var card = cardModel.Card;
            var mainSection = card.Sections[KrApprovalActionVirtual.SectionName];
            mainSection.FieldChanged -= this.MainSection_FieldChanged;

            var editInterjectOptionsSection = card.Sections[KrWeEditInterjectOptionsVirtual.SectionName];
            editInterjectOptionsSection.FieldChanged -= this.EditInterjectOptions_FieldChanged;

            foreach (var tableIndicator in this.tableIndicators)
            {
                tableIndicator.StopTracking();
            }
        }

        #endregion

        #region Private methods

        private void MainSection_FieldChanged(object sender, CardFieldChangedEventArgs e)
        {
            var mainSectionInner = (CardSection) sender;
            switch (e.FieldName)
            {
                case KrApprovalActionVirtual.Period when e.FieldValue is not null:
                    mainSectionInner.Fields[KrApprovalActionVirtual.Planned] = default;
                    break;
                case KrApprovalActionVirtual.Planned when e.FieldValue is not null:
                    mainSectionInner.Fields[KrApprovalActionVirtual.Period] = default;
                    break;
                case KrApprovalActionVirtual.IsAdvisory when e.FieldValue is bool isAdvisory:
                    if (isAdvisory)
                    {
                        if (!mainSectionInner.Fields.TryGet<Guid?>(Names.Table_ID).HasValue)
                        {
                            var kindID = KrConstants.AdvisoryTaskKindID;
                            var (isFound, kindCaption) = WorkflowHelper.GetKindAsync(this.viewService, kindID).GetAwaiter().GetResult();
                            if (isFound)
                            {
                                mainSectionInner.Fields[Names.Table_ID] = kindID;
                                mainSectionInner.Fields[KrApprovalActionVirtual.Kind + Table_Field_Caption] = kindCaption;
                            }
                        }
                    }
                    else
                    {
                        if (mainSectionInner.Fields.TryGet<Guid?>(Names.Table_ID) == KrConstants.AdvisoryTaskKindID)
                        {
                            mainSectionInner.Fields[Names.Table_ID] = default;
                            mainSectionInner.Fields[KrApprovalActionVirtual.Kind + Table_Field_Caption] = default;
                        }
                    }
                    break;
            }
        }

        private void EditInterjectOptions_FieldChanged(object sender, CardFieldChangedEventArgs e)
        {
            var mainSectionInner = (CardSection) sender;
            switch (e.FieldName)
            {
                case KrWeEditInterjectOptionsVirtual.Period when e.FieldValue is not null:
                    mainSectionInner.Fields[KrWeEditInterjectOptionsVirtual.Planned] = default;
                    break;
                case KrWeEditInterjectOptionsVirtual.Planned when e.FieldValue is not null:
                    mainSectionInner.Fields[KrWeEditInterjectOptionsVirtual.Period] = default;
                    break;
            }
        }

        private static bool ChangeVisibility(
            IControlViewModel control,
            Visibility visibility)
        {
            if (control.ControlVisibility != visibility)
            {
                control.ControlVisibility = visibility;
                return true;
            }

            return false;
        }

        private static bool ChangeVisibility(
            IBlockViewModel control,
            Visibility visibility)
        {
            if (control.BlockVisibility != visibility)
            {
                control.BlockVisibility = visibility;
                return true;
            }

            return false;
        }

        private static bool IsComputedRole(CardRow row)
            => (CardRowState?) row.TryGet<int?>(CardRow.SystemStateKey) != CardRowState.Deleted
                && row.TryGet<Guid?>(KrWeRolesVirtual.Role + Names.Table_ID) == KrConstants.SqlApproverRoleID;

        private static void AutocompleteSetAndUpdateDisplayValue(
            CardRow row,
            string fieldName,
            object value)
        {
            row.State = CardRowState.Deleted;
            row.Fields[fieldName] = value;
            row.State = CardRowState.Inserted;
        }

        /// <summary>
        /// Возвращает коллекцию строк содержащих информацию по доп. согласующим отфильтрованную по указанному согласующему.
        /// </summary>
        /// <param name="additionalPerformersRows">Коллекция строк содержащих информацию по доп. согласующим.</param>
        /// <param name="mainApproverRowID">Идентификатор согласующего по которому выполняется фильтрация.</param>
        /// <returns>Отфильтрованная коллекция строк содержащих информацию по доп. согласующим.</returns>
        private static IEnumerable<CardRow> GetAdditionalPerformerRows(
            IEnumerable<CardRow> additionalPerformersRows,
            Guid? mainApproverRowID)
        {
            return mainApproverRowID.HasValue
                ? additionalPerformersRows.Where(
                    i => i.State != CardRowState.Deleted
                    && i.Fields.Get<Guid>(KrApprovalActionAdditionalPerformersVirtual.MainApprover + Names.Table_RowID) == mainApproverRowID)
                : Enumerable.Empty<CardRow>();
        }

        #endregion
    }
}
