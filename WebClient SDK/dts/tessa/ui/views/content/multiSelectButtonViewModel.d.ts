import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IViewComponentBase } from '../viewComponentBase';
import { Visibility } from 'tessa/platform';
export declare class MultiSelectButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: number);
    protected _toolTip: string;
    protected _visibility: Visibility;
    get isLoading(): boolean;
    get multiSelectEnabled(): boolean;
    get toolTip(): string;
    set toolTip(value: string);
    get visibility(): Visibility;
    set visibility(value: Visibility);
    changeMultiSelectEnabled(): void;
}
