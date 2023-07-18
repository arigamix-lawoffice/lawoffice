import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class DeletedTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static restoreAction;
    private static viewCardAction;
    private static repairDeletedAction;
}
