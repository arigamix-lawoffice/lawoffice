using System;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;
using Tessa.UI.Cards.Controls.AutoComplete;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public sealed class KrStageTemplateUIExtension : CardUIExtension
    {
        #region constants

        private const string TableAlias = KrConstants.Ui.KrApprovalStagesControlAlias;

        private const string AddComputedRoleLink = KrConstants.Ui.AddComputedRoleLink;

        private const string Approvers = KrConstants.Ui.KrMultiplePerformersTableAcAlias;

        #endregion

        #region private

        private static bool HasComputedRole(Card card, Guid stageRowID) =>
            card.TryGetPerformersSection(out var sec)
            && sec.Rows.Any(row => HasComputedRoleInRow(row, stageRowID));

        private static bool HasComputedRoleInRow(CardRow row, Guid stageRowID) =>
            row.Fields.TryGetValue(KrConstants.KrPerformersVirtual.StageRowID, out var srid)
            && stageRowID.Equals(srid)
            && row.Fields.TryGetValue(KrConstants.KrPerformersVirtual.PerformerID, out var approverID)
            && KrConstants.SqlApproverRoleID.Equals(approverID)
            && row.State != CardRowState.Deleted;

        private static bool HasHiddenStages(
            Card card)
        {
            return card.TryGetStagesSection(out var sec, preferVirtual: true)
                && sec.Rows.Any(p => p.TryGet<bool?>(KrConstants.KrStages.Hidden) == true);
        }

        #endregion

        #region base overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var cardModel = context.Model;

            if (cardModel.Controls.TryGet(KrConstants.Ui.CanMoveCheckboxAlias, out var canMove)
                && canMove is CheckBoxViewModel canMoveCheckbox)
            {
                canMoveCheckbox.PropertyChanged += (
                    s,
                    e) =>
                {
                    if (s is CheckBoxViewModel checkbox
                        && e.PropertyName == "IsChecked"
                        && checkbox.IsChecked
                        && HasHiddenStages(cardModel.Card))
                    {
                        TessaDialog.ShowNotEmpty(
                            new ValidationResultBuilder()
                                // Там есть вложенные строки локализации, которые формат может разобрать
                                .AddWarning(LocalizationManager.Format("$KrProcess_HiddenStageWillBeRevealed")));
                    }
                };
            }

            if (cardModel.Controls.TryGet(TableAlias, out var control)
                && control is GridViewModel grid)
            {
                NotifyCollectionChangedEventHandler approversControlChanged = null;

                grid.RowInitializing += (s, e) =>
                {
                    e.Window.Width *= 1.5;

                    if (e.RowModel.Controls.TryGet(KrConstants.Ui.KrHiddenStageCheckboxAlias, out var hiddenCheckbox))
                    {
                        var canChangeOrder = e.CardModel.Card.Sections.TryGetValue(KrConstants.KrStageTemplates.Name, out var section)
                            && (section.Fields.TryGet<bool?>(KrConstants.KrStageTemplates.CanChangeOrder) ?? default);
                        // Если шаблон можно перемещать, то контрол становится ридонли
                        hiddenCheckbox.IsReadOnly = canChangeOrder;
                    }

                    if (e.RowModel.Controls.TryGet(AddComputedRoleLink, out var hyperlinkControl)
                        && hyperlinkControl is HyperlinkViewModel hyperlink)
                    {
                        hyperlink.ControlVisibility = HasComputedRole(e.CardModel.Card, e.Row.RowID)
                            ? Visibility.Collapsed
                            : Visibility.Visible;

                        if (e.RowModel.Controls.TryGet(Approvers, out var approversControlObj)
                            && approversControlObj is AutoCompleteTableViewModel approversControl)
                        {
                            approversControlChanged = (s1, e1) =>
                            {
                                var oldVisibility = hyperlink.ControlVisibility;
                                hyperlink.ControlVisibility = HasComputedRole(e.CardModel.Card, e.Row.RowID)
                                    ? Visibility.Collapsed
                                    : Visibility.Visible;
                                if (oldVisibility != hyperlink.ControlVisibility)
                                {
                                    if (hyperlink.ControlVisibility == Visibility.Visible
                                        && hyperlink.Block.BlockVisibility == Visibility.Collapsed)
                                    {
                                        hyperlink.Block.Form.Rearrange();
                                    }
                                    else
                                    {
                                        hyperlink.Block.RearrangeSelf();
                                    }
                                }
                            };

                            approversControl.Items.CollectionChanged += approversControlChanged;
                        }

                        var card = e.CardModel.Card;
                        var rows = card.Sections[KrConstants.KrPerformersVirtual.Synthetic].Rows;
                        var rowID = e.Row.RowID;

                        hyperlink.CommandClosure.Execute = _ =>
                        {
                            if (!HasComputedRole(e.CardModel.Card, e.Row.RowID))
                            {
                                var row = new CardRow
                                {
                                    RowID = Guid.NewGuid(),
                                    State = CardRowState.Inserted
                                };
                                row.Fields[KrConstants.KrPerformersVirtual.PerformerID] = KrConstants.SqlApproverRoleID;
                                row.Fields[KrConstants.KrPerformersVirtual.PerformerName] = "$KrProcess_SqlPerformersRole";
                                row.Fields[KrConstants.KrPerformersVirtual.StageRowID] = rowID;
                                row.Fields[KrConstants.KrPerformersVirtual.Order] = Int32Boxes.Box(
                                    rows.Count > 0 
                                    ? rows.Max(p => p.Fields.Get<int>(KrConstants.KrPerformersVirtual.Order)) + 1
                                    : 0);
                                rows.Add(row);
                            }
                        };
                    }
                };

                grid.RowEditorClosed += (s, e) =>
                {
                    if (approversControlChanged != null
                        && e.RowModel.Controls.TryGet(Approvers, out var approversControlObj)
                        && approversControlObj is AutoCompleteTableViewModel approversControl)
                    {
                        approversControl.Items.CollectionChanged -= approversControlChanged;
                    }
                };
            }
            
        }

        #endregion
    }
}