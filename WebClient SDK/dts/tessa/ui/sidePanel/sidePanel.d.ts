import * as React from 'react';
import { ITile, ITilePanel } from 'tessa/ui/tiles';
import { ElementSide } from './common';
export interface SidePanelProps {
    isPanelOpen: boolean;
    side: ElementSide.ELEMENT_LEFT_SIDE_MENU | ElementSide.ELEMENT_RIGHT_SIDE_MENU;
}
export default class SidePanel extends React.Component<SidePanelProps> {
    readonly internalRef: React.RefObject<SidePanelInternal>;
    render(): JSX.Element;
    private getTilePanel;
    private getTiles;
}
interface SidePanelInternalProps {
    panel: ITilePanel | null;
    tiles: ITile[];
    currentTile: ITile | null;
    isPanelOpen: boolean;
    side: ElementSide;
}
declare class SidePanelInternal extends React.Component<SidePanelInternalProps> {
    private readonly sidePanelContainer;
    private readonly scrollUpButton;
    private readonly scrollDownButton;
    readonly menuRef: React.RefObject<HTMLDivElement>;
    componentDidMount(): void;
    componentWillUnmount(): void;
    shouldComponentUpdate(nextProps: SidePanelInternalProps): boolean;
    componentDidUpdate(prevProps: SidePanelInternalProps): void;
    render(): JSX.Element;
    private renderTiles;
    private scrollDown;
    private scrollUp;
    private scrollListenerConditions;
    private visibilityButtonConditions;
    private blockClick;
    private handleScrollerRecalc;
    private handleGroupTileAction;
}
export {};
