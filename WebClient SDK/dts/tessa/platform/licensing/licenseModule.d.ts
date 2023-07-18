import { IStorage } from '../storage';
export declare class LicenseModule {
    constructor(storage: IStorage);
    readonly id: guid;
    readonly caption: string;
    readonly settings: IStorage;
}
