import * as React from 'react';
import { CardToolbarItem } from '../../cardToolbarItem';
export interface ToolBarItemProps {
    item: CardToolbarItem;
    bottom?: boolean;
    className?: string;
}
export interface ToolBarItemState {
    isDropDownOpen: boolean;
}
export declare class ToolBarItem extends React.Component<ToolBarItemProps, ToolBarItemState> {
    constructor(props: ToolBarItemProps);
    private _dropDownRef;
    render(): null | JSX.Element;
    private renderDefault;
    private renderGroup;
    private getActions;
    private handleCommand;
    private handleDropDownRef;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
export declare const StyledToolBarItem: import("styled-components").StyledComponent<typeof ToolBarItem, any, ToolBarItemProps, never>;
