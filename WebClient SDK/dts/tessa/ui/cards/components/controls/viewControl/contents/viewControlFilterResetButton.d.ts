import * as React from 'react';
import { ViewControlFilterResetButtonViewModel } from 'tessa/ui/cards/controls/viewControl/contents';
export interface ViewControlFilterResetButtonProps {
    viewModel: ViewControlFilterResetButtonViewModel;
}
export declare class ViewControlFilterResetButton extends React.Component<ViewControlFilterResetButtonProps> {
    render(): JSX.Element | null;
    private handleClick;
}
