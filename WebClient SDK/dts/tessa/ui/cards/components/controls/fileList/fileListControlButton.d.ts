import * as React from 'react';
import { FileListViewModel } from 'tessa/ui/cards/controls';
export interface FileListControlButtonProps {
    viewModel: FileListViewModel;
}
export interface FileListControlButtonState {
    isDropDownOpen: boolean;
}
export declare class FileListControlButton extends React.Component<FileListControlButtonProps, FileListControlButtonState> {
    constructor(props: FileListControlButtonProps);
    private _dropDownRef;
    render(): JSX.Element;
    private getActions;
    private handleDropDownRef;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
