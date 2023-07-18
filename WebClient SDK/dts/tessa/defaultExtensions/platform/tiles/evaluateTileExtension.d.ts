import { TileExtension, ITilePanelExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Выполняет изменение состояния и видимости всех плиток открываемой панели.
 */
export declare class EvaluateTileExtension extends TileExtension {
    openingLocal(context: ITilePanelExtensionContext): void;
}
/**
 * Выполняем расчет видимости плиток после всех инициализаций.
 * Нужно чтобы ui в sidePanel при рендере уже имел представление, сколько тайлов будет скрыто.
 */
export declare class PostInitializingLocalEvaluateTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
}
