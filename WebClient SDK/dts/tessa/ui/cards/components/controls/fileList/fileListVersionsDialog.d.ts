import * as React from 'react';
import { FileListVersionsDialogViewModel } from 'tessa/ui/cards/controls';
import { IFileVersion } from 'tessa/files';
export interface FileListVersionsDialogProps {
    viewModel: FileListVersionsDialogViewModel;
    onClose: () => void;
}
export interface FileListVersionsDialogState {
    sourceSelectedVersion: IFileVersion | null;
    selectedVersions: IFileVersion[];
}
export declare class FileListVersionsDialog extends React.Component<FileListVersionsDialogProps, FileListVersionsDialogState> {
    constructor(props: FileListVersionsDialogProps);
    private _dropDownRef;
    private _columns;
    private _rows;
    private _reactionDisposer;
    private _selectedRows;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private renderDropDown;
    private initRows;
    private handleRowClick;
    private getContextMenu;
    private handleDropDownRef;
    private handleClickDropDown;
    private handleCloseDropDown;
    private handleCloseForm;
}
