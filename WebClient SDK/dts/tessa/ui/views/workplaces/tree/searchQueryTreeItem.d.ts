import { SubsetsContainer, ISubsetsContainer } from './subsetsContainer';
import { IViewParameters } from '../../parameters';
import { WorkplaceViewSubsetMetadataSealed, WorkplaceSearchQueryMetadataSealed } from 'tessa/views/workplaces';
import { ViewMetadataSealed } from 'tessa/views/metadata';
export interface ISearchQueryTreeItem extends ISubsetsContainer<WorkplaceSearchQueryMetadataSealed> {
    readonly viewMetadata: ViewMetadataSealed | null;
}
export declare class SearchQueryTreeItem extends SubsetsContainer<WorkplaceSearchQueryMetadataSealed> implements ISearchQueryTreeItem {
    constructor(metadata: WorkplaceSearchQueryMetadataSealed, viewMetadata: ViewMetadataSealed | null, parameters: IViewParameters, subsetsMetadata: WorkplaceViewSubsetMetadataSealed[]);
    readonly viewMetadata: ViewMetadataSealed | null;
    protected onParametersChanged(): void;
}
