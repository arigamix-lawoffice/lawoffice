import { ViewSelectionMode } from 'tessa/views/metadata/viewSelectionMode';
export interface ISelectionState {
    readonly multiSelect: boolean;
    readonly selectionMode: ViewSelectionMode;
    readonly selectedColumn: string;
    readonly selectedColumnValue: any | null;
    readonly selectedRow: ReadonlyMap<string, any> | null;
    readonly selectedRows: ReadonlyArray<ReadonlyMap<string, any>> | null;
    assign(state: ISelectionState): any;
    clone(): ISelectionState;
    clear(): any;
    selectRow(row: ReadonlyMap<string, any>): any;
    unSelectRow(row: ReadonlyMap<string, any>): any;
    setSelection(row: ReadonlyMap<string, any> | null, column?: string): any;
    unSelectAllRows(): any;
}
export declare class SelectionState implements ISelectionState {
    constructor(multiSelect: boolean, selectionMode: ViewSelectionMode, selectedColumn?: string, selectedRow?: ReadonlyMap<string, any> | null, selectedRows?: ReadonlyArray<ReadonlyMap<string, any>> | null);
    private _rowsAtom;
    private _multiSelect;
    private _selectionMode;
    private _selectedColumn;
    private _selectedColumnValue;
    private _selectedRow;
    private _selectedRows;
    get multiSelect(): boolean;
    get selectionMode(): ViewSelectionMode;
    get selectedColumn(): string;
    get selectedColumnValue(): any | null;
    get selectedRow(): ReadonlyMap<string, any> | null;
    get selectedRows(): ReadonlyArray<ReadonlyMap<string, any>> | null;
    assign(state: ISelectionState): void;
    clone(): ISelectionState;
    clear(): void;
    selectRow(row: ReadonlyMap<string, any>): void;
    unSelectRow(row: ReadonlyMap<string, any>): void;
    unSelectAllRows(): void;
    setSelection(row: ReadonlyMap<string, any> | null, column?: string): void;
}
