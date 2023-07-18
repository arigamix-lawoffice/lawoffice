import {
  BaseViewControlItem,
  ViewControlViewModel,
  FileListViewModel
} from 'tessa/ui/cards/controls';
import { MenuAction } from 'tessa/ui';

export class ShowContextMenuButtonViewModel extends BaseViewControlItem {
  constructor(viewControl: ViewControlViewModel, fileControl: FileListViewModel) {
    super(viewControl);
    this._fileControl = fileControl;
  }

  private _fileControl: FileListViewModel;

  getMenuActions(): MenuAction[] {
    const actions = this._fileControl.getControlActions() as MenuAction[];
    return actions.filter(x => x.name !== 'Sortings' && x.name !== 'Multiselect');
  }
}
