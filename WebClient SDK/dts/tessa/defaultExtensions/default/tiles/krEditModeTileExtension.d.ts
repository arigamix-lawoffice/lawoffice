import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class KrEditModeTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static openForEditing;
}
