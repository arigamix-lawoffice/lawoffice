import * as React from 'react';
import { IQuickSearchViewModel } from '../../content';
export interface QuickSearchViewProps {
    viewModel: IQuickSearchViewModel;
}
export declare class QuickSearchView extends React.Component<QuickSearchViewProps> {
    private readonly _mainRef;
    constructor(props: QuickSearchViewProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: QuickSearchViewProps): void;
    render(): JSX.Element | null;
    focus(opt?: FocusOptions): void;
    blur(): void;
    private onBlur;
    private onFocus;
    private handleSearch;
    private handleFocusControlWhenDataWasLoaded;
}
