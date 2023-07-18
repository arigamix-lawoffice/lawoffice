import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
import { IWorkplaceLayoutMetadata, WorkplaceLayoutMetadataSealed } from './workplaceLayoutMetadata';
import { IWorkplaceLayoutViewMetadata, WorkplaceLayoutViewMetadataSealed } from './workplaceLayoutViewMetadata';
export interface IWorkplaceContentMetadata extends IWorkplaceCompositeMetadata {
    readonly layouts: ReadonlyArray<IWorkplaceLayoutMetadata>;
    readonly view: IWorkplaceLayoutViewMetadata | null;
    seal<T = WorkplaceContentMetadataSealed>(): T;
}
export interface WorkplaceContentMetadataSealed extends WorkplaceCompositeMetadataSealed {
    readonly layouts: ReadonlyArray<WorkplaceLayoutMetadataSealed>;
    readonly view: WorkplaceLayoutViewMetadataSealed | null;
    seal<T = WorkplaceContentMetadataSealed>(): T;
}
export declare class WorkplaceContentMetadata extends WorkplaceCompositeMetadata implements IWorkplaceContentMetadata {
    constructor();
    get layouts(): ReadonlyArray<IWorkplaceLayoutMetadata>;
    get view(): IWorkplaceLayoutViewMetadata | null;
    seal<T = WorkplaceContentMetadataSealed>(): T;
}
