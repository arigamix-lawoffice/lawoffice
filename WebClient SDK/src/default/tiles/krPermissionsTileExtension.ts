import { TileExtension, ITileGlobalExtensionContext, Tile, TileEvaluationEventArgs } from 'tessa/ui/tiles';
import { ICardModel } from 'tessa/ui/cards';
import { UIContext } from 'tessa/ui';
import { systemKeyPrefix } from 'tessa/cards';
import { createTypedField, DotNetType } from 'tessa/platform';

export class KrPermissionsTileExtension extends TileExtension {

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const panel = context.workspace.leftPanel;

    panel.tiles.push(
      new Tile({
        name: 'KrPermissionsDropCache',
        caption: '$KrTiles_DropPermissionsCache',
        icon: 'icon-thin-057',
        contextSource: panel.contextSource,
        command: this.tileAction,
        order: 10,
        evaluating: this.krPermissionsTileEvaluating
      })
    );
  }

  private krPermissionsTileEvaluating = (e: TileEvaluationEventArgs) => {
    const editor = e.currentTile.context.cardEditor;
    let model: ICardModel;

    e.setIsEnabledWithCollapsing(
      e.currentTile,
      !!editor
      && !!(model = editor.cardModel!)
      && model.cardType.id === 'fa9dbdac-8708-41df-bd72-900f69655dfa'); // KrPermissionsTypeID
  }

  private tileAction = async () => {
    const context = UIContext.current;
    const editor = UIContext.current.cardEditor;

    if (!editor) {
      return;
    }

    await editor.saveCard(
      context,
      {
        [systemKeyPrefix + 'DropPermissionsCache']: createTypedField(true, DotNetType.Boolean)
      }
    );
  }

}