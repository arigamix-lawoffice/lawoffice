import { ITileGlobalExtensionContext, ITileLocalExtensionContext, ITilePanelExtensionContext, TileExtension } from 'tessa/ui/tiles';
export declare class MySettingsTileExtension extends TileExtension {
    private static selectLanguage;
    private static selectFormat;
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    initializingLocal(context: ITileLocalExtensionContext): void;
    openingLocal(context: ITilePanelExtensionContext): void;
}
