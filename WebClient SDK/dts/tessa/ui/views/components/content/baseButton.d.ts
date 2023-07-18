import * as React from 'react';
import { IBaseButtonViewModel } from '../../content/baseButtonViewModel';
export interface BaseButtonProps {
    viewModel: IBaseButtonViewModel;
}
export declare class BaseButton extends React.Component<BaseButtonProps> {
    render(): JSX.Element | null;
    private handleClick;
}
