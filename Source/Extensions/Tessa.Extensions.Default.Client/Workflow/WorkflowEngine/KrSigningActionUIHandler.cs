using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Tessa.Cards;
using Tessa.Extensions.Default.Client.UI;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
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
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrSigningDescriptor"/>.
    /// </summary>
    public sealed class KrSigningActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Fields

        private readonly ICardMetadata cardMetadata;
        private readonly IActionCompletionOptionsProvider completionOptionsActionsProvider;
        private readonly TableRowContentIndicator[] tableIndicators = new TableRowContentIndicator[3];

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrSigningActionUIHandler"/>.
        /// </summary>
        /// <param name="cardMetadata">Репозиторий с метаинформацией.</param>
        /// <param name="completionOptionsActionsProvider">Объект предоставляющий доступ к вариантам завершения действий.</param>
        public KrSigningActionUIHandler(
            ICardMetadata cardMetadata,
            IActionCompletionOptionsProvider completionOptionsActionsProvider)
            : base(KrDescriptors.KrSigningDescriptor)
        {
            this.cardMetadata = cardMetadata;
            this.completionOptionsActionsProvider = completionOptionsActionsProvider;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        protected override async Task AttachToCardCoreAsync(WorkflowEngineBindingContext bindingContext)
        {
            if (bindingContext.ActionTemplate is not null)
            {
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrSigningActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrSigningActionVirtual.SectionName];

                await this.AttachFieldToTemplateAsync(
                    bindingContext,
                    KrSigningActionVirtual.InitTaskScript,
                    typeof(string),
                    KrSigningActionVirtual.SectionName,
                    KrSigningActionVirtual.InitTaskScript);

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
                KrSigningActionVirtual.SectionName,
                KrSigningActionVirtual.SectionName);

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
                KrSigningActionOptionsVirtual.SectionName,
                KrSigningActionOptionsVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrSigningActionOptionsActionVirtual.SectionName,
                KrSigningActionOptionsActionVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrSigningActionOptionLinksVirtual.SectionName,
                KrSigningActionOptionLinksVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrWeRolesVirtual.SectionName,
                KrWeRolesVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrSigningActionNotificationRolesVirtual.SectionName,
                KrSigningActionNotificationRolesVirtual.SectionName);

            await this.AttachTableSectionAsync(
                bindingContext,
                KrSigningActionNotificationActionRolesVirtual.SectionName,
                KrSigningActionNotificationActionRolesVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                WorkflowTaskActionBase.EventsSectionName,
                WorkflowTaskActionBase.EventsSectionName);

            this.AddLinkBinding(
                bindingContext,
                new[] { KrSigningActionOptionLinksVirtual.Link, Names.Table_ID },
                KrSigningActionOptionLinksVirtual.SectionName);
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
            var addComputedRoleLink = (HyperlinkViewModel) cardModel.Controls[WorkflowConstants.UI.AddComputedRoleLink];

            if (!isReadOnly)
            {
                var mainSection = card.Sections[KrSigningActionVirtual.SectionName];
                mainSection.FieldChanged += this.MainSection_FieldChanged;

                performersSection.Rows.ItemChanged += (sender, e) =>
                {
                    switch (e.Action)
                    {
                        case ListStorageAction.Remove when IsComputedRole(e.Item):
                        case ListStorageAction.Clear:
                            ChangeVisibilityAndRearrangeChildren(
                                addComputedRoleLink,
                                addComputedRoleLink.Block.Form,
                                Visibility.Visible);
                            break;
                    }
                };

                addComputedRoleLink.CommandClosure.Execute = _ =>
                {
                    var newRow = performersSection.Rows.Add();
                    newRow.RowID = Guid.NewGuid();
                    newRow.State = CardRowState.None;
                    newRow[KrWeRolesVirtual.Role + Names.Table_ID] = KrConstants.SqlApproverRoleID;
                    newRow[KrWeRolesVirtual.Role + Table_Field_Name] = KrConstants.SqlApproverRoleName;
                    newRow.State = CardRowState.Inserted;

                    ChangeVisibilityAndRearrangeChildren(
                        addComputedRoleLink,
                        addComputedRoleLink.Block.Form,
                        Visibility.Collapsed);
                };

                var validationResult = new ValidationResultBuilder();
                await WorkflowHelper.InializeTaskCompletionOptionsAsync(
                    this.cardMetadata,
                    card.Sections[KrSigningActionOptionsVirtual.SectionName].Rows,
                    cardModel.CreateEmptyRow(KrSigningActionOptionsVirtual.SectionName),
                    new[]
                    {
                        DefaultTaskTypes.KrSigningTypeID,
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
                    card.Sections[KrSigningActionOptionsActionVirtual.SectionName].Rows,
                    cardModel.CreateEmptyRow(KrSigningActionOptionsActionVirtual.SectionName),
                    new[]
                    {
                        ActionCompletionOptions.Signed,
                        ActionCompletionOptions.Declined
                    });
            }

            ChangeVisibilityAndRearrangeChildren(
                addComputedRoleLink,
                addComputedRoleLink.Block.Form,
                performersSection.Rows.Any(IsComputedRole) ? Visibility.Collapsed : Visibility.Visible);

            this.tableIndicators[0] = await TableRowContentIndicator.InitializeAndStartTrackingAsync(
                cardModel,
                this.cardMetadata,
                WorkflowConstants.UI.ActionCompletionOptionsTable,
                exceptSectionNames: ExceptSectionNames,
                exceptSectionFieldNames: new Dictionary<string, ICollection<string>>(StringComparer.Ordinal)
                {
                    {
                        KrSigningActionOptionsActionVirtual.SectionName,
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
                        KrSigningActionOptionsVirtual.SectionName,
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
            var mainSection = card.Sections[KrSigningActionVirtual.SectionName];
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
            var mainSectionInner = (CardSection) sender;
            switch (e.FieldName)
            {
                case KrSigningActionVirtual.Period when e.FieldValue != null:
                    mainSectionInner.Fields[KrSigningActionVirtual.Planned] = default;
                    break;
                case KrSigningActionVirtual.Planned when e.FieldValue != null:
                    mainSectionInner.Fields[KrSigningActionVirtual.Period] = default;
                    break;
            }
        }

        private static void ChangeVisibilityAndRearrangeChildren(
            IControlViewModel control,
            IFormWithBlocksViewModel form,
            Visibility visibility)
        {
            if (control.ControlVisibility != visibility)
            {
                control.ControlVisibility = visibility;
                form.RearrangeChildren();
            }
        }

        private static bool IsComputedRole(CardRow row)
            => (CardRowState?) row.TryGet<int?>(CardRow.SystemStateKey) != CardRowState.Deleted
                && row.TryGet<Guid?>(KrWeRolesVirtual.Role + Names.Table_ID) == KrConstants.SqlApproverRoleID;

        #endregion
    }
}
