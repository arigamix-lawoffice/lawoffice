import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
export declare class GlobalButtonsInitalizationExtension extends ApplicationExtension {
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
}
