using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.UI.Cards;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Storage;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrAmendingDescriptor"/>.
    /// </summary>
    public sealed class KrAmendingActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Fields
        
        private readonly ICardMetadata cardMetadata;
        private TableRowContentIndicator tableIndicator;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrAmendingActionUIHandler"/>.
        /// </summary>
        /// <param name="cardMetadata">Репозиторий с метаинформацией.</param>
        public KrAmendingActionUIHandler(
            ICardMetadata cardMetadata)
            : base(KrDescriptors.KrAmendingDescriptor)
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
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrAmendingActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrAmendingActionVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrAmendingActionVirtual.InitTaskScript,
                    typeof(string),
                    KrAmendingActionVirtual.SectionName,
                    KrAmendingActionVirtual.InitTaskScript);
            }

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrAmendingActionVirtual.SectionName,
                KrAmendingActionVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                WorkflowTaskActionBase.NotificationRolesSectionName,
                WorkflowTaskActionBase.NotificationRolesSectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                WorkflowTaskActionBase.EventsSectionName,
                WorkflowTaskActionBase.EventsSectionName);
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
            var mainSection = card.Sections[KrAmendingActionVirtual.SectionName];
            mainSection.FieldChanged += this.MainSection_FieldChanged;

            this.tableIndicator = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
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
            var mainSection = card.Sections[KrAmendingActionVirtual.SectionName];
            mainSection.FieldChanged -= this.MainSection_FieldChanged;

            this.tableIndicator.StopTracking();
        }

        #endregion

        #region Private methods

        private void MainSection_FieldChanged(object sender, CardFieldChangedEventArgs e)
        {
            var mainSectionInner = (CardSection)sender;
            switch (e.FieldName)
            {
                case KrAmendingActionVirtual.Period when e.FieldValue != null:
                    mainSectionInner.Fields[KrAmendingActionVirtual.Planned] = default;
                    break;
                case KrAmendingActionVirtual.Planned when e.FieldValue != null:
                    mainSectionInner.Fields[KrAmendingActionVirtual.Period] = default;
                    break;
            }
        }

        #endregion
    }
}
