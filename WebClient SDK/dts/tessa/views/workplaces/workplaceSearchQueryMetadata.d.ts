import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed, IWorkplaceExpandingMetadata, WorkplaceExpandingMetadataSealed } from './workplaceCompositeMetadata';
import { ExpandingMode } from './expandingMode';
import { IWorkplaceLayoutMetadata, WorkplaceLayoutMetadataSealed } from './workplaceLayoutMetadata';
import { IFolderNodeMetadata, FolderNodeMetadataSealed } from './folderNodeMetadata';
import { RowCounterVisibility } from './rowCounterVisibility';
import { IWorkplaceViewSubsetMetadata, WorkplaceViewSubsetMetadataSealed } from './workplaceViewSubsetMetadata';
import { IDataNodeMetadata, DataNodeMetadataSealed } from './dataNodeMetadata';
import { ISearchQueryMetadata, SearchQueryMetadataSealed } from '../searchQueries';
import { ShowMode } from '../showMode';
export interface IWorkplaceSearchQueryMetadata extends IWorkplaceCompositeMetadata, IWorkplaceExpandingMetadata {
    caption: string;
    isNode: boolean;
    readonly layout: IWorkplaceLayoutMetadata | null;
    metadata: ISearchQueryMetadata | null;
    readonly nodes: ReadonlyArray<IFolderNodeMetadata>;
    rowCounterVisible: RowCounterVisibility;
    readonly searchQueries: ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    searchQueryId: guid;
    showMode: ShowMode;
    readonly subsetsSettings: ReadonlyArray<IWorkplaceViewSubsetMetadata>;
    readonly views: ReadonlyArray<IDataNodeMetadata>;
    seal<T = WorkplaceSearchQueryMetadataSealed>(): T;
}
export interface WorkplaceSearchQueryMetadataSealed extends WorkplaceCompositeMetadataSealed, WorkplaceExpandingMetadataSealed {
    readonly caption: string;
    readonly isNode: boolean;
    readonly layout: WorkplaceLayoutMetadataSealed | null;
    readonly metadata: SearchQueryMetadataSealed | null;
    readonly nodes: ReadonlyArray<FolderNodeMetadataSealed>;
    readonly rowCounterVisible: RowCounterVisibility;
    readonly searchQueries: ReadonlyArray<WorkplaceSearchQueryMetadataSealed>;
    readonly searchQueryId: guid;
    readonly showMode: ShowMode;
    readonly subsetsSettings: ReadonlyArray<WorkplaceViewSubsetMetadataSealed>;
    readonly views: ReadonlyArray<DataNodeMetadataSealed>;
    seal<T = WorkplaceSearchQueryMetadataSealed>(): T;
}
export declare class WorkplaceSearchQueryMetadata extends WorkplaceCompositeMetadata implements IWorkplaceSearchQueryMetadata {
    constructor();
    private _metadata;
    caption: string;
    expandingMode: ExpandingMode;
    isNode: boolean;
    get layout(): IWorkplaceLayoutMetadata | null;
    get metadata(): ISearchQueryMetadata | null;
    set metadata(value: ISearchQueryMetadata | null);
    get nodes(): ReadonlyArray<IFolderNodeMetadata>;
    rowCounterVisible: RowCounterVisibility;
    get searchQueries(): ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    searchQueryId: guid;
    showMode: ShowMode;
    get subsetsSettings(): ReadonlyArray<IWorkplaceViewSubsetMetadata>;
    get views(): ReadonlyArray<IDataNodeMetadata>;
    seal<T = WorkplaceSearchQueryMetadataSealed>(): T;
}
