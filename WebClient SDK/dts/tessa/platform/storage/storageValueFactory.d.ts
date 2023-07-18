import { IStorage, IStorageArray } from './storage';
export interface IStorageValueFactory<V = any> {
    getValue(storage: IStorage | IStorageArray): V;
    getValueAndStorage(): {
        value: V;
        storage: IStorage | IStorageArray;
    };
}
export declare function isIStorageValueFactory(object: any): boolean;
export interface IKeyedStorageValueFactory<K = string, V = any> {
    getValue(key: K, storage: IStorage | IStorageArray): V;
    getValueAndStorage(key: K): {
        value: V;
        storage: IStorage | IStorageArray;
    };
}
export declare function isIKeyedStorageValueFactory(object: any): boolean;
export declare class StorageValueFactory<S = IStorage | IStorageArray, V = any> implements IStorageValueFactory<V> {
    private _createStorage;
    private _createFunc;
    constructor(_createStorage: () => S, _createFunc: (value: S) => V);
    getValue(storage: S): V;
    getValueAndStorage(): {
        value: V;
        storage: S;
    };
}
export declare class KeyedStorageValueFactory<S = IStorage | IStorageArray, K = string, V = any> implements IKeyedStorageValueFactory<K, V> {
    private _createStorage;
    private _createFunc;
    constructor(_createStorage: () => S, _createFunc: (key: K, value: S) => V);
    getValue(key: K, storage: S): V;
    getValueAndStorage(key: K): {
        value: V;
        storage: S;
    };
}
