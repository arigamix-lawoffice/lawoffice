import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
export declare class WorkflowGlobalButtonsInitializationExtension extends ApplicationExtension {
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
}
