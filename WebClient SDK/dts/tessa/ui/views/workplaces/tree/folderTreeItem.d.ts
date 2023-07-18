import { TreeItemWithMetadataBase } from './treeItemWithMetadataBase';
import { ITreeItemWithMetadata } from './treeItem';
import { FolderNodeMetadataSealed } from 'tessa/views/workplaces';
export interface IFolderTreeItem extends ITreeItemWithMetadata<FolderNodeMetadataSealed> {
    hasContent: boolean;
}
export declare class FolderTreeItem extends TreeItemWithMetadataBase<FolderNodeMetadataSealed> implements IFolderTreeItem {
    constructor(metadata: FolderNodeMetadataSealed);
    hasContent: boolean;
}
