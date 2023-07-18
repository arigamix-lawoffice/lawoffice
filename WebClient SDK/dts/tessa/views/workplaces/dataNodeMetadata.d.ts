import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed, IWorkplaceExpandingMetadata, WorkplaceExpandingMetadataSealed } from './workplaceCompositeMetadata';
import { ExpandingMode } from './expandingMode';
import { RowCounterVisibility } from './rowCounterVisibility';
import { IWorkplaceLayoutMetadata, WorkplaceLayoutMetadataSealed } from './workplaceLayoutMetadata';
import { IFolderNodeMetadata, FolderNodeMetadataSealed } from './folderNodeMetadata';
import { IWorkplaceSearchQueryMetadata, WorkplaceSearchQueryMetadataSealed } from './workplaceSearchQueryMetadata';
import { IWorkplaceViewSubsetMetadata, WorkplaceViewSubsetMetadataSealed } from './workplaceViewSubsetMetadata';
import { RequestParameter, ViewSelectionMode } from '../metadata';
import { ShowMode } from '../showMode';
import { IDataSourceMetadata, DataSourceMetadataSealed } from '../dataSourceMetadata';
import { IWorkplaceViewReferenceMetadata, WorkplaceViewReferenceMetadataSealed } from './workplaceViewReferenceMetadata';
export interface IDataNodeMetadata extends IWorkplaceCompositeMetadata, IWorkplaceExpandingMetadata {
    caption: string;
    dataSourceMetadata: IDataSourceMetadata | null;
    enableAutoWidth: boolean;
    isNode: boolean;
    readonly layout: IWorkplaceLayoutMetadata | null;
    multiSelect: boolean | null;
    readonly nodes: ReadonlyArray<IFolderNodeMetadata>;
    parameters: Array<RequestParameter>;
    parametersByState: Map<string, Array<RequestParameter>>;
    references: Array<IWorkplaceViewReferenceMetadata>;
    rowCounterVisible: RowCounterVisibility;
    scopeName: string;
    readonly searchQueries: ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    selectionMode: ViewSelectionMode;
    showMode: ShowMode;
    readonly subsetsSettings: ReadonlyArray<IWorkplaceViewSubsetMetadata>;
    readonly views: ReadonlyArray<IDataNodeMetadata>;
    seal<T = DataNodeMetadataSealed>(): T;
}
export interface DataNodeMetadataSealed extends WorkplaceCompositeMetadataSealed, WorkplaceExpandingMetadataSealed {
    readonly caption: string;
    readonly dataSourceMetadata: DataSourceMetadataSealed | null;
    readonly enableAutoWidth: boolean;
    readonly isNode: boolean;
    readonly layout: WorkplaceLayoutMetadataSealed | null;
    readonly multiSelect: boolean | null;
    readonly nodes: ReadonlyArray<FolderNodeMetadataSealed>;
    readonly parameters: ReadonlyArray<RequestParameter>;
    readonly parametersByState: ReadonlyMap<string, ReadonlyArray<RequestParameter>>;
    readonly references: ReadonlyArray<WorkplaceViewReferenceMetadataSealed>;
    readonly rowCounterVisible: RowCounterVisibility;
    readonly scopeName: string;
    readonly searchQueries: ReadonlyArray<WorkplaceSearchQueryMetadataSealed>;
    readonly selectionMode: ViewSelectionMode;
    readonly showMode: ShowMode;
    readonly subsetsSettings: ReadonlyArray<WorkplaceViewSubsetMetadataSealed>;
    readonly views: ReadonlyArray<DataNodeMetadataSealed>;
    seal<T = DataNodeMetadataSealed>(): T;
}
export declare class DataNodeMetadata extends WorkplaceCompositeMetadata implements IDataNodeMetadata {
    constructor();
    caption: string;
    dataSourceMetadata: IDataSourceMetadata | null;
    enableAutoWidth: boolean;
    expandingMode: ExpandingMode;
    isNode: boolean;
    get layout(): IWorkplaceLayoutMetadata | null;
    multiSelect: boolean | null;
    get nodes(): ReadonlyArray<IFolderNodeMetadata>;
    parameters: Array<RequestParameter>;
    parametersByState: Map<string, Array<RequestParameter>>;
    references: Array<IWorkplaceViewReferenceMetadata>;
    rowCounterVisible: RowCounterVisibility;
    scopeName: string;
    get searchQueries(): ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    selectionMode: ViewSelectionMode;
    showMode: ShowMode;
    get subsetsSettings(): ReadonlyArray<IWorkplaceViewSubsetMetadata>;
    get views(): ReadonlyArray<IDataNodeMetadata>;
    seal<T = DataNodeMetadataSealed>(): T;
}
