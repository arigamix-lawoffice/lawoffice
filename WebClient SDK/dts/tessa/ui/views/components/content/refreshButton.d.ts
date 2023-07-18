import * as React from 'react';
import { IRefreshButtonViewModel } from '../../content/refreshButtonViewModel';
export interface RefreshButtonProps {
    viewModel: IRefreshButtonViewModel;
}
export declare class RefreshButton extends React.Component<RefreshButtonProps> {
    render(): JSX.Element;
    private handleClick;
}
