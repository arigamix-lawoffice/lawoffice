import * as React from 'react';
import { SortButtonViewModel } from '../../content/sortButtonViewModel';
export interface SortButtonProps {
    viewModel: SortButtonViewModel;
}
export interface SortButtonState {
    isDropDownOpen: boolean;
}
export declare class SortButton extends React.Component<SortButtonProps, SortButtonState> {
    constructor(props: SortButtonProps);
    private _dropdownButtonRef;
    render(): JSX.Element | null;
    private renderDropDown;
    private renderDropDownItem;
    private handleOpenDropDown;
    private handleCloseDropDown;
    private handleColumnClick;
}
