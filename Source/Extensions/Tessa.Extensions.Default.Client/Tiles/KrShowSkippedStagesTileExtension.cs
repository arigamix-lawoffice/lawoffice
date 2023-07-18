using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class KrShowSkippedStagesTileExtension: TileExtension
    {
        #region Command Actions

        private static async void OpenWithSkipStagesButtonsAsync(object parameter)
        {
            var card = UIContext.Current?.CardEditor?.CardModel?.Card;
            if (card is null)
            {
                return;
            }

            var info = new Dictionary<string, object>();
            info.DontHideSkippedStages();

            await KrTileHelper.OpenMarkedCardAsync(
                null,
                null, //Не требуем подтверждения действия, если не было изменений
                null,
                getInfo: info);
        }

        #endregion

        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            ITileContextSource contextSource = context.Workspace.LeftPanel;
            var cardOthersTile = context.Workspace.LeftPanel.Tiles.TryGet(TileNames.CardOthers);
            cardOthersTile?.Tiles.Add(
                new Tile(
                    DefaultTileNames.KrShowSkippedStages,
                    TileHelper.SplitCaption("$KrTiles_ShowSkipStages"),
                    context.Icons.Get("Thin45"),
                    contextSource,
                    new DelegateCommand(OpenWithSkipStagesButtonsAsync),
                    TileGroups.Cards,
                    order: 25,
                    size: TileSize.Half,
                    toolTip: TileHelper.GetToolTip("$KrTiles_ShowSkipStagesTooltip", KrTileKeys.ShowSkipStages),
                    evaluating: TileHelper.EnableWhenVisibleInCardHandler));

            return Task.CompletedTask;
        }


        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            ITile showSkipStages = context
                .Workspace
                .LeftPanel
                .Tiles
                .TryGet(TileNames.CardOthers)
                ?.Tiles
                ?.TryGet(DefaultTileNames.KrShowSkippedStages);
            showSkipStages?.Context.AddInputBinding(showSkipStages, KrTileKeys.ShowSkipStages);

            return Task.CompletedTask;
        }

        #endregion
    }
}