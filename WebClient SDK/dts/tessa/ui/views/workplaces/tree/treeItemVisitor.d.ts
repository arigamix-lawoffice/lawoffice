import { ITreeItem } from './treeItem';
export interface ITreeItemVisitor {
    visitEnter(treeItem: ITreeItem): boolean;
    visitLeave(treeItem: ITreeItem): boolean;
}
export interface ITreeItemAsyncVisitor {
    visitEnter(treeItem: ITreeItem): Promise<boolean>;
    visitLeave(treeItem: ITreeItem): Promise<boolean>;
}
