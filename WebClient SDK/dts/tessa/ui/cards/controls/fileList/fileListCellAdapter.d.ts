import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import React from 'react';
import { MenuAction } from 'tessa/ui/menuAction';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class FileListCellAdapter implements IGridCellViewModel {
    constructor(content: string | (() => string), columnId: string, parent: IGridRowViewModel);
    private _content;
    private _contentProvider;
    private _columnId;
    private _parent;
    get content(): string;
    get columnId(): string;
    get parent(): IGridRowViewModel;
    getContextMenu: (e: React.MouseEvent) => ReadonlyArray<MenuAction>;
    readonly className: ClassNameList;
}
