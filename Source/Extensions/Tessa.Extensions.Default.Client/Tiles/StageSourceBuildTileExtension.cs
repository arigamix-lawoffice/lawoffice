using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Client.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class StageSourceBuildTileExtension: TileExtension
    {
        #region Private Methods

        private static void EnableForSendRequest(object sender, TileEvaluationEventArgs e)
        {
            var model = e.CurrentTile.Context.CardEditor?.CardModel;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                model != null
                && KrConstants.CompiledCardTypes.Any(p => model.CardType.ID == p)
                && model.Card.StoreMode == CardStoreMode.Update);
        }

        #endregion

        #region base overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            var panel = context.Workspace.LeftPanel;
            var tiles = panel.Tiles;

            var tile =
                new Tile(
                    TileNames.StageSourceBuild,
                    "$UI_Tiles_StageSourceBuild",
                    context.Icons.Get("Thin96"),
                    panel,
                    new DelegateCommand(async o => await KrUIHelper.SendCompileRequestAsync(KrConstants.Keys.CompileWithValidationResult)),
                    TileGroups.Cards,
                    order: 26,
                    toolTip: TileHelper.GetToolTip("$UI_Tiles_StageSourceBuild_ToolTip", KrTileKeys.StageSourceBuild),
                    evaluating: EnableForSendRequest);

            tiles.Add(tile);
            return Task.CompletedTask;
        }

        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            var tile = context.Workspace.LeftPanel.Tiles.TryGet(TileNames.StageSourceBuild);
            tile?.Context.AddInputBinding(tile, KrTileKeys.StageSourceBuild);
            return Task.CompletedTask;
        }

        #endregion
    }
}