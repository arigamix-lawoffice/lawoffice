import { TreeItemWithMetadataBase } from './treeItemWithMetadataBase';
import { ITreeItemWithMetadata } from './treeItem';
import { IViewParameters } from '../../parameters';
import { WorkplaceMetadataComponentSealed, IWorkplaceComponentMetadata, WorkplaceViewSubsetMetadataSealed } from 'tessa/views/workplaces';
export interface ISubsetsContainer<TMetadata extends IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed> extends ITreeItemWithMetadata<TMetadata> {
    readonly availableSubsets: WorkplaceViewSubsetMetadataSealed[];
    readonly subsetsMetadata: WorkplaceViewSubsetMetadataSealed[];
    readonly parameters: IViewParameters;
}
export declare abstract class SubsetsContainer<TMetadata extends IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed> extends TreeItemWithMetadataBase<TMetadata> implements ISubsetsContainer<TMetadata> {
    constructor(metadata: TMetadata, parameters: IViewParameters, subsetsMetadata: WorkplaceViewSubsetMetadataSealed[]);
    private _parametersReaction;
    readonly availableSubsets: WorkplaceViewSubsetMetadataSealed[];
    readonly subsetsMetadata: WorkplaceViewSubsetMetadataSealed[];
    readonly parameters: IViewParameters;
    initialize(): void;
    dispose(): void;
    protected onParametersChanged(): void;
}
