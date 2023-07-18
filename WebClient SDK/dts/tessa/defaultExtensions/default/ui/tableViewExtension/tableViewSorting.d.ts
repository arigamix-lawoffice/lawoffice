import { IViewSorting } from 'tessa/ui/views/viewSorting';
import { SortingColumn, SortDirection } from 'tessa/views/sortingColumn';
export declare class TableViewSorting implements IViewSorting {
    constructor();
    private _sortingColumns;
    private _atom;
    get columns(): ReadonlyArray<SortingColumn>;
    sortColumn(alias: string, _shiftDown: boolean, descendingByDefault: boolean): SortDirection | null;
    setColumns(columns: readonly SortingColumn[] | null): void;
    clear(): void;
}
