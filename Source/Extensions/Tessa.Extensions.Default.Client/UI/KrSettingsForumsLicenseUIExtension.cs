using System.Threading.Tasks;
using System.Windows;
using Tessa.Extensions.Default.Shared;
using Tessa.Forums;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI
{
    public class KrSettingsForumsLicenseUIExtension : CardUIExtension
    {
        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            if (context.Card.TryGetInfo()?.TryGet<bool>(ForumHelper.LicenseWarningFlag) != true)
            {
                return;
            }

            // расширение зарегистрировано только для этих двух типов карточек
            if (context.Model.CardType.ID == DefaultCardTypes.KrSettingsTypeID)
            {
                var grid = (GridViewModel) context.Model.Controls[KrTypesUIHelper.TypesControl];
                grid.RowInvoked += cardTypesControl_RowInvoked;
            }
            else if (context.Model.CardType.ID == DefaultCardTypes.KrDocTypeTypeID)
            {
                var control = context.Model.Controls[ForumHelper.LicenseWarningControlAlias];
                control.ControlVisibility = Visibility.Visible;
                control.Block.Form.RearrangeSelf();
            }
        }

        #endregion

        #region Event Handlers

        private static void cardTypesControl_RowInvoked(object sender, GridRowEventArgs e)
        {
            if (e.Action == GridRowAction.Inserted || e.Action == GridRowAction.Opening)
            {
                if (!e.RowModel.Controls.TryGet(ForumHelper.LicenseWarningControlAlias, out IControlViewModel control))
                {
                    return;
                }

                control.ControlVisibility = Visibility.Visible;
                e.RowModel.MainFormWithBlocks.RearrangeSelf();
            }
        }

        #endregion
    }
}