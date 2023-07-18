import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IViewParameters } from '../parameters';
import { IViewComponentBase } from '../viewComponentBase';
import { Visibility } from 'tessa/platform';
import { RequestParameter } from 'tessa/views/metadata';
export interface IFilterTextViewModel {
    readonly parameters: IViewParameters;
    readonly visibility: Visibility;
    readonly canOpenFilter: boolean;
    readonly canClearFilter: boolean;
    readonly isLoading: boolean;
    showContextButtons: boolean;
    openFilter: (params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }) => Promise<void>;
    clearFilter: () => void;
}
export declare class FilterTextViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> implements IFilterTextViewModel {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: number);
    protected _showContextButtons: boolean;
    get parameters(): IViewParameters;
    get visibility(): Visibility;
    get canOpenFilter(): boolean;
    get canClearFilter(): boolean;
    get isLoading(): boolean;
    get showContextButtons(): boolean;
    set showContextButtons(value: boolean);
    openFilter(params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }): Promise<void>;
    clearFilter(): void;
}
