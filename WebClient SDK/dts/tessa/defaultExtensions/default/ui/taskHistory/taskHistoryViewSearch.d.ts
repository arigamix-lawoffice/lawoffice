import React from 'react';
import { TaskHistoryViewSearchViewModel } from './taskHistoryViewSearchViewModel';
export interface ITaskHistoryViewSearchProps {
    viewModel: TaskHistoryViewSearchViewModel;
}
interface ITaskHistoryViewSearchState {
    expandSearch: boolean;
}
export declare class TaskHistoryViewSearch extends React.Component<ITaskHistoryViewSearchProps, ITaskHistoryViewSearchState> {
    constructor(props: ITaskHistoryViewSearchProps);
    render(): JSX.Element;
    private handleFocusChange;
}
export {};
