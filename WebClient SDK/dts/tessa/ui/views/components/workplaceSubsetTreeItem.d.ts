import * as React from 'react';
import { ISubsetTreeItem } from '../workplaces/tree';
export interface WorkplaceSubsetTreeItemProps {
    item: ISubsetTreeItem;
    level: number;
}
export declare class WorkplaceSubsetTreeItem extends React.Component<WorkplaceSubsetTreeItemProps> {
    render(): JSX.Element;
    private renderSubsetPanel;
    private clickWrapper;
    private handleOnSubsetClick;
    private handleOnSubsetDoubleClick;
    private handleOnSubsetContextMenu;
    private handleCloseSubsetCommandClick;
    private handleIndentPlusMinusClick;
}
