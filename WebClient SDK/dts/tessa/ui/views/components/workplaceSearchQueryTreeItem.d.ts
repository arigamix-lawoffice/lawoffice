import * as React from 'react';
import { ISearchQueryTreeItem } from '../workplaces/tree';
export interface WorkplaceSearchQueryTreeItemProps {
    item: ISearchQueryTreeItem;
    level: number;
}
export declare class WorkplaceSearchQueryTreeItem extends React.Component<WorkplaceSearchQueryTreeItemProps> {
    render(): JSX.Element;
    private clickWrapper;
    private handleOnSearchQueryClick;
    private handleOnSearchQueryDoubleClick;
    private handleOnSearchQueryContextMenu;
    private handleIndentPlusMinusClick;
}
