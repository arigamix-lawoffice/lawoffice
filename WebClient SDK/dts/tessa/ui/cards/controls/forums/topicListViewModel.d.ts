import { ForumViewModel } from './forumViewModel';
import { TopicViewModel } from './topicViewModel';
import { IAttachmentMenuContext } from './forumCommon';
import { ForumItemViewModel } from './forumItemViewModel';
import { UIButton } from 'tessa/ui/uiButton';
import { MenuAction } from 'tessa/ui/menuAction';
export declare class TopicListViewModel {
    constructor(forumViewModel: ForumViewModel, models: TopicViewModel[]);
    readonly forumViewModel: ForumViewModel;
    private readonly _topics;
    private _leftButtons;
    private _rightButtons;
    get topics(): TopicViewModel[];
    get leftButtons(): UIButton[];
    get rightButtons(): UIButton[];
    readonly attachmentContextMenuGenerators: ((ctx: IAttachmentMenuContext) => void)[];
    private initDefaultButtons;
    private initMessages;
    getAttachmentContextMenu(item: ForumItemViewModel): MenuAction[];
    private getExpandButtonIcon;
    private getExpandButtonTooltip;
    onContentWidthRatioChange: (diff: number) => void;
    onContentWidthRatioChangeComplete: () => void;
    dispose(): void;
}
