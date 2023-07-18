import { ParticipantType } from 'tessa/forums';
import { IViewContext } from 'tessa/ui/views/viewContext';
import { ForumPermissionsProvider } from './controls/forums/forumPermissionsProvider';
export interface IForumOperationContext {
    participantType: ParticipantType;
    isSuperModerator: boolean;
    cardID: string;
    permissionProvider: ForumPermissionsProvider;
}
export declare function getForumOperationContextFromViewContext(context: IViewContext): IForumOperationContext;
