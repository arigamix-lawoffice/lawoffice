import { CardFileRequestBase } from './cardFileRequestBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
import { CardGetFileContentMethod } from 'tessa/cards/cardGetFileContentMethod';
export declare class CardGetFileContentRequest extends CardFileRequestBase implements ICloneable<CardGetFileContentRequest> {
    constructor(storage?: IStorage);
    static readonly versionRowIdKey: string;
    static readonly systemMethodKey: string;
    get versionRowId(): guid | null;
    set versionRowId(value: guid | null);
    get method(): CardGetFileContentMethod;
    set method(value: CardGetFileContentMethod);
    clone(): CardGetFileContentRequest;
}
