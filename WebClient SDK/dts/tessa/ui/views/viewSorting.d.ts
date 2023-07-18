import { ViewMetadataSealed } from 'tessa/views/metadata/viewMetadata';
import { SortingColumn, SortDirection } from 'tessa/views/sortingColumn';
export interface IViewSorting {
    readonly columns: readonly SortingColumn[];
    sortColumn(alias: string, shiftDown: boolean, descendingByDefault: boolean): SortDirection | null;
    setColumns(columns: readonly SortingColumn[] | null): void;
    clear(): void;
}
export declare class ViewSorting implements IViewSorting {
    constructor(viewMetadata: ViewMetadataSealed | null, sortingColumns?: readonly SortingColumn[] | null);
    private _viewMetadata;
    private _sortingColumns;
    private _atom;
    get columns(): readonly SortingColumn[];
    sortColumn(alias: string, shiftDown: boolean, descendingByDefault: boolean): SortDirection | null;
    setColumns(columns: readonly SortingColumn[] | null): void;
    clear(): void;
    private setColumnsCore;
    private setDefaultSorting;
    private static getNextDirection;
}
