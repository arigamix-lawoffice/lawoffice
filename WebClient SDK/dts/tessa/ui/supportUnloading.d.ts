import { IValidationResultBuilder } from 'tessa/platform/validation';
export interface ISupportUnloading {
    readonly isUnloaded: boolean;
    unload(validationResult: IValidationResultBuilder): any;
}
