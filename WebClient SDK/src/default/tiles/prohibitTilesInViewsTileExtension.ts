import { TileExtension, ITileLocalExtensionContext, TileEvaluationEventArgs } from 'tessa/ui/tiles';
import { ITessaView } from 'tessa/views';
import { canDeleteCardFromViewDefault, canExportCardFromViewDefault,
  canViewCardStorageFromViewDefault } from './defaultViewAliases';

/**
 * Запрещает системные плитки "Удалить", "Экспорт" и "Показать структуру" для типовых представлений,
 * перечисленных в DefaultViewAliases.
 */
export class ProhibitTilesInViewsTileExtension extends TileExtension {

  //#region TileExtension

  public initializingLocal(context: ITileLocalExtensionContext) {
    const others = context.workspace.leftPanel.tryGetTile('ViewsOther');
    if (others) {
      const deleteTile = others.tryGetTile('DeleteCardFromView');
      if (deleteTile) {
        deleteTile.evaluating.add(ProhibitTilesInViewsTileExtension.deleteEvaluating);
      }

      const exportTile = others.tryGetTile('ExportCardFromView');
      if (exportTile) {
        exportTile.evaluating.add(ProhibitTilesInViewsTileExtension.exportEvaluating);
      }

      const exportAllTile = others.tryGetTile('ExportAllCardsFromView');
      if (exportAllTile) {
        exportAllTile.evaluating.add(ProhibitTilesInViewsTileExtension.exportEvaluating);
      }

      const viewStorageTile = others.tryGetTile('ViewStorageFromView');
      if (viewStorageTile) {
        viewStorageTile.evaluating.add(ProhibitTilesInViewsTileExtension.viewCardStorageEvaluating);
      }
    }
  }

  //#endregion

  //#region methods

  private static setEnabledWithCollapsingInViewContext(
    e: TileEvaluationEventArgs,
    canProcessViewFunc: (view: ITessaView) => boolean
  ) {
    const viewContext = e.currentTile.context.viewContext;

    let isEnabled = false;
    if (viewContext) {
      const view = viewContext.view;
      const canExecute = !!view
        && Array.from(view.metadata.references.values())
          .some(x => x.openOnDoubleClick && x.isCard)
        && !!viewContext.selectedRow
        && canProcessViewFunc(view);

      isEnabled = canExecute;
    }

    e.setIsEnabledWithCollapsing(e.currentTile, isEnabled);
  }

  private static deleteEvaluating = (e: TileEvaluationEventArgs) =>
    ProhibitTilesInViewsTileExtension.setEnabledWithCollapsingInViewContext(
      e, view => canDeleteCardFromViewDefault(view.metadata.alias));

  private static exportEvaluating = (e: TileEvaluationEventArgs) =>
    ProhibitTilesInViewsTileExtension.setEnabledWithCollapsingInViewContext(
      e, view => canExportCardFromViewDefault(view.metadata.alias));

  private static viewCardStorageEvaluating = (e: TileEvaluationEventArgs) =>
    ProhibitTilesInViewsTileExtension.setEnabledWithCollapsingInViewContext(
      e, view => canViewCardStorageFromViewDefault(view.metadata.alias));

  //#endregion

}