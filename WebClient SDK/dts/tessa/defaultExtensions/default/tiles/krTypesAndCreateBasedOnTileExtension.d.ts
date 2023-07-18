import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
export declare class KrTypesAndCreateBasedOnTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static enableIfHasDocumentCommonInfoAndNotCreating;
    private static enableIfNotInSpecialModeAndNotCreating;
    private static initializeKrTilesGlobal;
    private static countTypes;
    private static addDocTypeTiles;
    private static initializeKrCreateBasedOnTiles;
    private static createBasedOnTilesAndGroup;
    private static createBasedOnTilesWithoutGrouping;
    private static createBasedOnDocTypeTile;
    private static createBasedOnCardTypeTile;
    private static createBasedOnTileAction;
    private static getCardTypeGroupLocalizedCaption;
}
