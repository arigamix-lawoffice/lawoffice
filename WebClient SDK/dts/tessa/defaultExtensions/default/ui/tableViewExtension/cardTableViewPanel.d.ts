import React from 'react';
import { CardTableViewControlViewModel } from './cardTableViewControlViewModel';
import { UIButton } from 'tessa/ui';
import { BaseViewControlItem, ViewControlPagingViewModel } from 'tessa/ui/cards/controls';
export declare class CardTableViewPanelViewModel extends BaseViewControlItem {
    constructor(viewComponent: CardTableViewControlViewModel);
    leftButtons: UIButton[];
    rightButtons: UIButton[];
    paging: ViewControlPagingViewModel;
}
export interface ICardTableViewPanelProps {
    viewModel: CardTableViewPanelViewModel;
}
interface ICardTableViewPanelState {
    expandSearch: boolean;
}
export declare class CardTableViewPanel extends React.Component<ICardTableViewPanelProps, ICardTableViewPanelState> {
    constructor(props: ICardTableViewPanelProps);
    render(): JSX.Element;
    private handleFocusChange;
}
export {};
