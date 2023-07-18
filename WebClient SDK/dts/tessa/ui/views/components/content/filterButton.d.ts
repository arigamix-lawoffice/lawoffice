import * as React from 'react';
import { IFilterButtonViewModel } from '../../content/filterButtonViewModel';
export interface FilterButtonProps {
    viewModel: IFilterButtonViewModel;
}
export declare class FilterButton extends React.Component<FilterButtonProps> {
    render(): JSX.Element | null;
    private handleClick;
}
