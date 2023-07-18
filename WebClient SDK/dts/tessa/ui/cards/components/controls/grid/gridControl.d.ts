import * as React from 'react';
import { ControlProps } from '../controlProps';
import { IControlViewModel } from 'tessa/ui/cards/interfaces';
import { IGridViewBag } from 'components/cardElements/grid';
export interface IGridControlViewBag extends IGridViewBag {
    searchText?: string;
}
export declare class GridControl extends React.Component<ControlProps<IControlViewModel>> {
    private _mainRef;
    constructor(props: ControlProps<IControlViewModel>);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<IControlViewModel>): void;
    private _currentSearch;
    private _currentPage;
    private get rows();
    private get columns();
    get isEnabled(): boolean;
    get rowsPerPage(): number;
    get pages(): number;
    get currentPage(): number;
    movePage: (isForward: boolean) => void;
    setPage: (currentPage: number | undefined) => void;
    render(): JSX.Element | null;
    focus(opt?: FocusOptions): void;
    setCurrentPage: (value: number | undefined) => void;
    private handleKeyDown;
    private handleFocus;
    private handleBlur;
    private handleHeaderDrop;
}
