import { TopicModel } from 'tessa/forums';
import { ForumOperationItem } from 'tessa/ui/cards';
export declare class ForumDialogManager {
    private constructor();
    private static _instance;
    static get instance(): ForumDialogManager;
    addTopicShowDialog(cardId: guid, modifyAddingTopic?: (model: TopicModel) => void): Promise<TopicModel | null>;
    addParticipantsShowDialog(topicId: string, isModeratorEnabled?: boolean): Promise<boolean>;
    addRoleParticipantsShowDialog(topicId: string, isModeratorEnabled?: boolean): Promise<boolean>;
    changeParticipantsShowDialog(item: ForumOperationItem): Promise<ForumOperationItem | null>;
    changeRoleParticipantsShowDialog(item: ForumOperationItem): Promise<ForumOperationItem | null>;
    private static initChangeRoleParticipantModel;
    private static initChangeParticipantModel;
    private static initFromChangeParticipants;
    private static setIsModeratorIsReadOnly;
    private disableCheckBox;
    private static getParticipantsList;
    private changeParticipants;
    private changeRoleParticipants;
    private saveAddTopic;
    private saveAddParticipants;
    private saveAddRoleParticipants;
}
