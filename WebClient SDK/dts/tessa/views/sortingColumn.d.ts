export declare enum SortDirection {
    Ascending = 0,
    Descending = 1
}
export declare class SortingColumn {
    constructor(alias?: string, direction?: SortDirection);
    alias: string;
    sortDirection: SortDirection;
}
