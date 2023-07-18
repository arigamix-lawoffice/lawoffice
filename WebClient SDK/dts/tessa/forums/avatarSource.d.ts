import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class AvatarSource extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly cardIdKey: string;
    static readonly cardTypeIdKey: string;
    static readonly fileIdKey: string;
    static readonly versionRowIdKey: string;
    static readonly fileNameKey: string;
    get cardId(): string | null;
    set cardId(value: string | null);
    get cardTypeId(): string | null;
    set cardTypeId(value: string | null);
    get fileId(): string | null;
    set fileId(value: string | null);
    get versionRowId(): string | null;
    set versionRowId(value: string | null);
    get fileName(): string | null;
    set fileName(value: string | null);
}
