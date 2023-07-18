import { ITessaView, ITessaViewRequest, ITessaViewResult, Paging } from 'tessa/views';
import { IViewMetadata, ViewMetadataSealed } from 'tessa/views/metadata';
export declare class NamedRecordsView<T> implements ITessaView {
    constructor(args: {
        refSection: string;
        items: T[];
        mapper: (item: T) => any[];
        nameSelector: (item: T) => string;
        filterFunc?: (item: T) => boolean;
        itemsSourceFuncAsync?: (items: T[]) => Promise<T[]>;
        modifyMetadataFunc?: (meta: IViewMetadata) => void;
        pageLimit?: number;
        enableSorting?: boolean;
        localize?: boolean;
        multySelect?: boolean;
        startsWithFilter?: boolean;
    });
    protected _items: T[];
    protected _mapper: (item: T) => any[];
    protected _nameSelector: (item: T) => string;
    protected _filterFunc?: (item: T) => boolean;
    protected _itemsSourceFuncAsync?: (items: T[]) => Promise<T[]>;
    protected _pagingMode: Paging;
    protected _enableSorting: boolean;
    protected _startsWithFilter: boolean;
    readonly metadata: ViewMetadataSealed;
    getData(request: ITessaViewRequest): Promise<ITessaViewResult>;
}
