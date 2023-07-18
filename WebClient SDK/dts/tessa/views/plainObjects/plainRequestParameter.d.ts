import { CardStorageObject } from 'tessa/cards';
import { ArrayStorage, IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { PlainRequestCriteria, PlainRequestCriteriaFactory } from './plainRequestCriteria';
import { RequestParameter } from '../metadata';
export declare class PlainRequestParameterFactory implements IStorageValueFactory<PlainRequestParameter> {
    getValue(storage: IStorage): PlainRequestParameter;
    getValueAndStorage(): {
        value: PlainRequestParameter;
        storage: IStorage;
    };
}
export declare class PlainRequestParameter extends CardStorageObject {
    static CreateFromRequestParameter(parameter: RequestParameter): PlainRequestParameter;
    constructor(storage?: IStorage);
    static readonly nameKey: string;
    static readonly readOnlyKey: string;
    static readonly criteriaValuesKey: string;
    static readonly _requestParameterFactory: PlainRequestCriteriaFactory;
    get name(): string | null;
    set name(value: string | null);
    get readOnly(): boolean;
    set readOnly(value: boolean);
    get criteriaValues(): ArrayStorage<PlainRequestCriteria> | null;
    set criteriaValues(value: ArrayStorage<PlainRequestCriteria> | null);
}
