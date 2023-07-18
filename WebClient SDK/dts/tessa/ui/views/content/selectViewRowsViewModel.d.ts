import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { Visibility } from 'tessa/platform';
export declare class SelectViewRowsViewModel extends BaseContentItem {
    constructor(viewComponent: IWorkplaceViewComponent, area?: ContentPlaceArea, order?: number);
    private _visibility;
    get visibility(): Visibility;
    get isLoading(): boolean;
    private getVisibility;
    selectItems(): void;
}
