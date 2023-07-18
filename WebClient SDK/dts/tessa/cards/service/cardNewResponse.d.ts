import { CardValueResponseBase } from './cardValueResponseBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardNewResponse extends CardValueResponseBase implements ICloneable<CardNewResponse> {
    constructor(storage?: IStorage);
    static readonly cancelOpeningSystemKey: string;
    get cancelOpening(): boolean;
    set cancelOpening(value: boolean);
    clone(): CardNewResponse;
}
