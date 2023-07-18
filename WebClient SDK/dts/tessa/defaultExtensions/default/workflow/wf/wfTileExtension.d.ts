import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Плитки для бизнес-процессов Workflow.
 */
export declare class WfTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static createWfResolutionAction;
    private static enableOnCardIsNotTaskCard;
}
