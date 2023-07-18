import { CardNewMode, CardNewMethod } from 'tessa/cards';
import { CardInfoStorageObject } from 'tessa/cards/cardInfoStorageObject';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardNewRequest extends CardInfoStorageObject implements ICloneable<CardNewRequest> {
    constructor(storage?: IStorage);
    static readonly cardTypeIdKey: string;
    static readonly cardTypeNameKey: string;
    static readonly newModeKey: string;
    static readonly systemMethodKey: string;
    get cardTypeId(): guid | null;
    set cardTypeId(value: guid | null);
    get cardTypeName(): string | null;
    set cardTypeName(value: string | null);
    get newMode(): CardNewMode;
    set newMode(value: CardNewMode);
    get method(): CardNewMethod;
    set method(value: CardNewMethod);
    clean(): void;
    clone(): CardNewRequest;
}
