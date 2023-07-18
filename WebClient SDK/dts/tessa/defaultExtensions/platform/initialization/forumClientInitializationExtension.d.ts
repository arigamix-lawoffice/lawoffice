import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
export declare class ForumClientInitializationExtension extends ApplicationExtension {
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
}
