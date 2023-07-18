import { IStorage, StorageObject } from 'tessa/platform/storage';
export declare class PatchInfo extends StorageObject {
    constructor(storage?: IStorage);
    static readonly nameKey: string;
    static readonly dateKey: string;
    get name(): string;
    get date(): string;
}
