import { IGridCellViewModel, IGridRowTagViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import React from 'react';
import { ClassNameList, MenuAction } from 'tessa/ui';
import { ITableRowViewModel } from 'tessa/ui/views/content';
export declare class TableRowAdapter implements IGridRowViewModel {
    constructor(viewModel: ITableRowViewModel);
    private _viewModel;
    private _cells;
    get id(): string;
    get parentId(): string | null;
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get showChildren(): boolean;
    set showChildren(value: boolean);
    get showOverflow(): boolean;
    set showOverflow(value: boolean);
    get toolTip(): string;
    get isLastSelected(): boolean;
    get cells(): ReadonlyArray<IGridCellViewModel>;
    get className(): ClassNameList;
    get tags(): IGridRowTagViewModel[] | undefined;
    getContextMenu: () => ReadonlyArray<MenuAction>;
    onClick: (e: React.MouseEvent) => void;
    onDoubleClick: (e: React.MouseEvent) => void;
    onMouseDown: (e: React.MouseEvent) => void;
}
