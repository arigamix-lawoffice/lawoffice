import { CardFileRequestBase } from './cardFileRequestBase';
import { IStorage } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
import { CardGetFileContentRequest } from './cardGetFileContentRequest';
export declare class CardGetFileTemplateRequest extends CardFileRequestBase implements ICloneable<CardGetFileTemplateRequest> {
    constructor(storage?: IStorage);
    static readonly requestKey: string;
    static readonly viewContextKey: string;
    static readonly exportInfoKey: string;
    static readonly currentCardIDKey: string;
    static readonly userInfoKey: string;
    get request(): CardGetFileContentRequest;
    set request(value: CardGetFileContentRequest);
    get viewContext(): IStorage;
    set viewContext(value: IStorage);
    get exportInfo(): IStorage;
    set exportInfo(value: IStorage);
    get userInfo(): IStorage;
    set userInfo(value: IStorage);
    get currentCardID(): guid | null;
    set currentCardID(value: guid | null);
    clone(): CardGetFileTemplateRequest;
}
