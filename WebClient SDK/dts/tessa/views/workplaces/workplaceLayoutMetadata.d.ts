import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
import { SplitState } from './splitState';
import { IWorkplaceContentMetadata, WorkplaceContentMetadataSealed } from './workplaceContentMetadata';
export interface IWorkplaceLayoutMetadata extends IWorkplaceCompositeMetadata {
    readonly content: IWorkplaceContentMetadata | null;
    readonly firstChild: IWorkplaceLayoutMetadata | null;
    readonly secondChild: IWorkplaceLayoutMetadata | null;
    secondChildSize: string;
    splitPosition: string;
    splitState: SplitState;
    seal<T = WorkplaceLayoutMetadataSealed>(): T;
}
export interface WorkplaceLayoutMetadataSealed extends WorkplaceCompositeMetadataSealed {
    readonly content: WorkplaceContentMetadataSealed | null;
    readonly firstChild: WorkplaceLayoutMetadataSealed | null;
    readonly secondChild: WorkplaceLayoutMetadataSealed | null;
    readonly secondChildSize: string;
    readonly splitPosition: string;
    readonly splitState: SplitState;
    seal<T = WorkplaceLayoutMetadataSealed>(): T;
}
export declare class WorkplaceLayoutMetadata extends WorkplaceCompositeMetadata implements IWorkplaceLayoutMetadata {
    constructor();
    get content(): IWorkplaceContentMetadata | null;
    get firstChild(): IWorkplaceLayoutMetadata | null;
    get secondChild(): IWorkplaceLayoutMetadata | null;
    secondChildSize: string;
    splitPosition: string;
    splitState: SplitState;
    seal<T = WorkplaceLayoutMetadataSealed>(): T;
}
