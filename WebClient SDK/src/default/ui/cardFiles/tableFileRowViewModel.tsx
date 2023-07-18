import { reaction, comparer } from 'mobx';
import { FileCaptionCell } from './fileCaptionCell';
import {
  TableRowViewModel,
  ITableRowViewModelCreateOptions,
  ITableCellViewModel
} from 'tessa/ui/views/content';
import { FileViewModel, FileListViewModel } from 'tessa/ui/cards/controls';
import { ThemeManager } from 'tessa/ui/themes';
import React from 'react';

export class TableFileRowViewModel extends TableRowViewModel {
  constructor(args: ITableRowViewModelCreateOptions, fileControl: FileListViewModel) {
    super(args);

    this.fileViewModel = args.data.get('FileViewModel') as FileViewModel;
    this.fileControl = fileControl;
    this.style = this.getStyleForFile();
  }

  private readonly _disposes: Array<Function | null> = [];

  readonly fileViewModel: FileViewModel;

  readonly fileControl: FileListViewModel;

  public initialize(cells: ReadonlyMap<string, ITableCellViewModel>) {
    super.initialize(cells);

    this._disposes.push(
      reaction(
        () => {
          const file = this.fileViewModel;
          return {
            isMofidied: file.isModified,
            caption: file.caption,
            tag: file.tag,
            category: file.model.category,
            versionAdded: file.model.versionAdded
          };
        },
        () => {
          const viewComponent = this.grid.viewComponent;
          viewComponent.refresh();
        },
        {
          equals: comparer.shallow
        }
      )
    );

    const captionCell = this.getByName('Caption');
    if (captionCell) {
      captionCell.getContent = () => (
        <FileCaptionCell cell={captionCell} file={this.fileViewModel} />
      );
    }

    const categoryCell = this.getByName('CategoryCaption');
    if (categoryCell) {
      const style = categoryCell.style;
      categoryCell.style = Object.assign({}, style, { whiteSpace: 'nowrap' });
    }
    this.style = this.getStyleForFile();
  }

  public dispose() {
    super.dispose();
    for (const dispose of this._disposes) {
      if (dispose) {
        dispose();
      }
    }
    this._disposes.length = 0;
  }

  private getStyleForFile(): React.CSSProperties {
    const state = this.fileViewModel.getFileState();
    const theme = ThemeManager.instance.currentTheme.settings.files;
    let background = '';
    switch (state) {
      case 'Error':
        background = theme.fileErrorBackground;
        break;
      case 'Uploading':
        background = theme.fileBusyBackground;
        break;
      case 'Modified':
        background = theme.fileModifiedBackground;
        break;
    }
    return Object.assign({}, this.style, {
      background
    });
  }
}
