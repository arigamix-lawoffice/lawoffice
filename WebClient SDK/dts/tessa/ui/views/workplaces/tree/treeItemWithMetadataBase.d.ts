import { TreeItemBase } from './treeItemBase';
import { ITreeItemWithMetadata } from './treeItem';
import { IWorkplaceComponentMetadata, IExtensionMetadata, WorkplaceMetadataComponentSealed } from 'tessa/views/workplaces';
export declare abstract class TreeItemWithMetadataBase<TMetadata extends IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed> extends TreeItemBase implements ITreeItemWithMetadata<TMetadata> {
    constructor(metadata: TMetadata);
    protected _metadata: TMetadata;
    get metadata(): TMetadata;
    protected getExtensions(): ReadonlyArray<IExtensionMetadata>;
}
