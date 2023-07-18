import { IValidationResultBuilder } from 'tessa/platform/validation';
import { ISupportUnloading } from 'tessa/ui/supportUnloading';
export declare abstract class SupportUnloadingViewModel implements ISupportUnloading {
    private _isUnloaded;
    protected onUnloading(_validationResult: IValidationResultBuilder): void;
    get isUnloaded(): boolean;
    unload(validationResult: IValidationResultBuilder): void;
}
