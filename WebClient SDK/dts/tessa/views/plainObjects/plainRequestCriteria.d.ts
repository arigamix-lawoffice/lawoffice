import { CardStorageObject } from 'tessa/cards';
import { ArrayStorage, IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { PlainCriteriaValue, PlainCriteriaValueFactory } from './plainCriteriaValue';
import { RequestCriteria, ViewParameterMetadataSealed } from '../metadata';
export declare class PlainRequestCriteriaFactory implements IStorageValueFactory<PlainRequestCriteria> {
    getValue(storage: IStorage): PlainRequestCriteria;
    getValueAndStorage(): {
        value: PlainRequestCriteria;
        storage: IStorage;
    };
}
export declare class PlainRequestCriteria extends CardStorageObject {
    static CreateFromRequestParameter(criteria: RequestCriteria, metadata: ViewParameterMetadataSealed): PlainRequestCriteria;
    constructor(storage?: IStorage);
    static readonly criteriaNameKey: string;
    static readonly readOnlyKey: string;
    static readonly valuesKey: string;
    static readonly _criteriaValueFactory: PlainCriteriaValueFactory;
    get criteriaName(): string | null;
    set criteriaName(value: string | null);
    get readOnly(): boolean;
    set readOnly(value: boolean);
    get values(): ArrayStorage<PlainCriteriaValue> | null;
    set values(value: ArrayStorage<PlainCriteriaValue> | null);
}
