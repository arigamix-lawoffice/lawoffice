import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import React from 'react';
import { IFileSignature } from 'tessa/files';
import { ClassNameList, MenuAction } from 'tessa/ui';
export declare class FileListSignsRowAdapter implements IGridRowViewModel {
    constructor(signature: IFileSignature, selectedRows: IGridRowViewModel[], canOpenSignatureInfo: (sign: IFileSignature) => boolean, removeSignature: (sign: IFileSignature) => Promise<void>);
    private _cells;
    private _signature;
    private _selectedRows;
    private _canOpenSignatureInfo;
    private _removeSignature;
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
    readonly className: ClassNameList;
    onDoubleClick: (e: React.MouseEvent) => Promise<void>;
    getContextMenu: (_e: React.SyntheticEvent | React.KeyboardEvent, _columnId?: string | undefined) => ReadonlyArray<MenuAction>;
}
