import { IForumData } from 'tessa/forums';
import { IForumCachedData } from '.';
export declare class ForumClientCachedDataManager {
    private constructor();
    private static _instance;
    static get instance(): ForumClientCachedDataManager;
    private _forumCachedData?;
    addReadTopicId(topicId: guid, lastReadMessageTime: string): void;
    addTopicId(topicId: guid): void;
    setCachedData: (forumCachedData: IForumCachedData) => void;
    getForumData: () => IForumData | undefined;
}
