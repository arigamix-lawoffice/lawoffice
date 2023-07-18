import { showConfirmWithCancel } from 'tessa/ui';
import { TileExtension, ITileGlobalExtensionContext, Tile, TileGroups,
  ITileLocalExtensionContext, TileHotkey, openMarkedCard, enableWhenVisibleInCardHandler } from 'tessa/ui/tiles';

export class KrEditModeTileExtension extends TileExtension {

  //#region TileExtension

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const contextSource = context.workspace.leftPanel.contextSource;

    context.workspace.leftPanel.tiles.push(
      new Tile({
        name: 'KrEditMode',
        caption: '$KrTiles_EditMode',
        icon: 'ta icon-thin-002',
        contextSource,
        command: KrEditModeTileExtension.openForEditing,
        group: TileGroups.Cards,
        order: 22,
        evaluating: enableWhenVisibleInCardHandler,
        toolTip: '$KrTiles_EditModeTooltip'
      })
    );
  }

  public initializingLocal(context: ITileLocalExtensionContext) {
    const panel = context.workspace.leftPanel;
    const hotkeyStorage = panel.contextSource.hotkeyStorage;
    const krEnterEditMode = panel.tryGetTile('KrEditMode');
    if (krEnterEditMode) {
      hotkeyStorage.addTileHotkey(new TileHotkey(krEnterEditMode, 'Alt+E', 'KeyE', {alt: true}));
    }
  }

  //#endregion

  //#region actions

  private static async openForEditing() {
    await openMarkedCard(
      'kr_calculate_permissions',
      null, // Не требуем подтверждения действия, если не было изменений
      () => showConfirmWithCancel('$KrTiles_EditModeConfirmation')
    );
  }

  //#endregion

}