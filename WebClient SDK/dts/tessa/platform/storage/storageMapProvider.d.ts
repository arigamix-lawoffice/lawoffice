import { IStorage } from './storage';
import { IStorageProvider } from './storageProvider';
export interface IStorageMapProvider extends IStorageProvider {
    getStorage(): IStorage;
}
export declare function isIStorageMapProvider(object: any): boolean;
