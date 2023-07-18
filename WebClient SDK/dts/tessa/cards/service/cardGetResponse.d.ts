import { CardValueResponseBase } from './cardValueResponseBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
import { Card } from 'tessa/cards';
export declare class CardGetResponse extends CardValueResponseBase implements ICloneable<CardGetResponse> {
    constructor(storage?: IStorage);
    static readonly compressedSystemKey: string;
    static readonly cancelOpeningSystemKey: string;
    get compressed(): boolean;
    set compressed(value: boolean);
    get cancelOpening(): boolean;
    set cancelOpening(value: boolean);
    protected createDefaultCard(storage: IStorage): Card;
    clone(): CardGetResponse;
}
