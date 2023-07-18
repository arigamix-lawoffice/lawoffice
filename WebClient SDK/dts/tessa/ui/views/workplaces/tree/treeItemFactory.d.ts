import { IFolderTreeItem } from './folderTreeItem';
import { ITreeItem } from './treeItem';
import { ITreeItemAsyncLoader } from './treeItemAsyncLoader';
import { ISubsetsContainer } from './subsetsContainer';
import { IViewTreeItem } from './viewTreeItem';
import { ISubsetTreeItem } from './subsetTreeItem';
import { ISubsetDataTreeItem } from './subsetDataTreeItem';
import { ISearchQueryTreeItem } from './searchQueryTreeItem';
import { ITreeItemUpdateContext } from './treeItemUpdateContext';
import { WorkplaceCreationContext } from '../../workplaceCreationContext';
import { IWorkplaceViewModel } from '../../workplaceViewModel';
import { IViewParameters } from '../../parameters';
import { ISubsetData } from '../../subsetData';
import { DataNodeMetadataSealed, WorkplaceSearchQueryMetadataSealed, FolderNodeMetadataSealed, IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed, WorkplaceViewSubsetMetadataSealed } from 'tessa/views/workplaces';
import { ITessaView, ITessaViewResult } from 'tessa/views';
import { ViewParameterMetadataSealed, ViewMetadataSealed, ViewSubsetMetadataSealed } from 'tessa/views/metadata';
export declare class TreeItemFactory {
    private constructor();
    private static _instance;
    static get instance(): TreeItemFactory;
    createFolder(workplace: IWorkplaceViewModel, metadata: FolderNodeMetadataSealed): IFolderTreeItem;
    createSearchQuery(workplace: IWorkplaceViewModel, metadata: WorkplaceSearchQueryMetadataSealed): ISearchQueryTreeItem | null;
    createSubset(workplace: IWorkplaceViewModel, metadata: WorkplaceViewSubsetMetadataSealed, viewParameters: IViewParameters): ISubsetTreeItem;
    showSubset(treeItem: ISubsetsContainer<any>, subsetMetadata: WorkplaceViewSubsetMetadataSealed): ISubsetTreeItem;
    removeSubset(subset: ISubsetTreeItem): boolean;
    createSubsetData(workplace: IWorkplaceViewModel, metadata: WorkplaceViewSubsetMetadataSealed, data: ISubsetData, viewParameters: IViewParameters, view: ITessaView): ISubsetDataTreeItem;
    createView(workplace: IWorkplaceViewModel, metadata: DataNodeMetadataSealed): IViewTreeItem | null;
    createWorkplace(creationContext: WorkplaceCreationContext): IWorkplaceViewModel | null;
    buildItems(parent: ITreeItem, childNodesMetadata: (IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed)[] | ReadonlyArray<IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed>, workplace: IWorkplaceViewModel): void;
    private createViewParameters;
    private isExtraParametersView;
    private getIcon;
    private getSubsetPosition;
    private injectExtensionsWithInitialization;
}
export declare abstract class TreeItemAsyncLoader<T extends ITreeItem, R> implements ITreeItemAsyncLoader {
    constructor();
    private _asyncGuard;
    hasItems: boolean;
    itemsLoaded: boolean;
    canBeDeffered: boolean;
    injectAsyncLoader(treeItem: T): this;
    protected abstract getDataForLoad(treeItem: T): Promise<R>;
    protected getDataForRefresh(treeItem: T): Promise<R>;
    protected abstract loadDataInUI(treeItem: T, data: R): void;
    protected abstract refreshDataInUI(treeItem: T, data: R, context: ITreeItemUpdateContext): Promise<void>;
    loadChildAsync(treeItem: T): Promise<void>;
    refreshChildAsync(treeItem: T, context: ITreeItemUpdateContext): Promise<void>;
}
export declare class WorkplaceAsyncLoader extends TreeItemAsyncLoader<IWorkplaceViewModel, WorkplaceMetadataComponentSealed[]> {
    injectAsyncLoader(treeItem: IWorkplaceViewModel): this;
    protected getDataForLoad(treeItem: IWorkplaceViewModel): Promise<WorkplaceMetadataComponentSealed[]>;
    protected loadDataInUI(treeItem: IWorkplaceViewModel, data: WorkplaceMetadataComponentSealed[]): void;
    protected getDataForRefresh(_treeItem: IWorkplaceViewModel): Promise<WorkplaceMetadataComponentSealed[]>;
    protected refreshDataInUI(treeItem: IWorkplaceViewModel, _data: WorkplaceMetadataComponentSealed[], context: ITreeItemUpdateContext): Promise<void>;
}
export declare class FolderTreeItemAsyncLoader extends TreeItemAsyncLoader<IFolderTreeItem, WorkplaceMetadataComponentSealed[]> {
    injectAsyncLoader(treeItem: IFolderTreeItem): this;
    protected getDataForLoad(treeItem: IFolderTreeItem): Promise<WorkplaceMetadataComponentSealed[]>;
    protected loadDataInUI(treeItem: IFolderTreeItem, data: WorkplaceMetadataComponentSealed[]): void;
    protected getDataForRefresh(_treeItem: IFolderTreeItem): Promise<WorkplaceMetadataComponentSealed[]>;
    protected refreshDataInUI(treeItem: IFolderTreeItem, _data: WorkplaceMetadataComponentSealed[], context: ITreeItemUpdateContext): Promise<void>;
}
export declare class ViewTreeItemAsyncLoader extends TreeItemAsyncLoader<IViewTreeItem, [
    WorkplaceViewSubsetMetadataSealed[],
    WorkplaceMetadataComponentSealed[]
]> {
    injectAsyncLoader(treeItem: IViewTreeItem): this;
    protected getDataForLoad(treeItem: IViewTreeItem): Promise<[WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]]>;
    protected loadDataInUI(treeItem: IViewTreeItem, data: [WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]]): void;
    protected getDataForRefresh(_treeItem: IViewTreeItem): Promise<[WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]]>;
    protected refreshDataInUI(treeItem: IViewTreeItem, _data: [WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]], context: ITreeItemUpdateContext): Promise<void>;
}
export declare class SearchQueryTreeItemAsyncLoader extends TreeItemAsyncLoader<ISearchQueryTreeItem, [
    WorkplaceViewSubsetMetadataSealed[],
    WorkplaceMetadataComponentSealed[]
]> {
    injectAsyncLoader(treeItem: ISearchQueryTreeItem): this;
    protected getDataForLoad(treeItem: ISearchQueryTreeItem): Promise<[WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]]>;
    protected loadDataInUI(treeItem: ISearchQueryTreeItem, data: [WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]]): void;
    protected getDataForRefresh(_treeItem: ISearchQueryTreeItem): Promise<[WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]]>;
    protected refreshDataInUI(treeItem: ISearchQueryTreeItem, _data: [WorkplaceViewSubsetMetadataSealed[], WorkplaceMetadataComponentSealed[]], context: ITreeItemUpdateContext): Promise<void>;
}
export declare abstract class AbstractSubsetFamilyTreeItemAsyncLoader<T extends ITreeItem, R> extends TreeItemAsyncLoader<T, R> {
    protected static addOrReplaceSubsetRefParam(parameters: IViewParameters, viewMetadata: ViewMetadataSealed, refParam: string, caption: string, value: unknown): void;
    protected static getSubsetInfo(result: ITessaViewResult, subset: ViewSubsetMetadataSealed, metadata: ViewMetadataSealed): ISubsetData[];
    protected static providerTreeRefParameter(parameters: IViewParameters, viewMetadata: ViewMetadataSealed, refParam: string): void;
    protected static removeRefParameter(parameters: IViewParameters, viewMetadata: ViewMetadataSealed, refParam: string): void;
    protected static tryGetRefParameter(viewMetadata: ViewMetadataSealed, refParam: string): ViewParameterMetadataSealed | null;
}
export declare class SubsetTreeItemAsyncLoader extends AbstractSubsetFamilyTreeItemAsyncLoader<ISubsetTreeItem, ISubsetData[]> {
    constructor();
    injectAsyncLoader(treeItem: ISubsetTreeItem): this;
    protected getDataForLoad(treeItem: ISubsetTreeItem): Promise<ISubsetData[]>;
    protected loadDataInUI(treeItem: ISubsetTreeItem, data: ISubsetData[]): void;
    protected getDataForRefresh(treeItem: ISubsetTreeItem): Promise<ISubsetData[]>;
    protected refreshDataInUI(treeItem: ISubsetTreeItem, data: ISubsetData[], context: ITreeItemUpdateContext): Promise<void>;
    private getSubsetRowsWithUniqueId;
}
export declare class SubsetDataTreeItemAsyncLoader extends AbstractSubsetFamilyTreeItemAsyncLoader<ISubsetDataTreeItem, ISubsetData[]> {
    constructor();
    injectAsyncLoader(treeItem: ISubsetDataTreeItem): this;
    protected getDataForLoad(treeItem: ISubsetDataTreeItem): Promise<ISubsetData[]>;
    protected loadDataInUI(treeItem: ISubsetDataTreeItem, data: ISubsetData[]): void;
    protected getDataForRefresh(treeItem: ISubsetDataTreeItem): Promise<ISubsetData[]>;
    protected refreshDataInUI(treeItem: ISubsetDataTreeItem, data: ISubsetData[], context: ITreeItemUpdateContext): Promise<void>;
}
