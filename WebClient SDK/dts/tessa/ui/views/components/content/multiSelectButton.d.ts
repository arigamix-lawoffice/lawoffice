import * as React from 'react';
import { MultiSelectButtonViewModel } from '../../content/multiSelectButtonViewModel';
export interface MultiSelectButtonProps {
    viewModel: MultiSelectButtonViewModel;
}
export declare class MultiSelectButton extends React.Component<MultiSelectButtonProps> {
    render(): JSX.Element | null;
    private handleClick;
}
