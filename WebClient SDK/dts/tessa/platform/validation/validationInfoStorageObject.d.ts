import { ValidationStorageObject } from './validationStorageObject';
import { IStorage, IStorageCachePolicyProvider, IStorageCleanable } from 'tessa/platform/storage';
export declare class ValidationInfoStorageObject extends ValidationStorageObject implements IStorageCachePolicyProvider, IStorageCleanable {
    constructor(storage: IStorage);
    static readonly infoKey = "Info";
    get info(): IStorage;
    set info(value: IStorage);
    tryGetInfo(): IStorage | null | undefined;
    removeUserInfo(): void;
    removeSystemInfo(): void;
    ensureCacheResolved(): void;
    clean(): void;
}
