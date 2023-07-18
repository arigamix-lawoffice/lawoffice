import { IStorage } from 'tessa/platform/storage';
export declare class NumberType {
    constructor(id: guid, name: string);
    readonly id: guid;
    readonly name: string;
    readonly info: IStorage;
}
