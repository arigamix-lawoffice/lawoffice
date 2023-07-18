import * as React from 'react';
import { IViewTreeItem } from '../workplaces/tree';
export interface WorkplaceViewTreeItemProps {
    item: IViewTreeItem;
    level: number;
}
export declare class WorkplaceViewTreeItem extends React.Component<WorkplaceViewTreeItemProps> {
    render(): JSX.Element;
    private clickWrapper;
    private handleOnViewClick;
    private handleOnViewDoubleClick;
    private handleOnViewContextMenu;
    private handleIndentPlusMinusClick;
}
