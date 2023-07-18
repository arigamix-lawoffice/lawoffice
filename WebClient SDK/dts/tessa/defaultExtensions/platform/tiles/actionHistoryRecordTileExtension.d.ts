import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class ActionHistoryRecordTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static enableOnActionHistoryView;
    private static viewIsAllowed;
    private static openCardFromRecordAction;
}
