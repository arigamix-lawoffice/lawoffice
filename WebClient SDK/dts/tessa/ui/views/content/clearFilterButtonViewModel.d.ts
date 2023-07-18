import { Visibility } from 'tessa/platform';
import { IViewComponentBase, IWorkplaceViewComponent } from 'tessa/ui/views';
import { ContentPlaceArea } from './contentPlaceArea';
import { ContentPlaceOrder } from './contentPlaceOrder';
import { RequestParameter } from 'tessa/views/metadata';
import { BaseButtonViewModel } from './baseButtonViewModel';
export declare class ClearFilterButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseButtonViewModel<T> {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: ContentPlaceOrder);
    get visibility(): Visibility;
    get parameters(): readonly RequestParameter[];
}
