import { ITreeItem } from './treeItem';
import { ITreeItemUpdateContext } from './treeItemUpdateContext';
export interface ITreeItemAsyncLoader {
    hasItems: boolean;
    itemsLoaded: boolean;
    canBeDeffered: boolean;
    loadChildAsync(treeItem: ITreeItem): Promise<void>;
    refreshChildAsync(treeItem: ITreeItem, context: ITreeItemUpdateContext): Promise<void>;
}
