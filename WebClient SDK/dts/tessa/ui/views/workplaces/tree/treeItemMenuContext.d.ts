import { ITreeItem } from './treeItem';
import { MenuAction } from 'tessa/ui/menuAction';
export interface ITreeItemMenuContext {
    readonly treeItem: ITreeItem;
    readonly menuActions: MenuAction[];
}
export declare class TreeItemMenuContext implements ITreeItemMenuContext {
    constructor(treeItem: ITreeItem);
    readonly treeItem: ITreeItem;
    readonly menuActions: MenuAction[];
}
