import { IRawForumData, UserStatusModel } from '.';
export interface IForumData {
    readonly readTopicStatusList?: Map<string, UserStatusModel>;
    readonly topicTypes: Map<string, string>;
}
export declare class ForumData implements IForumData {
    constructor(rawForumData: IRawForumData);
    private readonly _readTopicStatusList?;
    private readonly _topicTypes;
    get readTopicStatusList(): Map<string, UserStatusModel> | undefined;
    get topicTypes(): Map<string, string>;
}
