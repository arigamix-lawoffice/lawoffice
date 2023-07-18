export interface IStorage<T = any> {
    [key: string]: T;
}
export interface IStorageArray<T = any> extends Array<T> {
    [key: number]: T;
}
