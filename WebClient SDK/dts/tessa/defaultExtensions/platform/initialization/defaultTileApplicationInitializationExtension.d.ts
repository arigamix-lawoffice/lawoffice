import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
export declare class DefaultTileApplicationInitializationExtension extends ApplicationExtension {
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
    private initTiles;
}
