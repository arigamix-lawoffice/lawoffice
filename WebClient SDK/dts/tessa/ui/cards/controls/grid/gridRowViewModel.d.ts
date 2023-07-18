/// <reference types="react" />
import { GridCellViewModel } from './gridCellViewModel';
import { GridColumnInfo } from './gridColumnInfo';
import { Card, CardRow, CardRowState } from 'tessa/cards';
import { ISelectionState } from 'tessa/ui/views/selectionState';
import { GridViewModel } from 'tessa/ui/cards/controls';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class GridRowViewModel {
    constructor(row: CardRow, card: Card, columnInfos: GridColumnInfo[], orderColumn: string | null, selectionState: ISelectionState, control: GridViewModel);
    private _row;
    private _cells;
    private _state;
    private _orderColumn;
    private _selectionState;
    private _style;
    private _isToggled;
    get row(): CardRow;
    get cells(): ReadonlyArray<GridCellViewModel>;
    get rowId(): guid;
    get state(): CardRowState;
    get order(): number;
    get isSelected(): boolean;
    set isSelected(value: boolean);
    get isLastSelected(): boolean;
    get style(): React.CSSProperties;
    set style(value: React.CSSProperties);
    get isToggled(): boolean;
    set isToggled(value: boolean);
    readonly className: ClassNameList;
    clean(): void;
    private onStateChanged;
    changeColumnOrderByTarget(sourceIndex: number, targetIndex: number): void;
    toggle: () => void;
}
