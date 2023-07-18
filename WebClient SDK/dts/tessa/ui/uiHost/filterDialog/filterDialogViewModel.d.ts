import { FilterDialogItemViewModel } from './filterDialogItemViewModel';
import { FilterDialogCriteriaViewModel } from './filterDialogCriteriaViewModel';
import { FilterSearchQueryViewModel } from './filterSearchQueryViewModel';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
export declare class FilterDialogViewModel {
    constructor(metadata: ViewMetadataSealed, parameters: ReadonlyArray<RequestParameter>);
    private _metadata;
    private _items;
    private _showHidden;
    private readonly _searchQuery;
    onUIChanged?: (ctx: FilterDialogViewModel) => void;
    get items(): ReadonlyArray<FilterDialogItemViewModel>;
    get showHidden(): boolean;
    set showHidden(value: boolean);
    get searchQuery(): FilterSearchQueryViewModel;
    initFocusParam?: {
        requestParam: RequestParameter;
        criteriaIndex: number;
    } | null;
    private createItems;
    private createItem;
    private createCriteria;
    canAddNewCriteria(item: FilterDialogItemViewModel): boolean;
    addNewCriteria(item: FilterDialogItemViewModel): void;
    canDeleteCriteria(criteria: FilterDialogCriteriaViewModel): boolean;
    deleteCriteria: (criteria: FilterDialogCriteriaViewModel) => void;
    canClearAll(item: FilterDialogItemViewModel): boolean;
    clearAll(item: FilterDialogItemViewModel): void;
    private getAvailableCriterias;
    getRequestParameters(): RequestParameter[];
    private processSelectedRowsToFilter;
    private currentQueryChanged;
    saveAsSearchQuery(): Promise<void>;
}
