import * as React from 'react';
import { WorkspaceModel } from 'tessa/ui/workspaceModel';
import { TabPanelButton } from './tabPanelButton';
export interface TabPanelProps {
    isSelectionMode?: boolean;
}
export declare class TabPanel extends React.Component<TabPanelProps> {
    render(): JSX.Element;
    private handleActivateWorkspace;
}
export declare enum TabDropDownType {
    Default = 1,
    Mobile = 2
}
export interface TabPanelInternalProps {
    tabPanelId: guid;
    workspaces: ReadonlyArray<WorkspaceModel>;
    viewWorkspaceCount: number;
    currentWorkspace: WorkspaceModel | null;
    isSelectionMode: boolean;
    buttons: TabPanelButton[];
    onActivateWorkspace: (model: WorkspaceModel) => void;
}
export interface TabPanelInternalState {
    isTabsDropDownOpen: TabDropDownType | null;
    tabsDropDownButtonView: string | null;
    showTabsButton: boolean;
}
export declare class TabPanelInternal extends React.Component<TabPanelInternalProps, TabPanelInternalState> {
    constructor(props: TabPanelInternalProps);
    private tabsDropdownButtonRef;
    private tabsDropdownElementRef;
    private _tabsContainerRef;
    render(): JSX.Element;
    private renderTabPanelButtons;
    private renderTabsDropDownButton;
    private renderTabsDropDown;
    private renderViewTreeButton;
    private handleDrop;
    private handleScrollUpdate;
    private handleOpenTabsDropDown;
    private handleMobileOpenTabsDropDown;
    private handleCloseTabsDropDown;
    private handleTabsDropDownButtonView;
    private handleTabOpen;
    private handleActivateWorkspace;
    private handleCloseWorkspace;
    private handleTreeViewButtonClick;
    private handleTabPanelButtonClick;
    private handleGetMenuActions;
}
