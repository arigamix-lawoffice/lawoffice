import * as React from 'react';
import { IFolderTreeItem } from '../workplaces/tree';
export interface WorkplaceViewFolderItemProps {
    item: IFolderTreeItem;
    level: number;
}
export declare class WorkplaceFolderTreeItem extends React.Component<WorkplaceViewFolderItemProps> {
    render(): JSX.Element;
    private clickWrapper;
    private handleOnFolderClick;
    private handleOnFolderDoubleClick;
    private handleOnFolderContextMenu;
    private handleIndentPlusMinusClick;
}
