import { ForumItemViewModel } from './forumItemViewModel';
import { ForumViewModel } from './forumViewModel';
import { MenuAction } from 'tessa/ui/menuAction';
export declare class EmptyForumViewModel {
    constructor(forumViewModel: ForumViewModel);
    private _forumViewModel;
    readonly contextMenuGenerators: ((ctx: {
        content: EmptyForumViewModel;
        forumViewModel: ForumViewModel;
        menuActions: MenuAction[];
    }) => void)[];
    getContextMenu(): MenuAction[];
    getAttachmentContextMenu(_item: ForumItemViewModel): MenuAction[];
    dispose(): void;
    addTopic(): Promise<void>;
    superModerator(): Promise<void>;
}
