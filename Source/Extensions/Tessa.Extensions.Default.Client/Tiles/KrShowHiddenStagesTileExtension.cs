using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class KrShowHiddenStagesTileExtension: TileExtension
    {
        #region Command Actions

        private static async void OpenWithHiddenButtonsAsync(object parameter)
        {
            var card = UIContext.Current?.CardEditor?.CardModel?.Card;
            if (card is null)
            {
                return;
            }

            var info = new Dictionary<string, object>();
            info.DontHideStages();
            if (card.Info.TryGet<bool>(KrPermissionsHelper.PermissionsCalculatedMark))
            {
                info.Add(KrPermissionsHelper.CalculatePermissionsMark, BooleanBoxes.True);
            }

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
                    DefaultTileNames.KrShowHiddenStages,
                    TileHelper.SplitCaption("$KrTiles_ShowHiddenStages"),
                    context.Icons.Get("Thin45"),
                    contextSource,
                    new DelegateCommand(OpenWithHiddenButtonsAsync),
                    TileGroups.Cards,
                    order: 25,
                    size: TileSize.Half,
                    toolTip: TileHelper.GetToolTip("$KrTiles_ShowHiddenStagesTooltip", KrTileKeys.ShowHiddenStages),
                    evaluating: TileHelper.EnableWhenVisibleInCardHandler));

            return Task.CompletedTask;
        }


        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            ITile showHiddenStages = context
                .Workspace
                .LeftPanel
                .Tiles
                .TryGet(TileNames.CardOthers)
                ?.Tiles
                ?.TryGet(DefaultTileNames.KrShowHiddenStages);
            showHiddenStages?.Context.AddInputBinding(showHiddenStages, KrTileKeys.ShowHiddenStages);

            return Task.CompletedTask;
        }

        #endregion
    }
}