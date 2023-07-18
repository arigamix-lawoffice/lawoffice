import { ConfigurationMetadata, ServiceWorkerMetadata } from 'tessa/platform';
export interface CommonMetadata {
    cryptoProPluginEnabled: boolean;
    maxWallpaperSizeB: number;
    previewPdfEnabled: boolean;
    maxFileSizeB: number;
    denyFileDownload: boolean;
    createBasedOnTypes: ReadonlyArray<guid>;
    configuration: ConfigurationMetadata;
    serviceWorkerInfo: ServiceWorkerMetadata;
    unavailableTypes: readonly guid[];
}
