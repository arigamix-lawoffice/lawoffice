import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { IStorage } from 'tessa/platform/storage';
import { ValidationStorageResultBuilder } from 'tessa/platform/validation';
export declare class CardResponseBase extends CardInfoStorageObject {
    constructor(storage?: IStorage);
    static readonly validationResultKey: string;
    get validationResult(): ValidationStorageResultBuilder;
    set validationResult(value: ValidationStorageResultBuilder);
    tryGetValidationResult(): ValidationStorageResultBuilder | null | undefined;
    clean(): void;
}
