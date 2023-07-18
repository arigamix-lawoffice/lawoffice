import * as React from 'react';
import { RequestParameter } from 'tessa/views/metadata';
import { IFilterTextViewModel } from '../../content';
export declare const FilterPanel: import("styled-components").StyledComponent<"div", any, {}, never>;
export interface FilterTextProps {
    viewModel: IFilterTextViewModel;
}
export declare class FilterText extends React.Component<FilterTextProps> {
    private _buttonWidthInRem;
    render(): JSX.Element | null;
    static renderParamsAsText(params: RequestParameter[], openFilter: (e: React.SyntheticEvent, params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }) => void): JSX.Element[];
    private static renderItemName;
    private static renderItemValue;
    private static getContentForParameter;
    private renderButtons;
    private handleOpenFilter;
    private handleClearFilter;
}
