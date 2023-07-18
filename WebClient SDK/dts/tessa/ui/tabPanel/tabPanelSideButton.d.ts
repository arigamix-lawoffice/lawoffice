import * as React from 'react';
import { ElementSide } from '../sidePanel/common';
export declare class TabPanelSideButton extends React.Component<TabPanelSideButtonProps> {
    handleClick: (event: any) => void;
    render(): JSX.Element;
}
export interface TabPanelSideButtonProps {
    side: ElementSide;
}
