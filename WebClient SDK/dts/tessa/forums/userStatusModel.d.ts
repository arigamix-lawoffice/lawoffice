import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class UserStatusModel extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly userIdKey: string;
    static readonly topicIdKey: string;
    static readonly lastReadMessageTimeKey: string;
    static readonly preLastVisitedAtKey: string;
    get userId(): guid | null;
    set userId(value: guid | null);
    get topicId(): guid | null;
    set topicId(value: guid | null);
    get lastReadMessageTime(): string | null;
    set lastReadMessageTime(value: string | null);
    get preLastVisitedAt(): string | null;
    set preLastVisitedAt(value: string | null);
}
