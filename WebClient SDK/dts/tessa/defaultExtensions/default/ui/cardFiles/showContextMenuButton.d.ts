import * as React from 'react';
import { ShowContextMenuButtonViewModel } from './showContextMenuButtonViewModel';
export interface ShowContextMenuButtonProps {
    viewModel: ShowContextMenuButtonViewModel;
}
export interface ShowContextMenuButtonState {
    isDropDownOpen: boolean;
}
export declare class ShowContextMenuButton extends React.Component<ShowContextMenuButtonProps, ShowContextMenuButtonState> {
    constructor(props: ShowContextMenuButtonProps);
    private _dropDownRef;
    render(): JSX.Element;
    private getActions;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
