import { SubsetsContainer, ISubsetsContainer } from './subsetsContainer';
import { IViewParameters } from '../../parameters';
import { DataNodeMetadataSealed, WorkplaceViewSubsetMetadataSealed } from 'tessa/views/workplaces';
export interface IViewTreeItem extends ISubsetsContainer<DataNodeMetadataSealed> {
}
export declare class ViewTreeItem extends SubsetsContainer<DataNodeMetadataSealed> implements IViewTreeItem {
    constructor(metadata: DataNodeMetadataSealed, parameters: IViewParameters, subsetsMetadata: WorkplaceViewSubsetMetadataSealed[]);
    protected onParametersChanged(): void;
}
