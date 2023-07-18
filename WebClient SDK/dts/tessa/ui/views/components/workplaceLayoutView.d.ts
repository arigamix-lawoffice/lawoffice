import * as React from 'react';
import { WorkplaceLayoutViewModel } from '../workplaceLayoutViewModel';
import { WorkplaceLayoutContentCollection } from '../workplaceLayoutContentCollection';
export interface WorkplaceLayoutViewProps {
    viewModel: WorkplaceLayoutViewModel;
}
export declare class WorkplaceLayoutView extends React.Component<WorkplaceLayoutViewProps> {
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private handleCurrentContextClick;
}
export interface WorkplaceLayoutContentCollectionViewProps {
    viewModel: WorkplaceLayoutContentCollection;
}
export interface WorkplaceLayoutContentCollectionViewState {
    isDropDownOpen: boolean;
}
export declare class WorkplaceLayoutContentCollectionView extends React.Component<WorkplaceLayoutContentCollectionViewProps, WorkplaceLayoutContentCollectionViewState> {
    constructor(props: WorkplaceLayoutContentCollectionViewProps);
    private _tabControlRef;
    private _dropDownRef;
    render(): JSX.Element;
    private renderTabHeaders;
    private renderTabContent;
    private renderDropDown;
    private scrollIntoView;
    private createTabClickHandler;
    private handleTabControlRef;
    private handleDropDownRef;
    private handleOpenDropDown;
    private handleCloseDropDown;
}
