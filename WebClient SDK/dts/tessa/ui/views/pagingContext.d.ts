import { Paging } from 'tessa/views/paging';
export declare class PagingContext {
    pagingMode: Paging;
    optionalPagingStatus: boolean;
    pageOffset: number;
    pageLimit: number;
    constructor(pagingMode: Paging, optionalPagingStatus: boolean, pageOffset: number, pageLimit: number);
}
