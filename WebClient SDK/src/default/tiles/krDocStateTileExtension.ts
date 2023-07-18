import { TileExtension, ITileGlobalExtensionContext, Tile, TileGroups, TileEvaluationEventArgs, TileHotkey, ITileLocalExtensionContext } from 'tessa/ui/tiles';
import { UIContext } from 'tessa/ui';
import { DeleteIntegerCardOperation } from 'tessa/ui/cards';
import { createTypedField, DotNetType } from 'tessa/platform';
import { userSession } from 'common/utility';

export class KrDocStateTileExtension extends TileExtension {

  public initializingGlobal(context: ITileGlobalExtensionContext) {
    const panel = context.workspace.leftPanel;

    const viewsOther = panel.tryGetTile('ViewsOther');
    if (viewsOther) {
      const deleteCardFromView = viewsOther.tryGetTile('DeleteCardFromView');

      viewsOther.tiles.push(
        new Tile({
          name: 'DeleteKrDocStateFromView',
          caption: '$UI_Tiles_DeleteCardFromView',
          icon: 'icon-thin-059',
          contextSource: panel.contextSource,
          group: TileGroups.Views,
          order: deleteCardFromView ? deleteCardFromView.order : 100,
          command: KrDocStateTileExtension.deleteKrDocStateFromViewAsync,
          evaluating: KrDocStateTileExtension.deleteKrDocStateFromViewEvaluating,
          toolTip: '$UI_Tiles_DeleteCardFromView_ToolTip'
        })
      );
    }
  }

  public initializingLocal(context: ITileLocalExtensionContext) {
    const panel = context.workspace.leftPanel;
    if (!panel.context.viewContext) {
      return;
    }

    const hotkeyStorage = panel.contextSource.hotkeyStorage;

    const viewsOther = panel.tryGetTile('ViewsOther');
    if (!viewsOther) {
      return;
    }

    const deleteKrDocStateFromView = viewsOther.tryGetTile('DeleteKrDocStateFromView');
    if (deleteKrDocStateFromView) {
      hotkeyStorage.addTileHotkey(new TileHotkey(deleteKrDocStateFromView, 'Ctrl+D', 'KeyD', { ctrl: true }));
      if (userSession.isAdmin) {
        hotkeyStorage.addTileHotkey(
          new TileHotkey(deleteKrDocStateFromView, 'Ctrl+Alt+D', 'KeyD', { ctrl: true, alt: true })
        );
      }
    }
  }

  private static async deleteKrDocStateFromViewAsync() {
    const viewContext = UIContext.current.viewContext;
    if (viewContext) {
      // не включаем режим принудительного удаления без возможности восстановления, т.к. виртуальная карточка
      // и так удаляется без восстановления, но режим принудительного удаления отображает другие предупреждения перед удалением
      const withoutBackupOnly = false;

      const operation = new DeleteIntegerCardOperation(
        (request, item) => request.info['StateID'] = createTypedField(item.cardId, DotNetType.Int32),
        _item => 'e83a230a-f5fc-445e-9b44-7d0140ee69f6', // KrDocStateTypeID
        withoutBackupOnly
      );

      await operation.startAsync(viewContext);
    }

  }

  private static deleteKrDocStateFromViewEvaluating(e: TileEvaluationEventArgs) {
    const viewContext = e.currentTile.context.viewContext;

    e.setIsEnabledWithCollapsing(e.currentTile,
      !!viewContext
      && !!viewContext.view
      && viewContext.view.metadata.alias === 'KrDocStateCards'
    );
  }

}