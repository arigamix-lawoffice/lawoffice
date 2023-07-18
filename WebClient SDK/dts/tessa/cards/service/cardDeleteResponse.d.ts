import { CardResponseBase } from './cardResponseBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardDeleteResponse extends CardResponseBase implements ICloneable<CardDeleteResponse> {
    constructor(storage?: IStorage);
    static readonly systemDeletedCardIdKey: string;
    static readonly systemRestoredCardIdKey: string;
    static readonly cardTypeIdKey: string;
    static readonly cardTypeNameKey: string;
    get deletedCardId(): guid | null;
    set deletedCardId(value: guid | null);
    get restoredCardId(): guid | null;
    set restoredCardId(value: guid | null);
    get cardTypeId(): guid | null;
    set cardTypeId(value: guid | null);
    get cardTypeName(): string | null;
    set cardTypeName(value: string | null);
    clean(): void;
    clone(): CardDeleteResponse;
}
