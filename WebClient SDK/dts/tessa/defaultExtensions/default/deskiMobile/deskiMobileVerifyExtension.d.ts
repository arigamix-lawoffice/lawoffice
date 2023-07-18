import { IApplicationExtensionMetadataContext } from 'tessa';
import { ApplicationExtension } from 'tessa/applicationExtension';
export declare class DeskiMobileVerifyExtension extends ApplicationExtension {
    afterMetadataReceived(_context: IApplicationExtensionMetadataContext): Promise<void>;
    private verifyInDeskiMobile;
    private static getActualVerifyOperationResult;
}
