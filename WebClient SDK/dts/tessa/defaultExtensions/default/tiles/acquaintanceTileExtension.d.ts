import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class AcquaintanceTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static enableOnCardUpdateAndNotTaskCard;
    private static showAcquaintanceWindow;
    private static openAcquaintanceHistoryView;
    private static canUseResolutions;
    private static typeSupportsWorkflow;
    private static notEnoughPermissions;
    private static openRolesDialog;
}
