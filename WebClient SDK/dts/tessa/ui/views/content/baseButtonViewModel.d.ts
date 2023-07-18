import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IViewComponentBase } from '../viewComponentBase';
import { Visibility } from 'tessa/platform';
import { ClassNameList } from 'tessa/ui/classNameList';
export interface IBaseButtonViewModel {
    icon: string;
    toolTip: string;
    visibility: Visibility;
    readonly className: ClassNameList;
    readonly isLoading: boolean;
    onClick(): void;
}
export declare class BaseButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> implements IBaseButtonViewModel {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: number);
    protected _icon: string;
    protected _toolTip: string;
    protected _visibility: Visibility;
    get icon(): string;
    set icon(value: string);
    get toolTip(): string;
    set toolTip(value: string);
    get visibility(): Visibility;
    set visibility(value: Visibility);
    readonly className: ClassNameList;
    get isLoading(): boolean;
    onClick: () => void;
}
