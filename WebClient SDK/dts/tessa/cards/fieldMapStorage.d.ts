import { DotNetType, TypedField } from 'tessa/platform';
import { MapStorage, IStorage, StorageObjectStateProvider } from 'tessa/platform/storage';
import { ValidationResult } from 'tessa/platform/validation';
import { EventHandler } from 'tessa/platform/eventHandler';
export interface ICardFieldContainer {
    readonly fieldChanged: EventHandler<(args: CardFieldChangedEventArgs) => void>;
}
export interface CardFieldChangedEventArgs {
    readonly storage: FieldMapStorage;
    readonly fieldName: string;
    readonly fieldValue: any | null;
    readonly fieldTypedValue: TypedField | null | undefined;
}
export declare class FieldMapStorage extends MapStorage<TypedField | null> implements ICardFieldContainer {
    constructor(storage: IStorage);
    protected _stateProvider: StorageObjectStateProvider;
    protected _changedAction?: () => void;
    protected _fieldValidationResult: Map<string, ValidationResult>;
    get fieldValidationResult(): Map<string, ValidationResult>;
    protected getFieldValue(field: TypedField | null | undefined): any;
    protected setFieldChanged(key: string): void;
    protected resetFieldChanged(key: string): void;
    protected triggerItemsChanged(fieldName: string, _fieldValue: TypedField | null | undefined): void;
    init(stateProvider: StorageObjectStateProvider, changedAction?: () => void): void;
    getField(key: string): TypedField | null | undefined;
    tryGetField(key: string): TypedField | null | undefined;
    add(key: string): TypedField;
    add(key: string, item: TypedField): TypedField;
    tryGet(key: string, defaultValue?: any): any | null | undefined;
    clear(): void;
    delete(key: string): boolean;
    get(key: string): any | null | undefined;
    set(key: string, value: TypedField | null): this;
    set(key: string, value: any, type: DotNetType): this;
    rawSet(key: string, value: TypedField): this;
    rawSet(key: string, value: null): this;
    rawSet(key: string, value: any, type: DotNetType): this;
    systemSet(key: string, value: TypedField): this;
    systemSet(key: string, value: null): this;
    systemSet(key: string, value: any, type: DotNetType): this;
    protected internalSet(onStorageSet: (key: string, newValue: TypedField | null) => void, key: string, value: TypedField | null | any, type?: DotNetType): this;
    readonly fieldChanged: EventHandler<(args: CardFieldChangedEventArgs) => void>;
}
