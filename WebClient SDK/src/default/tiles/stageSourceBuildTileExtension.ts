import { sendCompileRequest, CompiledCardTypes } from '../workflow/krProcess/krUIHelper';
import { TileExtension, ITileGlobalExtensionContext, Tile, TileGroups, ITileLocalExtensionContext, TileHotkey } from 'tessa/ui/tiles';
import { CardStoreMode } from 'tessa/cards';

export class StageSourceBuildTileExtension extends TileExtension {

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const panel = context.workspace.leftPanel;
    const contextSource = panel.contextSource;

    const tile = new Tile({
      name: 'StageSourceBuild',
      caption: '$UI_Tiles_StageSourceBuild',
      icon: 'ta icon-thin-096',
      contextSource,
      command: async () => await sendCompileRequest('.CompileWithValidationResult'),
      group: TileGroups.Cards,
      order: 26,
      evaluating: e =>  {
        const editor = e.currentTile.context.cardEditor;
        e.setIsEnabledWithCollapsing(e.currentTile,
          !!editor
          && !!editor.cardModel
          && CompiledCardTypes.some(x => x === editor.cardModel!.cardType.id
          && editor.cardModel!.card.storeMode === CardStoreMode.Update)
        );
      },
      toolTip: '$UI_Tiles_StageSourceBuild_ToolTip'
    });

    panel.tiles.push(tile);
  }

  public initializingLocal(context: ITileLocalExtensionContext) {
    const leftPanel = context.workspace.leftPanel;
    const tile = leftPanel.tryGetTile('StageSourceBuild');
    if (!tile) {
      return;
    }

    const hotkeyStorage = leftPanel.contextSource.hotkeyStorage;
    hotkeyStorage.addTileHotkey(new TileHotkey(tile, 'Ctrl+Shift+B', 'KeyB', {ctrl: true, shift: true}));
  }

}