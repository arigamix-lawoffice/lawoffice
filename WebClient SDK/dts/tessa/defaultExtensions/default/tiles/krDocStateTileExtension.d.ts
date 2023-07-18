import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class KrDocStateTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static deleteKrDocStateFromViewAsync;
    private static deleteKrDocStateFromViewEvaluating;
}
