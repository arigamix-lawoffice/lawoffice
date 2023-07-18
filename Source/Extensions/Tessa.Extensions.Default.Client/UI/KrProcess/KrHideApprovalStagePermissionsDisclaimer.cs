using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public sealed class KrHideApprovalStagePermissionsDisclaimer :
        CardUIExtension
    {
        #region Private Methods

        private static void OnGridRowInvoked(object sender, GridRowEventArgs e)
        {
            if (e.Action != GridRowAction.Inserted && e.Action != GridRowAction.Opening)
            {
                return;
            }

            if (!e.RowModel.Controls.TryGet("DisclaimerControl", out IControlViewModel label))
            {
                return;
            }

            bool readOnly = e.RowModel.Card.Permissions.Resolver
                .GetRowPermissions(KrConstants.KrStages.Virtual, e.Row.RowID)
                .Has(CardPermissionFlags.ProhibitModify);

            if (readOnly)
            {
                string fieldName = StageTypeSettingsNaming.PlainColumnName("KrApprovalSettingsVirtual", "IsParallel");
                if (!e.Row.Get<bool>(fieldName))
                {
                    label.ControlVisibility = System.Windows.Visibility.Collapsed;
                    label.Block.RearrangeSelf();
                }
            }
            else if (e.RowModel.Controls.TryGet("IsParallelFlag", out IControlViewModel isParallelControl))
            {
                isParallelControl.PropertyChanged += (sender2, e2) =>
                {
                    if (e2.PropertyName == "IsChecked")
                    {
                        label.ControlVisibility =
                            (label.ControlVisibility == System.Windows.Visibility.Collapsed)
                                ? System.Windows.Visibility.Visible
                                : System.Windows.Visibility.Collapsed;

                        label.Block.RearrangeSelf();
                    }
                };
            }
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ICardModel model = context.Model;

            if (model.CardType.Flags.HasNot(CardTypeFlags.AllowTasks))
            {
                return;
            }

            if (!model.Forms.TryGet("ApprovalProcess", out IFormWithBlocksViewModel approvalTab))
            {
                return;
            }

            foreach (IBlockViewModel block in approvalTab.Blocks)
            {
                if (block.Name == "ApprovalStagesBlock")
                {
                    foreach (IControlViewModel control in block.Controls)
                    {
                        if (control is GridViewModel grid)
                        {
                            grid.RowInvoked += OnGridRowInvoked;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
