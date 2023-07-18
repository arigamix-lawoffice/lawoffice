import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import React from 'react';
import { IFileVersion } from 'tessa/files';
import { MenuAction } from 'tessa/ui/menuAction';
export declare class FileListRowAdapter implements IGridRowViewModel {
    constructor(version: IFileVersion, onClick: (e: React.MouseEvent, row: IGridRowViewModel) => void, selectedRows: IGridRowViewModel[], getContextMenu: (e: React.MouseEvent, row: IGridRowViewModel) => ReadonlyArray<MenuAction>);
    private _cells;
    private _version;
    private _selectedRows;
    private _getContextMenu;
    private _onClick;
    private _isSelected;
    private _isToggled;
    get id(): string;
    get cells(): IGridCellViewModel[];
    get showChildren(): boolean;
    set showChildren(value: boolean);
    get showOverflow(): boolean;
    set showOverflow(value: boolean);
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get isLastSelected(): boolean;
    getContextMenu: (e: React.MouseEvent) => ReadonlyArray<MenuAction>;
    onClick: (e: React.MouseEvent) => void;
}
