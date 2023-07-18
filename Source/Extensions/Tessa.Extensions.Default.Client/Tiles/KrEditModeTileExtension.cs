using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.UI;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class KrEditModeTileExtension :
        TileExtension
    {
        #region Command Actions

        private static async void OpenForEditingAsync(object parameter)
        {
            await KrTileHelper.OpenMarkedCardAsync(KrPermissionsHelper.CalculatePermissionsMark,
                null, //Не требуем подтверждения действия, если не было изменений
                () => TessaDialog.ConfirmWithCancel("$KrTiles_EditModeConfirmation"));
        }

        #endregion

        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            ITileContextSource contextSource = context.Workspace.LeftPanel;

            context.Workspace.LeftPanel.Tiles.Add(
                new Tile(
                    DefaultTileNames.KrEditMode,
                    TileHelper.SplitCaption("$KrTiles_EditMode"),
                    context.Icons.Get("Thin2"),
                    contextSource,
                    new DelegateCommand(OpenForEditingAsync),
                    TileGroups.Cards,
                    order: 22,
                    toolTip: TileHelper.GetToolTip("$KrTiles_EditModeTooltip", KrTileKeys.Edit),
                    evaluating: TileHelper.EnableWhenVisibleInCardHandler));

            return Task.CompletedTask;
        }


        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            ITile krEnterEditMode = context.Workspace.LeftPanel.Tiles.TryGet(DefaultTileNames.KrEditMode);
            if (krEnterEditMode != null)
            {
                krEnterEditMode.Context.AddInputBinding(krEnterEditMode, KrTileKeys.Edit);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
