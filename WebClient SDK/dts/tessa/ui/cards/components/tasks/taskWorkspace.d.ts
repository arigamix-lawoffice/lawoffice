import * as React from 'react';
import { TaskWorkspaceViewModel } from 'tessa/ui/cards/tasks';
export interface TaskWorkspaceProps {
    viewModel: TaskWorkspaceViewModel;
}
export interface TaskWorkspaceState {
    dropDownIsOpen: boolean;
}
export declare class TaskWorkspace extends React.Component<TaskWorkspaceProps, TaskWorkspaceState> {
    constructor(props: TaskWorkspaceProps);
    private _dropDownRef;
    render(): JSX.Element;
    private renderTaskData;
    private renderTaskAdditionalData;
    private renderTaskActions;
    private renderTaskAdditionalActions;
    private renderAdditionalActionsDropDown;
    private additionalActionsDropDownOpenHandler;
    private additionalActionsDropDownCloseHandler;
    private actionCommandHandler;
    private additionalActionsDropDownTheme;
}
