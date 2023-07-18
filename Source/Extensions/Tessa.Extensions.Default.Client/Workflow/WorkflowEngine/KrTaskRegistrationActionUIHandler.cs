using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform.Validation;
using Tessa.Scheme;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Storage;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrTaskRegistrationDescriptor"/>.
    /// </summary>
    public sealed class KrTaskRegistrationActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;
        private readonly TableRowContentIndicator[] tableIndicators = new TableRowContentIndicator[2];

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrTaskRegistrationActionUIHandler"/>.
        /// </summary>
        /// <param name="cardMetadata">Репозиторий с метаинформацией.</param>
        public KrTaskRegistrationActionUIHandler(
            ICardMetadata cardMetadata)
            : base(KrDescriptors.KrTaskRegistrationDescriptor)
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
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrTaskRegistrationActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrTaskRegistrationActionVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrTaskRegistrationActionVirtual.InitTaskScript,
                    typeof(string),
                    KrTaskRegistrationActionVirtual.SectionName,
                    KrTaskRegistrationActionVirtual.InitTaskScript);
            }

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrTaskRegistrationActionVirtual.SectionName,
                KrTaskRegistrationActionVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrTaskRegistrationActionOptionsVirtual.SectionName,
                KrTaskRegistrationActionOptionsVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrTaskRegistrationActionOptionLinksVirtual.SectionName,
                KrTaskRegistrationActionOptionLinksVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrTaskRegistrationActionNotificationRolesVitrual.SectionName,
                KrTaskRegistrationActionNotificationRolesVitrual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                WorkflowTaskActionBase.EventsSectionName,
                WorkflowTaskActionBase.EventsSectionName);

            this.AddLinkBinding(
                bindingContext,
                new[] { ActionOptionLinksBase.Link, Names.Table_ID },
                KrTaskRegistrationActionOptionLinksVirtual.SectionName);
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
            var mainSection = card.Sections[KrTaskRegistrationActionVirtual.SectionName];
            mainSection.FieldChanged += this.MainSection_FieldChanged;

            var validationResult = new ValidationResultBuilder();
            await WorkflowHelper.InializeTaskCompletionOptionsAsync(
                this.cardMetadata,
                card.Sections[KrTaskRegistrationActionOptionsVirtual.SectionName].Rows,
                cardModel.CreateEmptyRow(KrTaskRegistrationActionOptionsVirtual.SectionName),
                new[] { DefaultTaskTypes.KrRegistrationTypeID },
                validationResult,
                this,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            await TessaDialog.ShowNotEmptyAsync(validationResult).ConfigureAwait(false);

            this.tableIndicators[0] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
                cardModel,
                this.cardMetadata,
                WorkflowConstants.UI.CompletionOptionsTable,
                exceptSectionNames: ExceptSectionNames,
                exceptSectionFieldNames: new Dictionary<string, ICollection<string>>(StringComparer.Ordinal)
                {
                    {
                        KrTaskRegistrationActionOptionsVirtual.SectionName,
                        ExceptSectionFieldNames
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
            var mainSection = card.Sections[KrTaskRegistrationActionVirtual.SectionName];
            mainSection.FieldChanged -= this.MainSection_FieldChanged;

            foreach (var tableIndicator in this.tableIndicators)
            {
                tableIndicator.StopTracking();
            }
        }

        #endregion

        #region Private methods

        private void MainSection_FieldChanged(object sender, CardFieldChangedEventArgs e)
        {
            var mainSectionInner = (CardSection)sender;
            switch (e.FieldName)
            {
                case KrTaskRegistrationActionVirtual.Period when e.FieldValue != null:
                    mainSectionInner.Fields[KrTaskRegistrationActionVirtual.Planned] = default;
                    break;
                case KrTaskRegistrationActionVirtual.Planned when e.FieldValue != null:
                    mainSectionInner.Fields[KrTaskRegistrationActionVirtual.Period] = default;
                    break;
            }
        }

        #endregion
    }
}
