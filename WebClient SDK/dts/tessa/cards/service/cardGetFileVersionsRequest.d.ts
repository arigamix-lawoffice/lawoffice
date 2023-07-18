import { CardFileRequestBase } from './cardFileRequestBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
export declare class CardGetFileVersionsRequest extends CardFileRequestBase implements ICloneable<CardGetFileVersionsRequest> {
    constructor(storage?: IStorage);
    static readonly systemMethodKey: string;
    get method(): number;
    set method(value: number);
    clean(): void;
    clone(): CardGetFileVersionsRequest;
}
