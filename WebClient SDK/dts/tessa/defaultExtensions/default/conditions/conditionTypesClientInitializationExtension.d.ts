import { ApplicationExtension } from 'tessa/applicationExtension';
import { IApplicationExtensionMetadataContext } from 'tessa/applicationExtensionContext';
export declare class ConditionTypesClientInitializationExtension extends ApplicationExtension {
    afterMetadataReceived(_context: IApplicationExtensionMetadataContext): Promise<void>;
}
