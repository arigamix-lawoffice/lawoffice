import { ItemProperty, ItemPropertySealed, PropertyState } from 'tessa/views/workplaces/properties';
import { JsonWorkplaceComponentMetadata } from './jsonWorkplaceComponentMetadata';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
export declare class JsonItemPropertyFactory implements IStorageValueFactory<JsonItemProperty> {
    getValue(storage: IStorage): JsonItemProperty;
    getValueAndStorage(): {
        value: JsonItemProperty;
        storage: IStorage;
    };
}
export declare class JsonItemProperty extends JsonWorkplaceComponentMetadata {
    constructor(metadata?: ItemProperty | ItemPropertySealed, storage?: IStorage);
    static readonly propertyNameKey: string;
    static readonly propertyStateKey: string;
    static readonly valueKey: string;
    get propertyName(): string | null;
    set propertyName(value: string | null);
    get propertyState(): PropertyState;
    set propertyState(value: PropertyState);
    get value(): string | null;
    set value(value: string | null);
}
