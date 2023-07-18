import * as React from 'react';
import { IFormViewModelBase } from 'tessa/ui/cards/interfaces';
export interface DefaultFormMainProps {
    viewModel: IFormViewModelBase;
}
export interface DefaultFormMainState {
    mode: 'mono' | 'spread';
    isPreviewMenuOpen: boolean;
}
export declare class DefaultFormMain extends React.Component<DefaultFormMainProps, DefaultFormMainState> {
    constructor(props: DefaultFormMainProps);
    private _tabScrollInViewDispose;
    private _mouseClicked;
    private _treeWidth;
    private _bodyRef;
    private _previewRef;
    private _resizerRef;
    private _previewButtonRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private getAutohideOrder;
    private getToolbarProps;
    private handleContextMenu;
    private isTabSelected;
    private renderTabContent;
    private renderDropDown;
    private renderPreviewButtons;
    private createTabClickHandler;
    private handlePreviewMenuToggle;
    private handleMouseUp;
    private handleMouseDown;
    private handleMouseMove;
}
