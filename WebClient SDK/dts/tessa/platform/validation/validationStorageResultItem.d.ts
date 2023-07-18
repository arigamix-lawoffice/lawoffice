import { ValidationStorageObject } from './validationStorageObject';
import { ValidationResultType } from './validationResultType';
import { IValidationResultItem } from './validationResultItem';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
export declare class ValidationStorageResultItem extends ValidationStorageObject implements IValidationResultItem {
    constructor(storage?: IStorage);
    static readonly keyKey = "Key";
    static readonly typeKey = "Type";
    static readonly messageKey = "Message";
    static readonly fieldNameKey = "FieldName";
    static readonly objectNameKey = "ObjectName";
    static readonly objectTypeKey = "ObjectType";
    static readonly detailsKey = "Details";
    get key(): guid;
    set key(value: guid);
    get type(): ValidationResultType;
    set type(value: ValidationResultType);
    get message(): string;
    set message(value: string);
    get fieldName(): string;
    set fieldName(value: string);
    get objectName(): string;
    set objectName(value: string);
    get objectType(): string;
    set objectType(value: string);
    get details(): string;
    set details(value: string);
    isEmpty(): boolean;
}
export declare class ValidationStorageResultItemFactory implements IStorageValueFactory<ValidationStorageResultItem> {
    getValue(storage: IStorage): ValidationStorageResultItem;
    getValueAndStorage(): {
        value: ValidationStorageResultItem;
        storage: IStorage;
    };
}
