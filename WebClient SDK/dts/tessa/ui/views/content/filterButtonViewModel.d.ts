import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { Visibility } from 'tessa/platform';
import { IViewComponentBase } from '../viewComponentBase';
import { RequestParameter } from 'tessa/views/metadata';
export interface IFilterButtonViewModel {
    toolTip: string;
    visibility: Visibility;
    icon: string;
    readonly isLoading: boolean;
    openFilter(params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }): any;
}
export declare class FilterButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> implements IFilterButtonViewModel {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: number);
    protected _toolTip: string;
    protected _visibility: Visibility;
    protected _icon: string;
    get toolTip(): string;
    set toolTip(value: string);
    get visibility(): Visibility;
    set visibility(value: Visibility);
    get isLoading(): boolean;
    get icon(): string;
    set icon(icon: string);
    openFilter(params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }): Promise<void>;
}
