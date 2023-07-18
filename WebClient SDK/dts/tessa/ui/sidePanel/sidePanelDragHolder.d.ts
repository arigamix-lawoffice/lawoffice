import * as React from 'react';
import { ISidePanelHolder } from './sidePanelHelper';
import { ElementSide, InteractionEventTypes } from './common';
export interface ISidePanelDragHolderProps {
    side: ElementSide.ELEMENT_LEFT_SIDE_MENU | ElementSide.ELEMENT_RIGHT_SIDE_MENU;
    isPanelOpen: boolean;
    changePanelState: (state: string, side: ElementSide, eventType: InteractionEventTypes) => void;
    isOnClickOnly?: boolean;
}
declare class SidePanelDragHolder extends React.Component<ISidePanelDragHolderProps> implements ISidePanelHolder {
    constructor(props: ISidePanelDragHolderProps);
    private holderWidth;
    private panelWidth;
    private startX;
    private lastX;
    private offsetX;
    private openAmountValue;
    private isDragging;
    private openDirection;
    private closeDirection;
    private dragHolderGesture;
    private releaseHolderGesture;
    private dragPanelGesture;
    private releasePanelGesture;
    private isInit;
    private panelRef;
    private readonly dragHolderRef;
    componentWillUnmount(): void;
    shouldComponentUpdate(nextProps: ISidePanelDragHolderProps): boolean;
    render(): JSX.Element | null;
    enableOrDisableIfNeeded(isOnClickOnly: boolean, panelRef: HTMLDivElement): void;
    init(panelRef: HTMLDivElement): void;
    changePanelState(state: string, withTimeout?: boolean): void;
    private release;
    private openPercentage;
    private openAmount;
    private snapToRest;
    private getOpenRatio;
    private getOffset;
    private getOpenAmount;
    private onDrag;
    private onEndDrag;
    private handleClick;
}
export default SidePanelDragHolder;
