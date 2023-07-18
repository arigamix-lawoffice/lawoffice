import { CardRequestBase } from './cardRequestBase';
import { IStorage } from 'tessa/platform/storage';
export declare abstract class CardFileRequestBase extends CardRequestBase {
    constructor(storage?: IStorage);
    static readonly fileIdKey: string;
    static readonly fileNameKey: string;
    static readonly fileTypeIdKey: string;
    static readonly fileTypeNameKey: string;
    get fileId(): guid | null;
    set fileId(value: guid | null);
    get fileName(): string | null;
    set fileName(value: string | null);
    get fileTypeId(): guid | null;
    set fileTypeId(value: guid | null);
    get fileTypeName(): string | null;
    set fileTypeName(value: string | null);
    clean(): void;
}
