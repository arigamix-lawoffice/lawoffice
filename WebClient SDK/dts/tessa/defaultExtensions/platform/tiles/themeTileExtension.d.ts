import { TileExtension, ITileGlobalExtensionContext, ITilePanelExtensionContext } from 'tessa/ui/tiles';
export declare class ThemeTileExtension extends TileExtension {
    private static _wallpapersCache;
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    openingLocal(context: ITilePanelExtensionContext): void;
    private static createWallpaperTile;
    private static setTheme;
    private static setWallpapper;
}
