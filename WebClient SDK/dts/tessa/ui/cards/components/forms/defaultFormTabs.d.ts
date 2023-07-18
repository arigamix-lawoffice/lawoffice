import * as React from 'react';
import { IFormViewModelBase } from 'tessa/ui/cards/interfaces';
export interface DefaultFormTabsProps {
    viewModel: IFormViewModelBase;
}
export interface DefaultFormTabsState {
    isDropDownOpen: boolean;
}
export declare class DefaultFormTabs extends React.Component<DefaultFormTabsProps, DefaultFormTabsState> {
    constructor(props: DefaultFormTabsProps);
    private _tabControlRef;
    private _dropDownRef;
    render(): JSX.Element | null;
    private renderTabHeaders;
    private renderDropDown;
    private renderTabContents;
    private scrollIntoView;
    private createTabClickHandler;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
