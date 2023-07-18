import { TopicViewModelBase } from './topicViewModelBase';
import { ForumViewModel } from './forumViewModel';
import { MessageViewModel } from './messageViewModel';
import { MessageModel } from 'tessa/forums';
export declare class FoundMessageViewModel extends MessageViewModel {
    constructor(forumViewModel: ForumViewModel, topic: TopicViewModelBase, model: MessageModel);
    private _loadMessage?;
    set loadMessage(value: () => void);
    private onLoadMessage;
}
