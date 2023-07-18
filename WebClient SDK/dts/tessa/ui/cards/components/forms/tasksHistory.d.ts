import * as React from 'react';
import { DefaultFormTabWithTaskHistoryViewModel } from 'tessa/ui/cards/forms/defaultFormTabWithTaskHistoryViewModel';
import { ColumnSortDirection } from 'tessa/ui/cards/controls/columnSortDirection';
import { IGridBlockViewModel } from 'components/cardElements/grid';
export interface ITaskHistoryViewBag {
    searchText?: string;
}
export interface ITasksHistoryProps {
    viewModel: DefaultFormTabWithTaskHistoryViewModel;
}
interface ITaskHistoryState {
    expandSearch: boolean;
}
export declare class TasksHistory extends React.Component<ITasksHistoryProps, ITaskHistoryState> {
    constructor(props: ITasksHistoryProps);
    rootBlock: IGridBlockViewModel;
    wfResolutionRootBlock: IGridBlockViewModel;
    otherRootBlock: IGridBlockViewModel;
    _sortingColumn: string | undefined;
    _sortDirection: ColumnSortDirection;
    render(): JSX.Element;
    setSortingColumn(id: string, descendingByDefault?: boolean): void;
    private get columns();
    private get rows();
    private get blocks();
    private get rawRows();
    private getSortedRows;
    private searchFilterFunc;
    private handleKeyDown;
    private handleHeaderDrop;
    private handleFocusChange;
}
export {};
