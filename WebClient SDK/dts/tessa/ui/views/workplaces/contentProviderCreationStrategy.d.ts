import { ITreeItem } from './tree';
import { DoubleClickAction } from '../doubleClickInfo';
import { IContentProvider } from '../contentProvider';
import { IWorkplaceViewModel } from '../workplaceViewModel';
import { IViewParameters } from '../parameters';
import { SelectAction } from '../selectFromViewContext';
import { WorkplaceMetadataComponentSealed } from 'tessa/views/workplaces';
import { ViewParameterMetadataSealed, RequestParameter } from 'tessa/views/metadata';
import { SortingColumn } from 'tessa/views';
export interface TreeItemContentFactory {
    (treeItem: ITreeItem, workplace: IWorkplaceViewModel, strategy: IContentProviderCreationStrategy): IContentProvider | null;
}
export interface IContentProviderCreationStrategy {
    readonly doubleClickAction: DoubleClickAction | null;
    readonly refSection: ReadonlyArray<string> | null;
    create(workplace: IWorkplaceViewModel, metadata: WorkplaceMetadataComponentSealed, parameters: IViewParameters | null, sortingColumns?: Map<guid, SortingColumn[]> | null): IContentProvider | null;
    createParameters(parametersMetadata: ViewParameterMetadataSealed[]): IViewParameters;
    tryGetSortingColumns(treeItem: ITreeItem): Map<guid, SortingColumn[]> | null;
}
export declare class ContentProviderCreationStrategy implements IContentProviderCreationStrategy {
    constructor(extraParameters: RequestParameter[], refSection?: ReadonlyArray<string> | null, doubleClickAction?: DoubleClickAction | null, selectAction?: SelectAction | null);
    private _extraParameters;
    private _extraParametersAliases;
    readonly doubleClickAction: DoubleClickAction | null;
    readonly refSection: ReadonlyArray<string> | null;
    readonly selectAction: SelectAction | null;
    create(workplace: IWorkplaceViewModel, metadata: WorkplaceMetadataComponentSealed, parameters: IViewParameters | null, sortingColumns?: Map<guid, SortingColumn[]> | null): IContentProvider | null;
    createParameters(parametersMetadata: ViewParameterMetadataSealed[]): IViewParameters;
    tryGetSortingColumns(_treeItem: ITreeItem): Map<guid, SortingColumn[]> | null;
    private createForView;
    private createForExtendedSearchQuery;
    private createForSearchQuery;
    private isExtraParametersView;
    private tryGetExtendedSearchQueryTemplate;
    private createTemplateClone;
}
