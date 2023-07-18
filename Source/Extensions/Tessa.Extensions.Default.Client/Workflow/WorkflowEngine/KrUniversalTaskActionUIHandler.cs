using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Localization;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Storage;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrUniversalTaskDescriptor"/>.
    /// </summary>
    public sealed class KrUniversalTaskActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Fields

        private static readonly ImmutableHashSet<string> exceptSectionFieldNames =
            ExceptSectionFieldNames.Union(
                new string[]
                {
                    KrUniversalTaskActionButtonsVirtual.Caption,
                    KrUniversalTaskActionButtonsVirtual.IsShowComment,
                    KrUniversalTaskActionButtonsVirtual.IsAdditionalOption,
                    KrUniversalTaskActionButtonsVirtual.Digest,
                    KrUniversalTaskActionButtonsVirtual.OptionID,
                });

        private readonly ICardMetadata cardMetadata;
        private readonly TableRowContentIndicator[] tableIndicators = new TableRowContentIndicator[2];

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrUniversalTaskActionUIHandler"/>.
        /// </summary>
        /// <param name="cardMetadata">Репозиторий с метаинформацией.</param>
        public KrUniversalTaskActionUIHandler(
            ICardMetadata cardMetadata)
            : base(KrDescriptors.KrUniversalTaskDescriptor)
        {
            this.cardMetadata = cardMetadata;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        protected override async Task AttachToCardCoreAsync(WorkflowEngineBindingContext bindingContext)
        {
            if (bindingContext.ActionTemplate != null)
            {
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrUniversalTaskActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrUniversalTaskActionVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrUniversalTaskActionVirtual.InitTaskScript,
                    typeof(string),
                    KrUniversalTaskActionVirtual.SectionName,
                    KrUniversalTaskActionVirtual.InitTaskScript);
            }

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrUniversalTaskActionVirtual.SectionName,
                KrUniversalTaskActionVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrUniversalTaskActionButtonsVirtual.SectionName,
                KrUniversalTaskActionButtonsVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrUniversalTaskActionButtonLinksVirtual.SectionName,
                KrUniversalTaskActionButtonLinksVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrUniversalTaskActionNotificationRolesVitrual.SectionName,
                KrUniversalTaskActionNotificationRolesVitrual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                WorkflowTaskActionBase.EventsSectionName,
                WorkflowTaskActionBase.EventsSectionName);

            this.AddLinkBinding(
                bindingContext,
                new[] { KrUniversalTaskActionButtonLinksVirtual.Link, Names.Table_ID },
                KrUniversalTaskActionButtonLinksVirtual.SectionName);
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
            var mainSection = card.Sections[KrUniversalTaskActionVirtual.SectionName];

            mainSection.FieldChanged += this.MainSection_FieldChanged;

            var completionOptionsTable = (GridViewModel) cardModel.Controls[WorkflowConstants.UI.CompletionOptionsTable];
            completionOptionsTable.RowInvoked += this.CompletionOptionsTable_RowInvoked;
            completionOptionsTable.RowEditorClosing += this.CompletionOptionsTable_RowEditorClosing;

            this.tableIndicators[0] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
                cardModel,
                this.cardMetadata,
                WorkflowConstants.UI.CompletionOptionsTable,
                exceptSectionNames: ExceptSectionNames,
                exceptSectionFieldNames: new Dictionary<string, ICollection<string>>(StringComparer.Ordinal)
                {
                    {
                        KrUniversalTaskActionButtonsVirtual.SectionName,
                        exceptSectionFieldNames
                    }
                },
                cancellationToken: cancellationToken).ConfigureAwait(false);

            this.tableIndicators[1] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
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
        }

        /// <inheritdoc/>
        protected override async Task InvalidateFormCore(ICardModel cardModel, CancellationToken cancellationToken = default)
        {
            await base.InvalidateFormCore(cardModel, cancellationToken);

            if (cardModel is null)
            {
                return;
            }

            var card = cardModel.Card;
            var mainSection = card.Sections[KrUniversalTaskActionVirtual.SectionName];
            mainSection.FieldChanged -= this.MainSection_FieldChanged;

            var completionOptionsTable = (GridViewModel) cardModel.Controls[WorkflowConstants.UI.CompletionOptionsTable];
            completionOptionsTable.RowInvoked -= this.CompletionOptionsTable_RowInvoked;
            completionOptionsTable.RowEditorClosing -= this.CompletionOptionsTable_RowEditorClosing;

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
                case KrUniversalTaskActionVirtual.Period when e.FieldValue is not null:
                    mainSectionInner.Fields[KrUniversalTaskActionVirtual.Planned] = default;
                    break;
                case KrUniversalTaskActionVirtual.Planned when e.FieldValue is not null:
                    mainSectionInner.Fields[KrUniversalTaskActionVirtual.Period] = default;
                    break;
            }
        }

        /// <summary>
        /// Обработчик события <see cref="GridViewModel.RowEditorClosing"/>. Проверяет корректность заполнения параметров вариантов завершения действия.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void CompletionOptionsTable_RowEditorClosing(object sender, GridRowEventArgs e)
        {
            var row = e.Row;
            IValidationResultBuilder validationResult = null;

            var optionID = row.TryGet<Guid?>(KrUniversalTaskActionButtonsVirtual.OptionID);

            if (optionID.HasValue)
            {
                var rows = e.CardModel.Card.Sections[KrUniversalTaskActionButtonsVirtual.SectionName].Rows;

                if (CheckDuplicatesOptionID(rows, row.RowID, optionID.Value))
                {
                    validationResult ??= new ValidationResultBuilder();
                    validationResult.AddError(
                        this,
                        LocalizationManager.Format("$KrActions_UniversalTask_CompletionOptionIDNotUnique"));
                    e.Cancel = true;
                }
            }
            else
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(
                    this,
                    LocalizationManager.Format("$KrActions_UniversalTask_CompletionOptionIDEmpty"));
                e.Cancel = true;
            }

            if (string.IsNullOrEmpty(row.TryGet<string>(KrUniversalTaskActionButtonsVirtual.Caption)))
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(
                    this,
                    LocalizationManager.Format("$KrActions_UniversalTask_CompletionOptionCaptionEmpty"));
                e.Cancel = true;
            }


            if (validationResult is not null)
            {
                TessaDialog.ShowNotEmpty(validationResult);
            }
        }

        private static bool CheckDuplicatesOptionID(ListStorage<CardRow> rows, Guid rowID, Guid optionID)
        {
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i];

                if (row.RowID == rowID
                    || row.State == CardRowState.Deleted)
                {
                    continue;
                }

                var iOptionID = row.Fields.TryGet<Guid?>(KrUniversalTaskActionButtonsVirtual.OptionID);

                if (!iOptionID.HasValue)
                {
                    continue;
                }

                if (optionID == iOptionID)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Обработчик события <see cref="GridViewModel.RowInvoked"/>. Выполняет инициализацию строки содержащую параметры настраиваемого варианта завершения.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void CompletionOptionsTable_RowInvoked(object sender, GridRowEventArgs e)
        {
            if (e.Action == GridRowAction.Inserted)
            {
                e.Row.Fields[KrUniversalTaskActionButtonsVirtual.OptionID] = Guid.NewGuid();
            }
        }

        #endregion
    }
}
