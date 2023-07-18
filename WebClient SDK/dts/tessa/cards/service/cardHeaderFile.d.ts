import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { ArrayStorage, IKeyedStorageValueFactory, IStorage } from 'tessa/platform/storage';
import { DotNetType, TypedField, ICloneable } from 'tessa/platform';
export declare class CardHeaderFile extends CardInfoStorageObject implements ICloneable<CardHeaderFile> {
    constructor(id: guid, storage?: IStorage);
    id: guid;
    static readonly orderKey: string;
    static readonly sizeKey: string;
    static readonly hashKey: string;
    get order(): number;
    set order(value: number);
    get size(): number;
    set size(value: number);
    get hash(): ArrayStorage<TypedField<DotNetType.Byte, number>> | null;
    set hash(value: ArrayStorage<TypedField<DotNetType.Byte, number>> | null);
    tryGetHash(): ArrayStorage<TypedField<DotNetType.Byte, number>> | null | undefined;
    clone(): CardHeaderFile;
}
export declare class CardHeaderFileFactory implements IKeyedStorageValueFactory<guid, CardHeaderFile> {
    getValue(key: guid, storage: IStorage): CardHeaderFile;
    getValueAndStorage(key: guid): {
        value: CardHeaderFile;
        storage: IStorage;
    };
}
