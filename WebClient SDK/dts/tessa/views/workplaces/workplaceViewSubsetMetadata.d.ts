import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed, IWorkplaceExpandingMetadata, WorkplaceExpandingMetadataSealed } from './workplaceCompositeMetadata';
import { ExpandingMode } from './expandingMode';
import { SubsetVisibility } from './subsetVisibility';
import { IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
import { IViewSubsetMetadata, ViewSubsetMetadataSealed } from '../metadata';
export interface IWorkplaceViewSubsetMetadata extends IWorkplaceCompositeMetadata, IWorkplaceExpandingMetadata {
    caption: string;
    subset: IViewSubsetMetadata | null;
    readonly subsets: ReadonlyArray<IWorkplaceViewSubsetMetadata>;
    subsetsOwnerMetadata: IWorkplaceComponentMetadata | null;
    visibility: SubsetVisibility;
    seal<T = WorkplaceViewSubsetMetadataSealed>(): T;
}
export interface WorkplaceViewSubsetMetadataSealed extends WorkplaceCompositeMetadataSealed, WorkplaceExpandingMetadataSealed {
    readonly caption: string;
    readonly subset: ViewSubsetMetadataSealed | null;
    readonly subsets: ReadonlyArray<WorkplaceViewSubsetMetadataSealed>;
    readonly subsetsOwnerMetadata: WorkplaceMetadataComponentSealed | null;
    readonly visibility: SubsetVisibility;
    seal<T = WorkplaceViewSubsetMetadataSealed>(): T;
}
export declare class WorkplaceViewSubsetMetadata extends WorkplaceCompositeMetadata implements IWorkplaceViewSubsetMetadata {
    constructor();
    private _subset;
    caption: string;
    expandingMode: ExpandingMode;
    get subset(): IViewSubsetMetadata | null;
    set subset(value: IViewSubsetMetadata | null);
    get subsets(): ReadonlyArray<IWorkplaceViewSubsetMetadata>;
    subsetsOwnerMetadata: IWorkplaceComponentMetadata | null;
    visibility: SubsetVisibility;
    seal<T = WorkplaceViewSubsetMetadataSealed>(): T;
}
export declare class WorkplaceLazyViewSubsetMetadata extends WorkplaceViewSubsetMetadata {
    constructor(settings: ReadonlyArray<WorkplaceViewSubsetMetadataSealed>, metadata: ReadonlyArray<ViewSubsetMetadataSealed>);
    private _settings;
    private _metadata;
    private _subsets;
    get subsets(): ReadonlyArray<IWorkplaceViewSubsetMetadata>;
}
export declare function generateWithSubsetSettings<T extends IWorkplaceViewSubsetMetadata | WorkplaceViewSubsetMetadataSealed>(subsetsOwner: WorkplaceMetadataComponentSealed, settings: ReadonlyArray<WorkplaceViewSubsetMetadataSealed>, metadata: ReadonlyArray<ViewSubsetMetadataSealed>): T[];
