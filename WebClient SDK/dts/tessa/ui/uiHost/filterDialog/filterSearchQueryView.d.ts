import React from 'react';
import { FilterSearchQueryViewModel } from './filterSearchQueryViewModel';
export interface FilterSearchQueryViewProps {
    viewModel: FilterSearchQueryViewModel;
}
export declare class FilterSearchQueryView extends React.Component<FilterSearchQueryViewProps> {
    private _leftSideWidth;
    private _treeRef;
    render(): JSX.Element | null;
    private _renderTree;
    private _renderSearchQueries;
    private _handleUserHeaderClick;
    private _handlePublicHeaderClick;
    private _handleCurrentQuery;
    private _handleResize;
}
