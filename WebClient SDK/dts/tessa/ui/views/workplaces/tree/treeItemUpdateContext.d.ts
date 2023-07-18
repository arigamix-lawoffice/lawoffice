import { ITreeItem } from './treeItem';
export interface ITreeItemUpdateContext {
    needSetSelection: ITreeItem | null;
}
export declare class TreeItemUpdateContext implements ITreeItemUpdateContext {
    needSetSelection: ITreeItem | null;
}
