import { CardRequestBase } from './cardRequestBase';
import { CardDeletionMode, CardDeleteMethod } from 'tessa/cards';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardDeleteRequest extends CardRequestBase implements ICloneable<CardDeleteRequest> {
    constructor(storage?: IStorage);
    static readonly systemDeletionModeKey: string;
    static readonly systemMethodKey: string;
    static readonly keepFileContentKey: string;
    get mode(): CardDeletionMode;
    set mode(value: CardDeletionMode);
    get method(): CardDeleteMethod;
    set method(value: CardDeleteMethod);
    get keepFileContent(): boolean;
    set keepFileContent(value: boolean);
    clone(): CardDeleteRequest;
}
