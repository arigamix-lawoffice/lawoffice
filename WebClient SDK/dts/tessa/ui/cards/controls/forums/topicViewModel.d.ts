import { TopicViewModelBase } from './topicViewModelBase';
import { ForumViewModel } from './forumViewModel';
import { TopicModel, MessageModel } from 'tessa/forums';
export declare class TopicViewModel extends TopicViewModelBase {
    constructor(forumViewModel: ForumViewModel, model: TopicModel, isSearchResult?: boolean, isVisibleEditAction?: (messageModel: MessageModel) => boolean);
    private _showFullDescription;
    private readonly _isEnableEditMessage?;
    get showFullDescription(): boolean;
    set showFullDescription(value: boolean);
    private createMessageViewModel;
    addMessage: (message: MessageModel, isSearchResult: boolean) => void;
    tryGetClientPageNumber(): number;
    toggleDescription: () => void;
    highlightNewMessages: () => void;
}
