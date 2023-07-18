import React from 'react';
import { IFilteringBlockViewModel } from 'tessa/ui/views/content/filteringBlockViewModel';
export interface ViewFilteringBlockProps {
    viewModel: IFilteringBlockViewModel;
}
export declare class ViewFilteringBlock extends React.Component<ViewFilteringBlockProps> {
    render(): JSX.Element | null;
    private renderFilterButton;
    private renderParameterBalloons;
    private removeParamHandler;
    private openFilterHandler;
}
