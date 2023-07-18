import { ITreeItemVisitor, ITreeItemAsyncVisitor } from './treeItemVisitor';
import { ITreeItemMenuContext } from './treeItemMenuContext';
import { ITreeItemUpdateContext } from './treeItemUpdateContext';
import { IWorkplaceViewModel } from '../../workplaceViewModel';
import { TreeItemContentFactory } from '../contentProviderCreationStrategy';
import { IExtensionMetadata, IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from 'tessa/views/workplaces';
import { EventHandler, Visibility } from 'tessa/platform';
import { MenuAction } from 'tessa/ui/menuAction';
import { IUIContext } from 'tessa/ui/uiContext';
import { IExtensionExecutor } from 'tessa/extensions';
export interface ITreeItem {
    readonly uiId: guid;
    compositionId: guid;
    contentProviderFactory: TreeItemContentFactory | null;
    readonly contextMenuGenerators: ((ctx: ITreeItemMenuContext) => void)[];
    readonly extensions: ReadonlyArray<IExtensionMetadata>;
    icon: string;
    expandedIcon: string;
    isExpanded: boolean;
    isSelected: boolean;
    readonly items: ReadonlyArray<ITreeItem>;
    readonly visibleItems: ReadonlyArray<ITreeItem>;
    readonly hasItems: boolean;
    ownerId: guid;
    parent: ITreeItem | null;
    switchExpandOnSingleClick: boolean;
    text: string;
    readonly uiContextExecutor: (action: (context: IUIContext) => void) => void;
    visibility: Visibility;
    readonly isLoading: boolean;
    workplace: IWorkplaceViewModel;
    lastUpdateTime: number;
    isBypassCache: boolean;
    readonly expanded: EventHandler<(args: {
        item: ITreeItem;
    }) => void>;
    readonly extensionExecutor: IExtensionExecutor | null;
    addItem(item: ITreeItem): boolean;
    addItems(items: ITreeItem[]): any;
    insertItem(index: number, item: ITreeItem): boolean;
    getContextMenu(): ReadonlyArray<MenuAction>;
    initialize(): any;
    dispose(): any;
    removeItem(treeItem: ITreeItem, withDispose?: boolean): boolean;
    removeItems(removingPolicy: (t: ITreeItem) => boolean, withDispose?: boolean): any;
    removeItems(items: ITreeItem[], withDispose?: boolean): any;
    tryGetItemById(compositionId: guid): ITreeItem | null;
    hasItem(item: ITreeItem): boolean;
    visit(visitor: ITreeItemVisitor): boolean;
    visitAsync(visitor: ITreeItemAsyncVisitor): Promise<boolean>;
    canRefreshNode(): boolean;
    refreshNode(context?: ITreeItemUpdateContext | null): Promise<void>;
    expand(): Promise<void>;
    hasSelection(): boolean;
    setExtensionExecutor(extensionExecutor: IExtensionExecutor): void;
}
export interface ITreeItemWithMetadata<TMetadata extends IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed> extends ITreeItem {
    readonly metadata: TMetadata;
}
