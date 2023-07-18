import { TableGridViewModelBase } from './tableGridViewModelBase';
import { ContentPlaceArea } from './contentPlaceArea';
import { ITableColumnViewModel, ITableColumnViewModelCreateOptions } from './tableColumnViewModel';
import { ITableBlockViewModel } from './tableBlockViewModel';
import type { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IUIContext } from 'tessa/ui/uiContext';
import type { MenuAction } from 'tessa/ui/menuAction';
import { ViewReferenceMetadataSealed } from 'tessa/views/metadata';
export interface TableGridColumnMenuContext {
    readonly tableGrid: TableGridViewModel;
    readonly column: ITableColumnViewModel;
    readonly menuActions: MenuAction[];
}
export interface TableGridBlockMenuContext {
    readonly tableGrid: TableGridViewModel;
    readonly block: ITableBlockViewModel;
    readonly menuActions: MenuAction[];
}
export declare class TableGridViewModel extends TableGridViewModelBase {
    constructor(viewComponent: IWorkplaceViewComponent, area?: ContentPlaceArea, order?: number);
    createColumnAction: (options: ITableColumnViewModelCreateOptions) => ITableColumnViewModel;
    readonly columnContextMenuGenerators: ((ctx: TableGridColumnMenuContext) => void)[];
    readonly blockContextMenuGenerators: ((ctx: TableGridBlockMenuContext) => void)[];
    initialize(): void;
    protected isReferencedColumn(refSection: ReadonlyArray<string> | null, referenceMetadata: ViewReferenceMetadataSealed | undefined): boolean;
    protected getColumnContextMenu: (column: ITableColumnViewModel) => ReadonlyArray<MenuAction>;
    protected getRowContextMenu: () => ReadonlyArray<MenuAction>;
    protected getBlockContextMenu: (block: ITableBlockViewModel) => ReadonlyArray<MenuAction>;
    getUIContext(): IUIContext;
}
