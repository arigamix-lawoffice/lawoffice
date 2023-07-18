import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { IStorage, MapStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
import { CardHeaderFile } from './cardHeaderFile';
export declare class CardHeader extends CardInfoStorageObject implements ICloneable<CardHeader> {
    constructor(storage?: IStorage);
    private static readonly _cardHeaderFilesFactory;
    static readonly filesKey: string;
    static readonly operationIDKey: string;
    get files(): MapStorage<CardHeaderFile> | null;
    set files(value: MapStorage<CardHeaderFile> | null);
    get operationID(): guid | null;
    set operationID(value: guid | null);
    tryGetFiles(): MapStorage<CardHeaderFile> | null | undefined;
    clone(): CardHeader;
}
