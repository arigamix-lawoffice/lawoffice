import * as React from 'react';
import ReactDropZone from 'react-dropzone';
import { FilesViewControlTableGridViewModel } from './filesViewControlTableGridViewModel';
import { TableView } from 'tessa/ui/views/components';
import { FileHelper } from 'src/law/helpers/fileHelper';

export interface FilesViewControlTableViewProps {
  viewModel: FilesViewControlTableGridViewModel;
}

export class FilesViewControlTableView extends React.Component<FilesViewControlTableViewProps> {
  render() {
    const { viewModel } = this.props;

    return (
      <React.Fragment>
        <ReactDropZone
          style={{}}
          activeStyle={{}}
          onDrop={this.handleDrop}
          preventDropOnDocument={false}
          disableClick={true}
          inputProps={{
            style: {
              display: 'none'
            }
          }}
        >
          <TableView viewModel={viewModel} />
        </ReactDropZone>
      </React.Fragment>
    );
  }

  private handleDrop = (files: File[]) => {
    if (files.length > 0) {
      const { viewModel } = this.props;
      const fileControl = viewModel.fileControl;
      FileHelper.addFilesAsync(fileControl, files);
    }
  };
}
