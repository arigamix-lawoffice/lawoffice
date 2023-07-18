import React from 'react';
import { Observer } from 'mobx-react';
import { ITableCellViewModel } from 'tessa/ui/views/content';
import { FileViewModel } from 'tessa/ui/cards/controls';
import { FileListTags } from 'tessa/ui/cards/components/controls';
import { maxWordLength } from 'components/cardElements/grid';
import { forceBreakLongWord } from 'common/utility';

export interface FileCaptionCellProps {
  cell: ITableCellViewModel;
  file: FileViewModel;
}

export class FileCaptionCell extends React.Component<FileCaptionCellProps> {
  render(): JSX.Element {
    const { cell, file } = this.props;
    const content =
      typeof cell.convertedValue === 'string'
        ? forceBreakLongWord(cell.convertedValue, maxWordLength)
        : cell.convertedValue;
    return (
      <span style={{ display: 'inline-flex' }}>
        <Observer>
          {() =>
            file.isLoading ? (
              <span className="files-control-item-loading">
                <i className="icon expand-icon fa fa-spinner fa-spin" />
              </span>
            ) : (
              <></>
            )
          }
        </Observer>
        {content}
        <FileListTags
          style={{
            marginLeft: '6px',
            fontSize: '1rem'
          }}
          viewModel={file}
        />
      </span>
    );
  }
}
