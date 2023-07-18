import { SortingColumn } from 'tessa/views/sortingColumn';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
export declare class ViewPlaceholderInfo {
    constructor();
    private _currentPage;
    private _pageLimit;
    parameters: RequestParameter[];
    sortingColumns: SortingColumn[];
    get currentPage(): number;
    set currentPage(value: number);
    get pageLimit(): number;
    set pageLimit(value: number);
}
