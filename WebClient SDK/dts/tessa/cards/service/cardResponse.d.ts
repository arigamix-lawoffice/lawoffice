import { CardResponseBase } from './cardResponseBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardResponse extends CardResponseBase implements ICloneable<CardResponse> {
    constructor(storage?: IStorage);
    clone(): CardResponse;
}
