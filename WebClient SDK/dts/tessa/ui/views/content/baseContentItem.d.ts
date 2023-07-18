import { ContentPlaceArea } from './contentPlaceArea';
import { IViewComponentBase } from '../viewComponentBase';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
export interface IBaseContentItem {
    readonly area: ContentPlaceArea;
    readonly order: number;
    initialize(): any;
    dispose(): any;
}
export declare abstract class BaseContentItem<T extends IViewComponentBase = IWorkplaceViewComponent> implements IBaseContentItem {
    constructor(viewComponent: T, area: ContentPlaceArea, order: number);
    readonly viewComponent: T;
    readonly area: ContentPlaceArea;
    readonly order: number;
    initialize(): void;
    dispose(): void;
}
