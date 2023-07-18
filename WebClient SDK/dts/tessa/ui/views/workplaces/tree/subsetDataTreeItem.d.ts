import { SubsetsContainer, ISubsetsContainer } from './subsetsContainer';
import { IViewParameters } from '../../parameters';
import { ISubsetData } from '../../subsetData';
import { WorkplaceViewSubsetMetadataSealed } from 'tessa/views/workplaces';
import { ITessaViewRequest, ITessaViewResult, ITessaView } from 'tessa/views';
export interface ISubsetDataTreeItem extends ISubsetsContainer<WorkplaceViewSubsetMetadataSealed> {
    view: ITessaView;
    readonly count: string;
    readonly hasChild: boolean;
    readonly value: any;
    getData(request: ITessaViewRequest): Promise<ITessaViewResult>;
    updateData(data: ISubsetData): any;
}
export declare class SubsetDataTreeItem extends SubsetsContainer<WorkplaceViewSubsetMetadataSealed> implements ISubsetDataTreeItem {
    constructor(metadata: WorkplaceViewSubsetMetadataSealed, parameters: IViewParameters, data: ISubsetData);
    protected _data: ISubsetData;
    view: ITessaView;
    get count(): string;
    get hasChild(): boolean;
    get value(): any;
    getData(request: ITessaViewRequest): Promise<ITessaViewResult>;
    updateData(data: ISubsetData): void;
    protected onParametersChanged(): void;
}
