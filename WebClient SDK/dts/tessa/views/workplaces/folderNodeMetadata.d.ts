import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed, IWorkplaceExpandingMetadata, WorkplaceExpandingMetadataSealed } from './workplaceCompositeMetadata';
import { ExpandingMode } from './expandingMode';
import { IWorkplaceSearchQueryMetadata, WorkplaceSearchQueryMetadataSealed } from './workplaceSearchQueryMetadata';
import { IDataNodeMetadata, DataNodeMetadataSealed } from './dataNodeMetadata';
import { ShowMode } from '../showMode';
export interface IFolderNodeMetadata extends IWorkplaceCompositeMetadata, IWorkplaceExpandingMetadata {
    readonly nodes: ReadonlyArray<IFolderNodeMetadata>;
    readonly searchQueries: ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    showMode: ShowMode;
    readonly views: ReadonlyArray<IDataNodeMetadata>;
    seal<T = FolderNodeMetadataSealed>(): T;
}
export interface FolderNodeMetadataSealed extends WorkplaceCompositeMetadataSealed, WorkplaceExpandingMetadataSealed {
    readonly nodes: ReadonlyArray<FolderNodeMetadataSealed>;
    readonly searchQueries: ReadonlyArray<WorkplaceSearchQueryMetadataSealed>;
    readonly showMode: ShowMode;
    readonly views: ReadonlyArray<DataNodeMetadataSealed>;
    seal<T = FolderNodeMetadataSealed>(): T;
}
export declare class FolderNodeMetadata extends WorkplaceCompositeMetadata implements IFolderNodeMetadata {
    constructor();
    expandingMode: ExpandingMode;
    get nodes(): ReadonlyArray<IFolderNodeMetadata>;
    get searchQueries(): ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    showMode: ShowMode;
    get views(): ReadonlyArray<IDataNodeMetadata>;
    seal<T = FolderNodeMetadataSealed>(): T;
}
