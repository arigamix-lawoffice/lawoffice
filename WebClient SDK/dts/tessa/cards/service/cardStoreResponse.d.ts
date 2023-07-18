import { CardResponseBase } from './cardResponseBase';
import { IStorage } from 'tessa/platform/storage';
export declare class CardStoreResponse extends CardResponseBase {
    constructor(storage?: IStorage);
    static readonly cardIdKey: string;
    static readonly cardTypeIdKey: string;
    static readonly cardVersionKey: string;
    static readonly storeDateTimeKey: string;
    get cardId(): guid;
    set cardId(value: guid);
    get cardTypeId(): guid;
    set cardTypeId(value: guid);
    get cardVersion(): number;
    set cardVersion(value: number);
    get storeDateTime(): string | null;
    set storeDateTime(value: string | null);
    clone(): CardStoreResponse;
}
