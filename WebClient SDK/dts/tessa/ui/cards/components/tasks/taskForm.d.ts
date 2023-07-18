import * as React from 'react';
import { TaskViewModel } from 'tessa/ui/cards/tasks';
export interface TaskFormProps {
    viewModel: TaskViewModel;
}
export declare class TaskForm extends React.Component<TaskFormProps> {
    render(): JSX.Element;
}
