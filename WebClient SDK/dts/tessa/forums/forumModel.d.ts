import { TopicModel } from './topicModel';
import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
import { ForumSettingsModel } from './forumSettingsModel';
export declare class ForumModel extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly cardIdKey: string;
    static readonly topicsKey: string;
    static readonly forumSettingsKey: string;
    get cardId(): guid;
    set cardId(value: guid);
    get topics(): ArrayStorage<TopicModel>;
    set topics(value: ArrayStorage<TopicModel>);
    get forumSettings(): ForumSettingsModel;
    set forumSettings(value: ForumSettingsModel);
    tryGetTopics(): ArrayStorage<TopicModel> | null | undefined;
    tryGetForumSettings(): ForumSettingsModel | null | undefined;
    private static readonly _topicsFactory;
    ensureCacheResolved(): void;
}
