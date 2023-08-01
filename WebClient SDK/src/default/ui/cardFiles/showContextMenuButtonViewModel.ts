import { BaseViewControlItem, ViewControlViewModel, FileListViewModel } from 'tessa/ui/cards/controls';
import { MenuAction, showFileDialog } from 'tessa/ui';
import { FileSelectContext } from 'tessa/ui/files';
import { FileHelper } from 'src/law/helpers/fileHelper';

export class ShowContextMenuButtonViewModel extends BaseViewControlItem {
  constructor(viewControl: ViewControlViewModel, fileControl: FileListViewModel) {
    super(viewControl);
    this._fileControl = fileControl;
  }

  private _fileControl: FileListViewModel;

  getMenuActions(): MenuAction[] {
    const actions = this._fileControl.getControlActions() as MenuAction[];
    const uploadButton = actions.find(button => button.name === 'Upload');
    if (uploadButton) {
      uploadButton.action = () => this.uploadAction(this._fileControl);
    }

    return actions.filter(x => x.name !== 'Sortings' && x.name !== 'Multiselect');
  }

  private async uploadAction(fileControl: FileListViewModel) {
    const modifyFileSelect = fileControl.modifyFileSelect;
    let accept: string | undefined;
    if (modifyFileSelect) {
      const context = new FileSelectContext(null);
      modifyFileSelect(context);
      accept = context.selectFileDialogAccept;
    }

    const contents = await showFileDialog(true, accept);
    if (contents.length === 0) {
      return;
    }

    await FileHelper.addFilesAsync(fileControl, contents);
  }
}
