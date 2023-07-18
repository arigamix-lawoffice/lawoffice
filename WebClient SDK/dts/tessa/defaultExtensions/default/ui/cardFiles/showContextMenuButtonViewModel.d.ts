import { BaseViewControlItem, ViewControlViewModel, FileListViewModel } from 'tessa/ui/cards/controls';
import { MenuAction } from 'tessa/ui';
export declare class ShowContextMenuButtonViewModel extends BaseViewControlItem {
    constructor(viewControl: ViewControlViewModel, fileControl: FileListViewModel);
    private _fileControl;
    getMenuActions(): MenuAction[];
}
