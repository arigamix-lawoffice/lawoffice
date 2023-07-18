import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
export declare class DeskiMobileApplicationInitializationExtension extends ApplicationExtension {
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
}
