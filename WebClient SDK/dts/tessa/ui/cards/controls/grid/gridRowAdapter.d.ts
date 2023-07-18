/// <reference types="react" />
import { IGridCellViewModel, IGridRowViewModel } from 'components/cardElements/grid';
import { GridRowViewModel, GridViewModel } from 'tessa/ui/cards/controls';
import { MenuAction } from 'tessa/ui/menuAction';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class GridRowAdapter implements IGridRowViewModel {
    constructor(viewModel: GridRowViewModel, control: GridViewModel, index: number);
    private _viewModel;
    private _control;
    private _cells;
    private _index;
    get id(): string;
    get index(): number;
    get parentId(): string | null;
    get style(): React.CSSProperties;
    get showChildren(): boolean;
    set showChildren(value: boolean);
    get showOverflow(): boolean;
    set showOverflow(value: boolean);
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get isLastSelected(): boolean;
    get cells(): IGridCellViewModel[];
    get className(): ClassNameList;
    onDoubleClick(): void;
    getContextMenu: (_e: React.MouseEvent, columnId?: string | undefined) => ReadonlyArray<MenuAction>;
}
