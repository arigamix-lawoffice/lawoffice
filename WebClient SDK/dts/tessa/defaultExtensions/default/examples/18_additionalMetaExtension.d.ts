import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
export declare class AdditionalMetaInitializationExtension extends ApplicationExtension {
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
}
export declare class AdditionalMetaTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
}
