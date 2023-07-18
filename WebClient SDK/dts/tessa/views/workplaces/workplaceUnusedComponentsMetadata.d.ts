import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
export interface IWorkplaceUnusedComponentsMetadata extends IWorkplaceCompositeMetadata {
    hasItems(): boolean;
    seal<T = WorkplaceUnusedComponentsMetadataSealed>(): T;
}
export interface WorkplaceUnusedComponentsMetadataSealed extends WorkplaceCompositeMetadataSealed {
    hasItems(): boolean;
    seal<T = WorkplaceUnusedComponentsMetadataSealed>(): T;
}
export declare class WorkplaceUnusedComponentsMetadata extends WorkplaceCompositeMetadata implements IWorkplaceUnusedComponentsMetadata {
    constructor();
    hasItems(): boolean;
    seal<T = WorkplaceUnusedComponentsMetadataSealed>(): T;
}
