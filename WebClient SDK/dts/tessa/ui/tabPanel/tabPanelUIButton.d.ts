import * as React from 'react';
import { TabPanelButton } from './tabPanelButton';
export interface TabPanelUIButtonProps {
    button: TabPanelButton;
}
export interface TabPanelUIButtonState {
    dropdownOpen: boolean;
}
export declare class TabPanelUIButton extends React.Component<TabPanelUIButtonProps, TabPanelUIButtonState> {
    constructor(props: TabPanelUIButtonProps);
    private _buttonRef;
    render(): JSX.Element;
    private handleClick;
    private handleDropDownClick;
    private handleCloseDropDown;
    private renderDropDown;
}
