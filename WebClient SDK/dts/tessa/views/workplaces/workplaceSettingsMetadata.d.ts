import { WorkplaceMetadataComponent, IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
export interface IWorkplaceSettingsMetadata extends IWorkplaceComponentMetadata {
    emptyFoldersVisible: boolean;
    leftSideWidth: number;
    treeVisibility: boolean;
    workplaceId: guid;
    seal<T = WorkplaceSettingsMetadataSealed>(): T;
}
export interface WorkplaceSettingsMetadataSealed extends WorkplaceMetadataComponentSealed {
    readonly emptyFoldersVisible: boolean;
    readonly leftSideWidth: number;
    readonly treeVisibility: boolean;
    readonly workplaceId: guid;
    seal<T = WorkplaceSettingsMetadataSealed>(): T;
}
export declare class WorkplaceSettingsMetadata extends WorkplaceMetadataComponent implements IWorkplaceSettingsMetadata {
    constructor();
    emptyFoldersVisible: boolean;
    leftSideWidth: number;
    treeVisibility: boolean;
    workplaceId: guid;
    seal<T = WorkplaceSettingsMetadataSealed>(): T;
}
