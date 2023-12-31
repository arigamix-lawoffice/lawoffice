import { IStorage, IStorageArray } from './storage';
import { IStorageCachePolicyProvider } from './storageCachePolicyProvider';
import { IStorageMapProvider } from './storageMapProvider';
import { IStorageProvider } from './storageProvider';
export declare abstract class StorageObject implements IStorageMapProvider, IStorageCachePolicyProvider {
    constructor(storage: IStorage);
    private readonly _storage;
    private readonly _cachedMembers;
    protected has(key: string): boolean;
    protected get<T>(key: string, defaultValueFunc?: () => T): T;
    protected getValue<T>(key: string, defaultValueFunc?: () => T): T;
    protected getMap<T>(key: string, defaultValueFunc: (storage: IStorage) => T): T;
    protected getArray<T>(key: string, defaultValueFunc: (storage: IStorageArray) => T): T;
    protected tryGet<T>(key: string, getValueFunc?: (storage: IStorage | IStorageArray) => T): T | null | undefined;
    protected tryGetValue<T>(key: string, getValueFunc?: (storage: IStorage | IStorageArray) => T): T | null | undefined;
    protected tryGetMap<T>(key: string, getValueFunc: (storage: IStorage) => T): T | null | undefined;
    protected tryGetArray<T>(key: string, getValueFunc: (storage: IStorageArray) => T): T | null | undefined;
    protected set<T>(key: string, value: T): void;
    protected setStorageValue(key: string, value: IStorageProvider | null): void;
    protected delete(key: string): void;
    protected remove(key: string): void;
    protected init<T>(key: string, value: T): void;
    protected initNotNull<T>(key: string, value: T): void;
    protected setNull(key: string): void;
    protected setNullIfEmptyEnumerable(key: string): void;
    protected cleanCollectionAndSetNullIfEmpty(key: string): void;
    protected clearCache(): void;
    protected setStorage(storage: IStorage): any;
    protected setStorage(storage: IStorageMapProvider): any;
    private setStorageInternal;
    getStorage(): IStorage;
    ensureCacheResolved(): void;
}
