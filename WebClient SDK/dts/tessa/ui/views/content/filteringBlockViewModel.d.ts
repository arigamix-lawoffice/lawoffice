import { RequestParameter } from 'tessa/views/metadata';
import { IViewComponentBase, IWorkplaceViewComponent } from 'tessa/ui/views';
import { Visibility } from 'tessa/platform';
import { BaseContentItem } from './baseContentItem';
import { IQuickSearchViewModel } from './quickSearchViewModel';
import { ContentPlaceArea } from './contentPlaceArea';
import { IFilterButtonViewModel } from './filterButtonViewModel';
import { IBaseButtonViewModel } from './baseButtonViewModel';
export interface IFilteringBlockViewModel {
    readonly quickSearchViewModel: IQuickSearchViewModel;
    readonly filterButtonViewModel: IFilterButtonViewModel;
    readonly clearFilterButton: IBaseButtonViewModel;
    readonly parameters: readonly RequestParameter[];
    visibility: Visibility;
    readonly isEmpty: boolean;
    removeParameter(param: RequestParameter): void;
}
export declare class FilteringBlockViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> implements IFilteringBlockViewModel {
    constructor(viewComponent: T, quickSearch: IQuickSearchViewModel, filterButton: IFilterButtonViewModel, area?: ContentPlaceArea, order?: number);
    protected _visibility: Visibility;
    readonly quickSearchViewModel: IQuickSearchViewModel;
    readonly filterButtonViewModel: IFilterButtonViewModel;
    readonly clearFilterButton: IBaseButtonViewModel;
    get parameters(): readonly RequestParameter[];
    get visibility(): Visibility;
    set visibility(value: Visibility);
    get isEmpty(): boolean;
    removeParameter(param: RequestParameter): void;
}
