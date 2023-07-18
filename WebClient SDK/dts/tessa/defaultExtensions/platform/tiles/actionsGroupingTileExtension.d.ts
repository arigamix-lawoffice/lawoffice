import { TileExtension, ITileLocalExtensionContext, ITilePanelExtensionContext } from 'tessa/ui/tiles';
/**
 * Расширение, группирующее плитки в группу "Действия".
 */
export declare class ActionsGroupingTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    openingLocal(context: ITilePanelExtensionContext): void;
}
