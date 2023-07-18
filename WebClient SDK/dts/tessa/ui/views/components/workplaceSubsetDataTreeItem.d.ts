import * as React from 'react';
import { ISubsetDataTreeItem } from '../workplaces/tree';
export interface WorkplaceSubsetDataTreeItemProps {
    item: ISubsetDataTreeItem;
    level: number;
}
export declare class WorkplaceSubsetDataTreeItem extends React.Component<WorkplaceSubsetDataTreeItemProps> {
    render(): JSX.Element;
    private clickWrapper;
    private handleOnSubsetDataClick;
    private handleOnSubsetDataDoubleClick;
    private handleOnSubsetDataContextMenu;
    private handleIndentPlusMinusClick;
}
