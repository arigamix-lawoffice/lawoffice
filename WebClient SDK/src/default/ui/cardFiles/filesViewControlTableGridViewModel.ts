import {
  ViewControlTableGridViewModel,
  FileListViewModel,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';
import { tryGetFromInfo } from 'tessa/ui';
import { ValidationResultBuilder } from 'tessa/platform/validation';

export class FilesViewControlTableGridViewModel extends ViewControlTableGridViewModel {
  private _fileControl: FileListViewModel;

  get fileControl(): FileListViewModel {
    return this._fileControl;
  }

  initialize(): void {
    super.initialize();

    const fileControl = tryGetFromInfo<FileListViewModel | null>(
      this.viewComponent.cardModel.info,
      this.viewComponent.name ?? '',
      null
    );
    if (!fileControl) {
      throw new Error(`File control not found.`);
    }
    this._fileControl = fileControl;

    const viewControlFromInfo = tryGetFromInfo<ViewControlViewModel | null>(
      fileControl.info,
      '.viewControl',
      null
    );
    if (viewControlFromInfo) {
      throw new Error(`File control already initialized with view control.`);
    }
    fileControl.info['.viewControl'] = this;
  }

  dispose(): void {
    super.dispose();

    if (this.fileControl) {
      const info = this.fileControl.info;
      if (info) {
        delete info['.viewControl'];
      }

      this.fileControl.onUnloading(new ValidationResultBuilder());

      this._fileControl = null!;
    }
  }
}
