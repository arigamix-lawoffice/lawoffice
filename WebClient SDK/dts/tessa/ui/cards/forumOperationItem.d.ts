import { ParticipantType } from 'tessa/forums';
import { CardOperationItem } from './cardOperationItem';
export declare class ForumOperationItem extends CardOperationItem {
    constructor(cardId: string, displayValue: string);
    topicId: string;
    roleType: number;
    roleId: string;
    roleName: string;
    isReadOnly: boolean;
    isSubscribed: boolean;
    participantType: ParticipantType;
}
