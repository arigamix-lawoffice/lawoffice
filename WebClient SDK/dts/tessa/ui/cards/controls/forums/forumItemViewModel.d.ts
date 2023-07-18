import { ForumViewModel } from './forumViewModel';
import { MessageViewModel } from './messageViewModel';
import { AttachmentType, ItemModel } from 'tessa/forums';
import { MenuAction } from 'tessa/ui/menuAction';
export declare class ForumItemViewModel {
    constructor(forumViewModel: ForumViewModel, message: MessageViewModel, model: ItemModel);
    private _caption;
    private _data;
    get id(): string | null;
    readonly forumViewModel: ForumViewModel;
    readonly message: MessageViewModel;
    readonly model: ItemModel;
    get caption(): string;
    set caption(value: string);
    get type(): AttachmentType;
    get data(): File | null;
    set data(value: File | null);
    getContextMenu: () => MenuAction[];
}
