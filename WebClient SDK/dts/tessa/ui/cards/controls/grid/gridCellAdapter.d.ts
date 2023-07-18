/// <reference types="react" />
import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import { GridCellViewModel, GridRowViewModel, GridViewModel } from 'tessa/ui/cards/controls';
import { MenuAction } from 'tessa/ui/menuAction';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class GridCellAdapter implements IGridCellViewModel {
    constructor(viewModel: GridCellViewModel, control: GridViewModel, row: GridRowViewModel, parent: IGridRowViewModel, columnId: string);
    private _viewModel;
    private _control;
    private _row;
    private _parent;
    private _columnId;
    get columnId(): string;
    get parent(): IGridRowViewModel;
    get content(): any;
    get style(): React.CSSProperties;
    get tooltip(): string;
    get className(): ClassNameList;
    onDoubleClick(): void;
    getContextMenu: () => ReadonlyArray<MenuAction>;
}
