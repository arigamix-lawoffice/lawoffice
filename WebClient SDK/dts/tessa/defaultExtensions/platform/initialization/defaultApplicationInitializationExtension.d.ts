import { ApplicationExtension, IApplicationExtensionMetadataContext, IApplicationExtensionContext } from 'tessa';
declare global {
    interface Window {
        cadesplugin_skip_extension_install: boolean;
    }
}
export declare class DefaultApplicationInitializationExtension extends ApplicationExtension {
    initialize(): void;
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
    hidden(_context: IApplicationExtensionContext): void;
    restore(_context: IApplicationExtensionContext): void;
    private initPersonalRoleSatelliteId;
    private initFormattingSettingsCache;
    private initLocalization;
    private initLicense;
    private initCommonMeta;
    private initSingletons;
    private initExistentSingletons;
    private initKrTypesCache;
    private initViews;
    private initSearchQueries;
    private initWorkplaces;
    private initCards;
    private initTheme;
    private initWallpaper;
    private initUserSettings;
    private initPaletteSettingsManager;
}
