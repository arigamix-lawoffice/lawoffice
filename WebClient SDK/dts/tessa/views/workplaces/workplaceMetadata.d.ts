import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
import { RoleLink } from '../roleLink';
import { IFolderNodeMetadata, FolderNodeMetadataSealed } from './folderNodeMetadata';
import { IWorkplaceSearchQueryMetadata, WorkplaceSearchQueryMetadataSealed } from './workplaceSearchQueryMetadata';
import { IDataNodeMetadata, DataNodeMetadataSealed } from './dataNodeMetadata';
export interface IWorkplaceMetadata extends IWorkplaceCompositeMetadata {
    emptyFoldersVisible: boolean;
    leftSideWidth: number;
    readonly nodes: ReadonlyArray<IFolderNodeMetadata>;
    roles: Array<RoleLink>;
    readonly searchQueries: ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    treeVisibility: boolean;
    version: number;
    readonly views: ReadonlyArray<IDataNodeMetadata>;
    seal<T = WorkplaceMetadataSealed>(): T;
}
export interface WorkplaceMetadataSealed extends WorkplaceCompositeMetadataSealed {
    readonly emptyFoldersVisible: boolean;
    readonly leftSideWidth: number;
    readonly nodes: ReadonlyArray<FolderNodeMetadataSealed>;
    readonly roles: ReadonlyArray<RoleLink>;
    readonly searchQueries: ReadonlyArray<WorkplaceSearchQueryMetadataSealed>;
    readonly treeVisibility: boolean;
    readonly version: number;
    readonly views: ReadonlyArray<DataNodeMetadataSealed>;
    seal<T = WorkplaceMetadataSealed>(): T;
}
export declare class WorkplaceMetadata extends WorkplaceCompositeMetadata implements IWorkplaceMetadata {
    constructor();
    emptyFoldersVisible: boolean;
    leftSideWidth: number;
    get nodes(): ReadonlyArray<IFolderNodeMetadata>;
    roles: Array<RoleLink>;
    get searchQueries(): ReadonlyArray<IWorkplaceSearchQueryMetadata>;
    treeVisibility: boolean;
    version: number;
    get views(): ReadonlyArray<IDataNodeMetadata>;
    seal<T = WorkplaceMetadataSealed>(): T;
}
