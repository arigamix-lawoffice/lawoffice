import { TileExtension, ITileGlobalExtensionContext, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class StageSourceBuildTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
}
