import { TagInfo } from 'tessa/tags/tagInfo';
import { IStorage } from 'tessa/platform/storage';
export declare class TagsForCard {
    constructor(storage?: IStorage);
    tags: TagInfo[];
    getStorage(): IStorage;
    static pack(tagsForCard: TagsForCard, storage: IStorage): void;
    static tryUnpack(storage: IStorage | null | undefined): TagsForCard | null;
}
