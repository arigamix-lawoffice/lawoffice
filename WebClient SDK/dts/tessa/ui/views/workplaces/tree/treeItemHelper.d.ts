import { ITreeItem, ITreeItemWithMetadata } from './treeItem';
import { IWorkplaceViewModel } from '../../workplaceViewModel';
import { WorkplaceMetadataSealed, WorkplaceViewSubsetMetadataSealed } from 'tessa/views/workplaces';
import { ITreeItemUpdateContext } from './treeItemUpdateContext';
import { ISubsetData } from 'tessa/ui/views/subsetData';
import { ISubsetDataTreeItem } from 'tessa/ui/views/workplaces/tree/subsetDataTreeItem';
export declare function treeItemFullRefresh(treeItem: ITreeItem, context?: ITreeItemUpdateContext | null): Promise<void>;
export declare function getTreeItemPath(treeItem: ITreeItem): ITreeItem[];
export declare function openParentOrFirstPossible(treeItem: ITreeItem): void;
export declare function findWorkplaceMetadata(workplaceId: guid): WorkplaceMetadataSealed | null;
export declare function findWorkplaceMetadataByCompositionId(compositionId: guid): {
    workplace: WorkplaceMetadataSealed | null;
    parentStack: guid[] | null;
};
export declare function findFirstVisibleItem(workplace: WorkplaceMetadataSealed | null): {
    compositionId: guid | null;
    parentStack: guid[] | null;
};
export declare function getParentStackForItem(compositionId: guid, workplace: WorkplaceMetadataSealed): guid[] | null;
export declare function tryOpenCompositionId(workplace: IWorkplaceViewModel, compositionId: guid | null | undefined, parentStack: guid[] | null, needDispatch: boolean): Promise<boolean>;
export declare function showEmptyWorkplaceFolders(workplace: IWorkplaceViewModel): Promise<void>;
export declare function hideEmptyWorkplaceFolders(workplace: IWorkplaceViewModel, treeItem?: ITreeItem): Promise<void>;
export declare function isTreeItemVisibleInPath(treeItem: ITreeItem): boolean;
export declare function tryGetSubsetData(treeItem: ITreeItemWithMetadata<WorkplaceViewSubsetMetadataSealed>, subsetData: ISubsetData): ISubsetDataTreeItem | null;
