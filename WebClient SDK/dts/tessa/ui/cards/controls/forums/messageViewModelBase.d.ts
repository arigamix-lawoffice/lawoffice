import { ForumViewModel } from './forumViewModel';
import { TopicViewModelBase } from './topicViewModelBase';
import { MessageModelBase } from 'tessa/forums';
export declare abstract class MessageViewModelBase {
    constructor(forumViewModel: ForumViewModel, topic: TopicViewModelBase, model: MessageModelBase);
    protected _message: string;
    private _created;
    readonly forumViewModel: ForumViewModel;
    readonly model: MessageModelBase;
    readonly topic: TopicViewModelBase;
    readonly id: guid;
    get message(): string;
    set message(value: string);
    get created(): string;
    set created(value: string);
}
