import * as React from 'react';
interface IWorkplaceProps {
    windowElement: HTMLElement;
}
interface IWorkplaceState {
    width: number;
    height: number;
}
export declare class Workplace extends React.Component<IWorkplaceProps, IWorkplaceState> {
    private _workspace;
    private _mouseClicked;
    private _treeWidth;
    private _treeRef;
    private _resizerRef;
    private _viewRef;
    private _cachedId;
    private _tabPanelHeight;
    constructor(props: IWorkplaceProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element | null;
    private handleMouseUp;
    private handleMouseDown;
    private handleMouseMove;
    private handleOnContextMenu;
    private handleResize;
}
export {};
