import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext, ITilePanelExtensionContext } from 'tessa/ui/tiles';
/**
 * Иерархия плиток для создания карточек различных типов.
 */
export declare class CreateCardTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    openingLocal(context: ITilePanelExtensionContext): void;
    private static getTypeGroupCaption;
    private static createCardAction;
}
