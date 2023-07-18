import { TileExtension, ITileGlobalExtensionContext, Tile, TileGroups, ITileLocalExtensionContext, TileHotkey,
  enableWhenVisibleInCardHandler, openMarkedCard} from 'tessa/ui/tiles';
import { UIContext, tryGetFromInfo } from 'tessa/ui';
import { createTypedField, DotNetType } from 'tessa/platform';

export class KrShowHiddenStagesTileExtension extends TileExtension {

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const panel = context.workspace.leftPanel;
    const contextSource = panel.contextSource;

    const cardOthers = panel.tryGetTile('CardOthers');
    if (!cardOthers) {
      return;
    }

    const tile = new Tile({
      name: 'KrShowHiddenStages',
      caption: '$KrTiles_ShowHiddenStages',
      icon: 'ta icon-thin-045',
      contextSource,
      command: KrShowHiddenStagesTileExtension.openWithHiddenButtons,
      group: TileGroups.Cards,
      order: 25,
      evaluating: enableWhenVisibleInCardHandler,
      toolTip: '$KrTiles_ShowHiddenStagesTooltip'
    });

    cardOthers.tiles.push(tile);
  }

  public initializingLocal(context: ITileLocalExtensionContext) {
    const leftPanel = context.workspace.leftPanel;
    const cardOthers = leftPanel.tryGetTile('CardOthers');
    if (!cardOthers) {
      return;
    }

    const tile = leftPanel.tryGetTile('KrShowHiddenStages');
    if (!tile) {
      return;
    }

    const hotkeyStorage = leftPanel.contextSource.hotkeyStorage;
    hotkeyStorage.addTileHotkey(new TileHotkey(tile, 'Ctrl+Alt+H', 'KeyH', {ctrl: true, alt: true}));
  }

  public static openWithHiddenButtons() {
    const editor = UIContext.current.cardEditor;
    if (!editor || !editor.cardModel) {
      return;
    }

    const card = editor.cardModel.card;
    const info = {
      'DontHideStagesInfoMark': createTypedField(true, DotNetType.Boolean)
    };

    if (tryGetFromInfo(card.info, 'kr_permissions_calculated', false)) {
      info['kr_calculate_permissions'] = createTypedField(true, DotNetType.Boolean);
    }

    openMarkedCard(null, null, null, null, info);
  }

}