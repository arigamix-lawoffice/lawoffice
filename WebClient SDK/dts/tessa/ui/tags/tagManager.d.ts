import { Tag, TagInfo } from 'tessa/tags';
import { ValidationResult } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class TagManager {
    private constructor();
    private static _instance;
    static get instance(): TagManager;
    deleteTag(cardId: guid, tagId: guid): Promise<ValidationResult>;
    storeTag(tag: Tag, tokenInfo?: IStorage | null): Promise<ValidationResult>;
    getTags(cardId: string): Promise<TagInfo[]>;
}
