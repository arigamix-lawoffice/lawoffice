import { IStorage } from 'tessa/platform/storage';
export declare class ServiceWorkerMetadata {
    static readonly serviceWorkerErrorKey: string;
    private storage;
    constructor(storage?: IStorage);
    get viewsCachedTime(): string;
    get cardsCachedTime(): string;
    get serviceWorkerError(): string;
    set serviceWorkerError(value: string);
}
