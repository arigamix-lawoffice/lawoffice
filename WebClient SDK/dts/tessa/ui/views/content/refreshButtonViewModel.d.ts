import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IViewComponentBase } from '../viewComponentBase';
import { Visibility } from 'tessa/platform';
export interface IRefreshButtonViewModel {
    toolTip: string;
    visibility: Visibility;
    readonly isLoading: boolean;
    refresh(): any;
}
export declare class RefreshButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> implements IRefreshButtonViewModel {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: number);
    private _toolTip;
    protected _visibility: Visibility;
    get toolTip(): string;
    set toolTip(value: string);
    get isLoading(): boolean;
    get visibility(): Visibility;
    set visibility(value: Visibility);
    refresh(): void;
}
