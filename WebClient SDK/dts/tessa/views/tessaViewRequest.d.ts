import { RequestParameter } from './metadata/requestParameter';
import { SortingColumn } from './sortingColumn';
import { IViewMetadata, ViewMetadataSealed } from './metadata/viewMetadata';
export interface ITessaViewRequest {
    sortingColumns: SortingColumn[];
    subsetName: string | null;
    calculateRowCounting: boolean;
    values: RequestParameter[];
    view: ViewMetadataSealed;
    viewAlias: string;
    executionTimeOut: number | null;
}
export declare class TessaViewRequest implements ITessaViewRequest {
    constructor(viewMetadata: IViewMetadata);
    constructor(viewMetadata: ViewMetadataSealed);
    calculateRowCounting: boolean;
    executionTimeOut: number | null;
    sortingColumns: SortingColumn[];
    subsetName: string | null;
    values: RequestParameter[];
    view: ViewMetadataSealed;
    viewAlias: string;
}
