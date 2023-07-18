import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class CreateMultipleTemplateTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private static typeIsAllowedForMultipleCreation;
    private static getTypeInfoForMultipleCreation;
    private static createMultipleCardsAction;
    private static tryCreateMultipleCardsAsync;
    private static tryLoadDirectory;
}
