import { BaseViewControlItem } from './baseViewControlItem';
import { ViewControlViewModel } from '../../viewControl/viewControlViewModel';
import { Visibility } from 'tessa/platform';
export declare class ViewControlFilterResetButtonViewModel extends BaseViewControlItem {
    constructor(viewComponent: ViewControlViewModel);
    private _toolTip;
    get toolTip(): string;
    set toolTip(value: string);
    get isLoading(): boolean;
    get visibility(): Visibility;
    reset(): void;
}
