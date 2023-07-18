import * as React from 'react';
import { SelectViewRowsViewModel } from '../../content/selectViewRowsViewModel';
export interface SelectViewRowsButtonProps {
    viewModel: SelectViewRowsViewModel;
}
export declare class SelectViewRowsButton extends React.Component<SelectViewRowsButtonProps> {
    render(): JSX.Element | null;
    private handleClick;
}
