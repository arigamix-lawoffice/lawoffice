import * as React from 'react';
import { ITreeItem } from '../workplaces/tree';
export interface WorkplaceTreeItemProps {
    item: ITreeItem;
    level: number;
}
export declare class WorkplaceTreeItem extends React.Component<WorkplaceTreeItemProps> {
    constructor(props: WorkplaceTreeItemProps);
    private _renderFunc;
    render(): React.ReactNode;
    private renderFolder;
    private renderView;
    private renderSearchQuery;
    private renderSubset;
    private renderSubsetData;
}
