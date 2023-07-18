import { TileExtension, ITileGlobalExtensionContext, ITilePanelExtensionContext, ITile } from 'tessa/ui/tiles';
export declare class HelpTileExtension extends TileExtension {
    aboutTile: ITile | undefined;
    aboutTileCaptionPrefix: string;
    timer: number | null;
    private updateAboutTileCaption;
    private timerHandler;
    private aboutAction;
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    openingLocal(context: ITilePanelExtensionContext): void;
    closedLocal(_context: ITilePanelExtensionContext): void;
}
