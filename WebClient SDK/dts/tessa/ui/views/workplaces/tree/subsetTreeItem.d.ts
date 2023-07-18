import { DataTreeItemBase, IDataTreeItemBase } from './dataTreeItemBase';
import { IViewParameters } from '../../parameters';
import { WorkplaceViewSubsetMetadataSealed } from 'tessa/views/workplaces';
import { ITessaViewRequest, ITessaViewResult } from 'tessa/views';
export interface ISubsetTreeItem extends IDataTreeItemBase<WorkplaceViewSubsetMetadataSealed> {
    readonly closeable: boolean;
    readonly subsetsMetadata: ReadonlyArray<WorkplaceViewSubsetMetadataSealed>;
}
export declare class SubsetTreeItem extends DataTreeItemBase<WorkplaceViewSubsetMetadataSealed> implements ISubsetTreeItem {
    constructor(metadata: WorkplaceViewSubsetMetadataSealed, parameters: IViewParameters, closeable?: boolean);
    protected _closeable: boolean;
    get closeable(): boolean;
    get subsetsMetadata(): ReadonlyArray<WorkplaceViewSubsetMetadataSealed>;
    getData(request: ITessaViewRequest): Promise<ITessaViewResult>;
    protected onParametersChanged(): void;
}
