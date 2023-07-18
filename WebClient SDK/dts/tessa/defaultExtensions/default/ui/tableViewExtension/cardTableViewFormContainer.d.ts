import React from 'react';
import { CardTableViewControlViewModel } from './cardTableViewControlViewModel';
import { ICardModel } from 'tessa/ui/cards';
import { BaseViewControlItem, IGridFormContainer } from 'tessa/ui/cards/controls';
import { Visibility } from 'tessa/platform';
export declare class CardTableViewFormContainerViewModel extends BaseViewControlItem implements IGridFormContainer {
    constructor(viewComponent: CardTableViewControlViewModel);
    private _isRowFormOpened;
    get cardModel(): ICardModel;
    get isRowFormOpened(): boolean;
    get visibility(): Visibility;
    openForm(): void;
    closeForm(): void;
}
export interface CardTableViewFormContainerProps {
    viewModel: CardTableViewFormContainerViewModel;
}
export declare class CardTableViewFormContainer extends React.Component<CardTableViewFormContainerProps> {
    render(): JSX.Element;
}
