import { Visibility } from 'tessa/platform';
import { BaseContentItem } from '../views/content/baseContentItem';
import { ContentPlaceArea } from '../views/content/contentPlaceArea';
import { IViewComponentBase } from '../views/viewComponentBase';
import { IWorkplaceViewComponent } from '../views/workplaceViewComponent';
export interface IMyTagsButtonViewModel {
    toolTip: string;
    visibility: Visibility;
    isChecked: boolean;
    onClickAction: () => void;
}
export declare class MyTagsButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> implements IMyTagsButtonViewModel {
    constructor(viewComponent: T, clickAction: (viewComponent: T, isChecked: boolean) => void, area?: ContentPlaceArea, order?: number);
    private _toolTip;
    protected _visibility: Visibility;
    protected _isChecked: boolean;
    private _clickAction;
    get toolTip(): string;
    set toolTip(value: string);
    get visibility(): Visibility;
    set visibility(value: Visibility);
    get isChecked(): boolean;
    set isChecked(value: boolean);
    onClickAction(): void;
}
