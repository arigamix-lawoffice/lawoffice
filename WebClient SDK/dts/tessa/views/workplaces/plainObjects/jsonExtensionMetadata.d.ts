import { ExtensionMetadata, ExtensionMetadataSealed } from 'tessa/views/workplaces';
import { CardStorageObject } from 'tessa/cards';
import { IStorage, IStorageValueFactory } from 'tessa/platform/storage';
export declare class JsonExtensionMetadataFactory implements IStorageValueFactory<JsonExtensionMetadata> {
    getValue(storage: IStorage): JsonExtensionMetadata;
    getValueAndStorage(): {
        value: JsonExtensionMetadata;
        storage: IStorage;
    };
}
export declare class JsonExtensionMetadata extends CardStorageObject {
    constructor(metadata?: ExtensionMetadata | ExtensionMetadataSealed, storage?: IStorage);
    static readonly typeNameKey: string;
    static readonly orderKey: string;
    static readonly dataKey: string;
    static readonly conditionKey: string;
    get typeName(): string | null;
    set typeName(value: string | null);
    get order(): number;
    set order(value: number);
    get data(): IStorage;
    set data(value: IStorage);
    get condition(): string | null;
    set condition(value: string | null);
}
