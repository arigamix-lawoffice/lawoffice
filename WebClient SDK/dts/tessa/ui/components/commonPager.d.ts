import * as React from 'react';
export interface CommonPagerThemeProps {
    pagingBorder: string;
    pagingBackground: string;
    pagingForeground: string;
    pagingWatermark: string;
}
export interface CommonPagerProps {
    disabled?: boolean;
    inputDisabled?: boolean;
    className?: string;
    isPagingEnabled?: boolean;
    firstPageButtonEnabled?: boolean;
    lastPageButtonEnabled?: boolean;
    pagingButtonClassName?: string;
    firstPageButtonIcon?: string;
    lastPageButtonIcon?: string;
    prevPagingButtonIcon?: string;
    nextPagingButtonIcon?: string;
    hasPreviousPage?: boolean;
    hasNextPage?: boolean;
    currentPage: number;
    pageCount: number;
    isPageCountVisible?: boolean;
    handleCurrentPageChanged?: (page: number) => void;
    isOptionalPagingEnabled?: boolean;
    optionalPagingStatus?: boolean;
    optionalPagingStatusClassName?: string;
    optionalPagingStatusColor?: string;
    optionalPagingStatusIcon?: string;
    handleOptionalPagingStatusChanged?: () => void;
    activePageToolTip?: string;
    nextPageToolTip?: string;
    previousPageToolTip?: string;
    switcherPagingModeToolTip?: string;
    firstPageButtonToolTip?: string;
    lastPageButtonToolTip?: string;
    theme?: CommonPagerThemeProps;
}
export declare class CommonPager extends React.Component<CommonPagerProps> {
    static defaultProps: CommonPagerProps;
    render(): JSX.Element;
    private renderOptionalPagingButton;
    private renderPaging;
    private handleOptionalPagingClick;
    private handleFirstPageClick;
    private handleLastPageClick;
    private handlePrevPageClick;
    private handleNextPageClick;
    private handleCurrentPageChanged;
}
