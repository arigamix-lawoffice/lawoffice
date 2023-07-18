import * as React from 'react';
import { ThemeProps } from 'styled-components';
import { IPagingViewModel } from '../../content';
export interface PagingViewProps {
    viewModel: IPagingViewModel;
}
declare class PagingViewInternal extends React.Component<PagingViewProps & ThemeProps<any>> {
    render(): JSX.Element;
    private handleOptionalPagingClick;
    private handleCurrentPageChanged;
}
export declare const PagingView: React.ForwardRefExoticComponent<{
    ref?: React.Ref<PagingViewInternal> | undefined;
    key?: React.Key | null | undefined;
    viewModel: IPagingViewModel;
} & {
    theme?: any;
}>;
export {};
