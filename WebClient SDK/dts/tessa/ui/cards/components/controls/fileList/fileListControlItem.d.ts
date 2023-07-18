import * as React from 'react';
import { FileViewModel, FileListViewModel } from 'tessa/ui/cards/controls';
export interface FileListControlItemProps {
    viewModel: FileViewModel;
    control: FileListViewModel;
}
export interface FileListControlItemState {
    isDropDownOpen: boolean;
}
export declare class FileListControlItem extends React.Component<FileListControlItemProps, FileListControlItemState> {
    constructor(props: FileListControlItemProps);
    private _dropDownRef;
    private _selectorRef;
    render(): JSX.Element;
    private renderLoadingIcon;
    private renderDropDown;
    private renderSelection;
    private handleDropDownRef;
    private handleClick;
    private handleClose;
    private handleDrop;
    private handleOnContextMenu;
    private handleSelectionChange;
}
