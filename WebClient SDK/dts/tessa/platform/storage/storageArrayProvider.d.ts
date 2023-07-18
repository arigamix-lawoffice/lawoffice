import { IStorageArray } from './storage';
import { IStorageProvider } from './storageProvider';
export interface IStorageArrayProvider extends IStorageProvider {
    getStorage(): IStorageArray;
}
export declare function isIStorageArrayProvider(object: any): boolean;
