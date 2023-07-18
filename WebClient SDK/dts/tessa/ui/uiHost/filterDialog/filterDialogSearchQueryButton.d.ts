import * as React from 'react';
import { FilterSearchQueryViewModel } from './filterSearchQueryViewModel';
export interface FilterDialogSearchQueryButtonProps {
    viewModel: FilterSearchQueryViewModel;
}
export interface FilterDialogSearchQueryButtonState {
    isDropDownOpen: boolean;
}
export declare class FilterDialogSearchQueryButton extends React.Component<FilterDialogSearchQueryButtonProps, FilterDialogSearchQueryButtonState> {
    constructor(props: FilterDialogSearchQueryButtonProps);
    private _dropdownButtonRef;
    render(): JSX.Element | null;
    private renderDropDownTree;
    private renderDropDownSearchQueries;
    private renderDropDown;
    private handleCurrentQuery;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
