import { CardStorageObject } from 'tessa/cards';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { TypedField } from 'tessa/platform';
import { CriteriaValue, ViewParameterMetadataSealed } from '../metadata';
export declare class PlainCriteriaValueFactory implements IStorageValueFactory<PlainCriteriaValue> {
    getValue(storage: IStorage): PlainCriteriaValue;
    getValueAndStorage(): {
        value: PlainCriteriaValue;
        storage: IStorage;
    };
}
export declare class PlainCriteriaValue extends CardStorageObject {
    static CreateFromCriteriaValue(value: CriteriaValue, metadata: ViewParameterMetadataSealed): PlainCriteriaValue;
    constructor(storage?: IStorage);
    static readonly valueKey: string;
    static readonly textKey: string;
    static readonly executorParameterNameKey: string;
    static readonly readOnlyKey: string;
    get value(): TypedField | null;
    set value(value: TypedField | null);
    get text(): string | null;
    set text(value: string | null);
    get executorParameterName(): string | null;
    set executorParameterName(value: string | null);
    get readOnly(): boolean;
    set readOnly(value: boolean);
    setTypedValue(value: any, metadata: ViewParameterMetadataSealed): void;
}
export declare class RequestParameterHelper {
    static GetValue(criteriaValue: any, metadata: ViewParameterMetadataSealed): TypedField | null;
}
