import { IItemProperties, ItemPropertiesSealed, PropertyState } from 'tessa/views/workplaces/properties';
import { JsonWorkplaceComponentMetadata } from './jsonWorkplaceComponentMetadata';
import { ArrayStorage, IStorage, IStorageValueFactory } from 'tessa/platform/storage';
import { JsonItemProperty } from './jsonItemProperty';
export declare class JsonItemPropertiesFactory implements IStorageValueFactory<JsonItemProperties> {
    getValue(storage: IStorage): JsonItemProperties;
    getValueAndStorage(): {
        value: JsonItemProperties;
        storage: IStorage;
    };
}
export declare class JsonItemProperties extends JsonWorkplaceComponentMetadata {
    constructor(metadata?: IItemProperties | ItemPropertiesSealed, storage?: IStorage);
    static readonly propertyClassKey: string;
    static readonly propertyStateKey: string;
    static readonly itemsKey: string;
    private static readonly _jsonItemPropertyFactory;
    get propertyClass(): string | null;
    set propertyClass(value: string | null);
    get propertyState(): PropertyState;
    set propertyState(value: PropertyState);
    get items(): ArrayStorage<JsonItemProperty> | null;
    set items(value: ArrayStorage<JsonItemProperty> | null);
}
