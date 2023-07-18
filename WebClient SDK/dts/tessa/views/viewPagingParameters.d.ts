import { Paging } from './paging';
import { RequestParameter } from './metadata/requestParameter';
import { IViewParameterMetadata } from './metadata/viewParameterMetadata';
export declare class ViewPagingParameters {
    getPageLimitParameter(pageLimit?: number, hidden?: boolean, readOnly?: boolean): RequestParameter;
    providePageLimitParameter(parameters: RequestParameter[], paging: Paging, pageLimit: number, optionalPaging: boolean): void;
    getPageOffsetParameter(currentPage?: number, pageLimit?: number, hidden?: boolean, readOnly?: boolean): RequestParameter;
    providePageOffsetParameter(parameters: RequestParameter[], paging: Paging, currentPage: number, pageLimit: number, optionalPaging: boolean): void;
    readonly pageLimit: string;
    readonly pageLimitName: string;
    getPageLimitParameterMetadata(hidden?: boolean): IViewParameterMetadata;
    readonly pageOffset: string;
    readonly pageOffsetName: string;
    getPageOffsetParameterMetadata(hidden?: boolean): IViewParameterMetadata;
}
