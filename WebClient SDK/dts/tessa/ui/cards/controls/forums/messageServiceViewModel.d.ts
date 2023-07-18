import { ForumViewModel } from './forumViewModel';
import { MessageViewModelBase } from './messageViewModelBase';
import { TopicViewModelBase } from './topicViewModelBase';
import { UIButton } from 'tessa/ui/uiButton';
import { ParticipantModel, MessageModel } from 'tessa/forums';
import { Visibility } from 'tessa/platform';
export declare class MessageServiceViewModel extends MessageViewModelBase {
    constructor(forumViewModel: ForumViewModel, topic: TopicViewModelBase, model: MessageModel, currentParticipant: ParticipantModel);
    private _buttons;
    private readonly _actionsVisibility;
    get buttons(): UIButton[];
    get actionsVisibility(): Visibility;
    private initDefaultButtons;
    private getUsers;
    private subscribeAction;
    private leaveAction;
}
