import * as React from 'react';
import { WorkspaceModel } from 'tessa/ui/workspaceModel';
import { MenuAction } from 'tessa/ui/menuAction';
export interface TabPanelElementProps {
    workspace: WorkspaceModel;
    onActivateWorkspace: (model: WorkspaceModel) => void;
    onCloseWorkspace: (model: WorkspaceModel, altKey: boolean) => void;
    isActive?: boolean;
    isFirst?: boolean;
    isSingle?: boolean;
    isCloseable?: boolean;
    isDragging?: boolean;
    onOpenTabsDropDown?: () => void;
    getMenuActions?: (workspace: WorkspaceModel) => MenuAction[];
    onDrop: (sourceWorkplace: WorkspaceModel, targetWorkplace: WorkspaceModel) => void;
}
export declare class TabPanelElement extends React.Component<TabPanelElementProps> {
    private liRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
    static dragStartWorkplace: WorkspaceModel | undefined;
    handleDragStart: (workplace: WorkspaceModel) => (e: React.DragEvent<HTMLDivElement>) => void;
    handleDragOver: (e: React.DragEvent<HTMLDivElement>) => boolean;
    handleDrop: (workplace: WorkspaceModel) => (e: React.DragEvent<HTMLDivElement>) => void;
    handleDragEnd: (e: React.DragEvent<HTMLElement>) => void;
    private getCardTab;
    private getTabCloseButton;
    private getWorkspaceTab;
    private close;
    private handleClick;
    private handleContextMenu;
    private preventDefaultIfMiddle;
}
