import { CardStorageObject } from './cardStorageObject';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardFileContentSource extends CardStorageObject implements ICloneable<CardFileContentSource> {
    constructor(storage?: IStorage);
    static readonly cardIdKey = "CardID";
    static readonly cardTypeIdKey = "CardTypeID";
    static readonly fileIdKey = "FileID";
    static readonly versionRowIdKey = "VersionRowID";
    static readonly storeSourceKey = "StoreSource";
    static readonly originalVersionRowIdKey = "OriginalVersionRowID";
    get cardId(): guid;
    set cardId(value: guid);
    get cardTypeId(): guid;
    set cardTypeId(value: guid);
    get fileId(): guid;
    set fileId(value: guid);
    get versionRowId(): guid;
    set versionRowId(value: guid);
    get storeSource(): number;
    set storeSource(value: number);
    get originalVersionRowId(): guid | null;
    set originalVersionRowId(value: guid | null);
    clone(): CardFileContentSource;
}
