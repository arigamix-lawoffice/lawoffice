import { ParticipantType } from './enums';
import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class ParticipantModel extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly topicIdKey: string;
    static readonly userIdKey: string;
    static readonly userNameKey: string;
    static readonly isReadOnlyKey: string;
    static readonly isSubscribedKey: string;
    static readonly typeKey: string;
    get topicId(): guid;
    set topicId(value: guid);
    get userId(): guid | null;
    set userId(value: guid | null);
    get userName(): string | null;
    set userName(value: string | null);
    get isReadOnly(): boolean;
    set isReadOnly(value: boolean);
    get isSubscribed(): boolean;
    set isSubscribed(value: boolean);
    get type(): ParticipantType;
    set type(value: ParticipantType);
}
