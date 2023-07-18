import * as React from 'react';
import { TaskHistoryItemInfo } from '../../tasks';
export interface TaskHistoryDetailsDialogProps {
    viewModel: TaskHistoryItemInfo;
    onClose: () => void;
}
export declare class TaskHistoryDetailsDialog extends React.Component<TaskHistoryDetailsDialogProps> {
    render(): JSX.Element;
    private renderDetails;
    private localize;
    private handleClose;
    private handleMiddleClick;
}
