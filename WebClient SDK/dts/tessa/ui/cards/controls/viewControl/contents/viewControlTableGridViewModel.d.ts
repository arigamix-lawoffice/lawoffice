import type { ViewControlViewModel } from '../viewControlViewModel';
import type { ITableColumnViewModel, ITableRowViewModel, ITableCellViewModel, ITableBlockViewModel } from 'tessa/ui/views/content';
import { TableGridViewModelBase } from 'tessa/ui/views/content';
import { IUIContext } from 'tessa/ui/uiContext';
import type { MenuAction } from 'tessa/ui/menuAction';
export interface ViewControlColumnMenuContext {
    readonly tableGrid: ViewControlTableGridViewModel;
    readonly column: ITableColumnViewModel;
    readonly menuActions: MenuAction[];
}
export interface ViewControlRowMenuContext {
    readonly tableGrid: ViewControlTableGridViewModel;
    readonly row: ITableRowViewModel;
    readonly cell: ITableCellViewModel | null;
    readonly menuActions: MenuAction[];
}
export interface ViewControlBlockMenuContext {
    readonly tableGrid: ViewControlTableGridViewModel;
    readonly block: ITableBlockViewModel;
    readonly menuActions: MenuAction[];
}
export declare class ViewControlTableGridViewModel extends TableGridViewModelBase<ViewControlViewModel> {
    constructor(viewComponent: ViewControlViewModel);
    readonly columnContextMenuGenerators: ((ctx: ViewControlColumnMenuContext) => void)[];
    readonly rowContextMenuGenerators: ((ctx: ViewControlRowMenuContext) => void)[];
    readonly blockContextMenuGenerators: ((ctx: ViewControlBlockMenuContext) => void)[];
    initialize(): void;
    protected getColumnContextMenu: (column: ITableColumnViewModel) => ReadonlyArray<MenuAction>;
    protected getRowContextMenu: (row: ITableRowViewModel) => ReadonlyArray<MenuAction>;
    protected getBlockContextMenu: (block: ITableBlockViewModel) => ReadonlyArray<MenuAction>;
    getUIContext(): IUIContext;
}
