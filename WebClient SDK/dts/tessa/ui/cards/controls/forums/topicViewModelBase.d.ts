import { ForumViewModel } from './forumViewModel';
import { MessageViewModelBase } from './messageViewModelBase';
import { TopicModel, ParticipantModel } from 'tessa/forums';
export declare abstract class TopicViewModelBase {
    constructor(forumViewModel: ForumViewModel, model: TopicModel);
    protected readonly _messages: MessageViewModelBase[];
    protected _title: string;
    protected _authorName: string;
    protected _created: string;
    protected _description: string;
    protected _currentParticipant: ParticipantModel;
    readonly forumViewModel: ForumViewModel;
    readonly model: TopicModel;
    readonly id: guid;
    get messages(): MessageViewModelBase[];
    get title(): string;
    set title(value: string);
    get authorName(): string;
    set authorName(value: string);
    get created(): string;
    set created(value: string);
    get description(): string;
    set description(value: string);
    get currentParticipant(): ParticipantModel;
}
