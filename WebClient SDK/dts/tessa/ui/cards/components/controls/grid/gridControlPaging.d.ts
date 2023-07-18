import * as React from 'react';
export declare class GridControlPaging extends React.Component<IGridControlPaging> {
    render(): JSX.Element | null;
}
export interface IGridControlPaging {
    movePage: (isForward: boolean) => void;
    setPage: (page: number | undefined) => void;
    firstPageButtonEnabled: boolean;
    isEnabled: boolean;
    pages: number;
    currentPage: number;
}
