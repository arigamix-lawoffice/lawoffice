import * as React from 'react';
import { ControlProps } from './controlProps';
import { IControlViewModel } from 'tessa/ui/cards/interfaces';
export interface TabControlState {
    isDropDownOpen: boolean;
}
export declare class TabControl extends React.Component<ControlProps<IControlViewModel>, TabControlState> {
    constructor(props: ControlProps<IControlViewModel>);
    private _tabControlRef;
    private _dropDownRef;
    render(): JSX.Element | null;
    private renderTabHeaders;
    private renderDropDown;
    private renderTabContents;
    private scrollIntoView;
    private createTabClickHandler;
    private handleTabControlRef;
    private handleDropDownRef;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
