import { TopicModel } from './topicModel';
import { MessageModel } from './messageModel';
import { ICloneable } from 'tessa/platform';
import { IStorage, ArrayStorage, StorageObject } from 'tessa/platform/storage';
import { ParticipantModel } from 'tessa/forums/participantModel';
import { CardInfoStorageObject } from 'tessa/cards';
export declare class ForumResponse extends CardInfoStorageObject implements ICloneable<ForumResponse> {
    constructor(storage?: IStorage);
    static readonly forumSettingsKey: string;
    static readonly satelliteIdKey: string;
    static readonly topicsKey: string;
    static readonly messagesKey: string;
    static readonly messageKey: string;
    get forumSettings(): IStorage;
    set forumSettings(value: IStorage);
    get satelliteId(): guid | null;
    set satelliteId(value: guid | null);
    get topics(): ArrayStorage<TopicModel>;
    set topics(value: ArrayStorage<TopicModel>);
    get messages(): ArrayStorage<MessageModel>;
    set messages(value: ArrayStorage<MessageModel>);
    get message(): MessageModel;
    set message(value: MessageModel);
    tryGetTopics(): ArrayStorage<TopicModel> | null | undefined;
    tryGetMessages(): ArrayStorage<MessageModel> | null | undefined;
    private static readonly _topicsFactory;
    private static readonly _messageFactory;
    clone(): ForumResponse;
}
export declare class ForumPermissionsResponse extends StorageObject {
    constructor(storage?: IStorage);
    static readonly HasRequiredPermissionKey: string;
    static readonly participantKey: string;
    get hasRequiredPermission(): boolean;
    set hasRequiredPermission(value: boolean);
    get participant(): ParticipantModel;
    set participant(value: ParticipantModel);
}
