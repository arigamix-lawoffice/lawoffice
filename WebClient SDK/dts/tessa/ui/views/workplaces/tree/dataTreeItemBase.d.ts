import { TreeItemWithMetadataBase } from './treeItemWithMetadataBase';
import { ITreeItemWithMetadata } from './treeItem';
import { IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from 'tessa/views/workplaces';
import { IViewParameters } from 'tessa/ui/views/parameters';
import { ITessaViewRequest, ITessaViewResult, ITessaView } from 'tessa/views';
export interface IDataTreeItemBase<TMetadata extends IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed> extends ITreeItemWithMetadata<TMetadata> {
    readonly parameters: IViewParameters;
    view: ITessaView;
    getData(r: ITessaViewRequest): Promise<ITessaViewResult>;
}
export declare abstract class DataTreeItemBase<TMetadata extends IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed> extends TreeItemWithMetadataBase<TMetadata> implements IDataTreeItemBase<TMetadata> {
    constructor(metadata: TMetadata, parameters: IViewParameters);
    private _parametersReaction;
    readonly parameters: IViewParameters;
    view: ITessaView;
    initialize(): void;
    dispose(): void;
    protected onParametersChanged(): void;
    abstract getData(r: ITessaViewRequest): Promise<ITessaViewResult>;
}
