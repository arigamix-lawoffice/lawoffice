import * as React from 'react';
import { TaskTagViewModel } from 'tessa/ui/cards/tasks';
export interface TaskTagProps {
    viewModel: TaskTagViewModel;
}
export declare class TaskTag extends React.Component<TaskTagProps> {
    constructor(props: TaskTagProps);
    render(): JSX.Element;
    private clickHandler;
}
