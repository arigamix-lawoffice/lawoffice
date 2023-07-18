using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Scheme;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.WorkflowViewer.Actions;
using Tessa.Workflow;
using Tessa.Workflow.Storage;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Client.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описывает логику пользовательского интерфейса для действия <see cref="KrDescriptors.KrResolutionDescriptor"/>.
    /// </summary>
    public sealed class KrResolutionActionUIHandler : WorkflowActionUIHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrResolutionActionUIHandler"/>.
        /// </summary>
        public KrResolutionActionUIHandler()
            : base(KrDescriptors.KrResolutionDescriptor)
        {
        }

        #endregion

        #region Base overrides

        /// <inheritdoc />
        protected override async Task AttachToCardCoreAsync(WorkflowEngineBindingContext bindingContext)
        {
            if (bindingContext.ActionTemplate is not null)
            {
                bindingContext.Section = bindingContext.Card.Sections.GetOrAdd(KrResolutionActionVirtual.SectionName);
                bindingContext.SectionMetadata = (await bindingContext.CardMetadata.GetSectionsAsync(bindingContext.CancellationToken).ConfigureAwait(false))[KrResolutionActionVirtual.SectionName];
            }

            await this.AttachEntrySectionAsync(
                bindingContext,
                KrResolutionActionVirtual.SectionName,
                KrResolutionActionVirtual.SectionName);

            await this.AttachTableSectionToTemplateAsync(
                bindingContext,
                KrWeRolesVirtual.SectionName,
                KrWeRolesVirtual.SectionName);
        }

        /// <inheritdoc />
        protected override Task UpdateFormCoreAsync(
            WorkflowStorageBase action,
            WorkflowStorageBase node,
            WorkflowStorageBase process,
            ICardModel cardModel,
            WorkflowActionStorage actionTemplate = null,
            CancellationToken cancellationToken = default)
        {
            var card = cardModel.Card;
            var isReadOnly = card.Permissions.CardPermissions.Has(CardPermissionFlags.ProhibitModify);
            var mainSection = card.Sections[KrResolutionActionVirtual.SectionName];
            var performersSection = card.Sections[KrWeRolesVirtual.SectionName];
            var addComputedRoleLink = (HyperlinkViewModel) cardModel.Controls[WorkflowConstants.UI.AddComputedRoleLink];
            var controllerAutoComplete = cardModel.Controls[WorkflowConstants.UI.ControllerAutoComplete];

            if (!isReadOnly)
            {
                mainSection.FieldChanged += (_, e) =>
                {
                    switch (e.FieldName)
                    {
                        case KrResolutionActionVirtual.Period when e.FieldValue is not null:
                            mainSection.Fields[KrResolutionActionVirtual.Planned] = default;
                            break;
                        case KrResolutionActionVirtual.Planned when e.FieldValue is not null:
                            mainSection.Fields[KrResolutionActionVirtual.Period] = default;
                            break;
                        case KrResolutionActionVirtual.WithControl:
                            ChangeVisibility(
                                controllerAutoComplete,
                                e.FieldValue is bool value && value);
                            break;
                        case KrResolutionActionVirtual.IsMassCreation:
                            ChangeIsMajorPerformerChecked(
                                mainSection,
                                e.FieldValue is bool isMassCreation
                                && isMassCreation);
                            break;
                    }
                };

                performersSection.Rows.ItemChanged += (sender, e) =>
                {
                    var rowsInner = (ListStorage<CardRow>) sender;

                    switch (e.Action)
                    {
                        case ListStorageAction.Clear:
                            ChangeVisibility(addComputedRoleLink, true);
                            SetIsMassCreation(mainSection, false);
                            break;

                        case ListStorageAction.Insert:
                        case ListStorageAction.Remove:
                            if (IsComputedRole(e.Item))
                            {
                                ChangeVisibility(addComputedRoleLink, true);
                            }

                            SetIsMassCreation(mainSection, HasManyPerformers(rowsInner));
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

                    ChangeVisibility(addComputedRoleLink, false);
                };
            }

            ChangeVisibility(
                addComputedRoleLink,
                !performersSection.Rows.Any(IsComputedRole));
            ChangeVisibility(
                controllerAutoComplete,
                mainSection.Fields.TryGet<bool?>(KrResolutionActionVirtual.WithControl) == true);
            ChangeIsMajorPerformerChecked(
                mainSection,
                mainSection.Fields.TryGet<bool?>(KrResolutionActionVirtual.IsMassCreation) == true);

            return Task.CompletedTask;
        }

        #endregion

        #region Private methods

        private static void ChangeVisibility(
            IControlViewModel control,
            Visibility visibility)
        {
            if (control.ControlVisibility != visibility)
            {
                control.ControlVisibility = visibility;
            }
        }

        private static bool IsComputedRole(CardRow row)
            => (CardRowState?) row.TryGet<int?>(CardRow.SystemStateKey) != CardRowState.Deleted
                && row.TryGet<Guid?>(KrWeRolesVirtual.Role + Names.Table_ID) == KrConstants.SqlApproverRoleID;

        private static void ChangeVisibility(
            IControlViewModel control,
            bool isVisible)
        {
            ChangeVisibility(
                control,
                isVisible
                ? Visibility.Visible
                : Visibility.Collapsed);
        }

        private static bool HasManyPerformers(
            IEnumerable<CardRow> rows)
        {
            bool isFound = default;
            foreach (var row in rows)
            {
                if (row.State != CardRowState.Deleted)
                {
                    if (isFound)
                    {
                        return true;
                    }

                    isFound = true;
                }
            }

            return false;
        }

        private static void SetIsMassCreation(CardSection mainSection, bool value)
        {
            var oldValue = mainSection.Fields.TryGet<bool?>(KrResolutionActionVirtual.IsMassCreation);
            if (oldValue != value)
            {
                mainSection.Fields[KrResolutionActionVirtual.IsMassCreation] = BooleanBoxes.Box(value);
            }
        }

        private static void ChangeIsMajorPerformerChecked(CardSection mainSection, bool isChecked)
        {
            if (!isChecked)
            {
                mainSection.Fields[KrResolutionActionVirtual.IsMajorPerformer] = BooleanBoxes.False;
            }
        }

        #endregion
    }
}
