import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Запрещает системные плитки "Удалить", "Экспорт" и "Показать структуру" для типовых представлений,
 * перечисленных в DefaultViewAliases.
 */
export declare class ProhibitTilesInViewsTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static setEnabledWithCollapsingInViewContext;
    private static deleteEvaluating;
    private static exportEvaluating;
    private static viewCardStorageEvaluating;
}
