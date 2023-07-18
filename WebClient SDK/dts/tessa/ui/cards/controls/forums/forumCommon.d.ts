import { ForumItemViewModel } from './forumItemViewModel';
import { MenuAction } from 'tessa/ui/menuAction';
import { ParticipantType } from 'tessa/forums';
export interface IAttachmentMenuContext {
    item: ForumItemViewModel;
    menuActions: MenuAction[];
}
export declare type ForumActionParameters = {
    topicID?: string;
    participantsTypeID?: ParticipantType;
    [key: string]: any;
};
export declare const convertFromCustomStyle: (customStyle: string, elem: Element) => void;
