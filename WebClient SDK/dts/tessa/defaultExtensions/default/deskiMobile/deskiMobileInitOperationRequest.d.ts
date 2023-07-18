import { IStorage, StorageObject } from 'tessa/platform/storage';
export declare class InitOperationRequest extends StorageObject {
    constructor(storage?: IStorage);
    private readonly cardIDKey;
    private readonly fileIDKey;
    private readonly versionRowIKey;
    private readonly fileNameKey;
    get cardID(): guid | null;
    get fileID(): guid | null;
    get versionRowID(): guid | null;
    get fileName(): string | null;
}
