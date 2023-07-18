import * as React from 'react';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms/defaultFormTabWithTasksViewModel';
export interface TaskEditorProps {
    viewModel: DefaultFormTabWithTasksViewModel;
}
export declare class TaskEditor extends React.Component<TaskEditorProps> {
    render(): JSX.Element;
    private changePostponedTasksVisibilityHandler;
    private changeAuthorTasksVisibilityHandler;
}
