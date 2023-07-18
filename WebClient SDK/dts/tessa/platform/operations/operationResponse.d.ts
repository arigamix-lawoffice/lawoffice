import { ValidationInfoStorageObject, ValidationStorageResultBuilder } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class OperationResponse extends ValidationInfoStorageObject {
    constructor(storage?: IStorage);
    static validationResultKey: string;
    get validationResult(): ValidationStorageResultBuilder;
    set validationResult(value: ValidationStorageResultBuilder);
    tryGetValidationResult(): ValidationStorageResultBuilder | null | undefined;
}
