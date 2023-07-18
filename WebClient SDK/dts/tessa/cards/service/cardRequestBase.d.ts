import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { IStorage } from 'tessa/platform/storage';
export declare abstract class CardRequestBase extends CardInfoStorageObject {
    constructor(storage?: IStorage);
    static readonly cardIdKey: string;
    static readonly cardTypeIdKey: string;
    static readonly cardTypeNameKey: string;
    get cardId(): guid | null;
    set cardId(value: guid | null);
    get cardTypeId(): guid | null;
    set cardTypeId(value: guid | null);
    get cardTypeName(): string | null;
    set cardTypeName(value: string | null);
    clean(): void;
}
