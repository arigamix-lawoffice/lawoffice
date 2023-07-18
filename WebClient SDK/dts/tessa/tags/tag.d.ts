import { IStorage } from 'tessa/platform/storage';
export declare class Tag {
    constructor(storage?: IStorage);
    tagId: guid;
    cardId: guid;
    userId: guid;
    setAt: string;
    isCommon: boolean;
    getStorage(): IStorage;
}
