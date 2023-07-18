import React from 'react';
import { observable, runInAction } from 'mobx';
import { CardTableViewControlViewModel } from './cardTableViewControlViewModel';
import { ICardModel } from 'tessa/ui/cards';
import { BaseViewControlItem, IGridFormContainer } from 'tessa/ui/cards/controls';
import { GridRowFormDialog } from 'tessa/ui/cards/components/controls';
import { Visibility } from 'tessa/platform';

export class CardTableViewFormContainerViewModel
  extends BaseViewControlItem
  implements IGridFormContainer
{
  //#region ctor

  constructor(viewComponent: CardTableViewControlViewModel) {
    super(viewComponent);
  }

  //#endregion

  //#region fields

  @observable
  private _isRowFormOpened: boolean;

  //#endregion

  //#region props

  get cardModel(): ICardModel {
    return this.viewComponent.cardModel;
  }

  get isRowFormOpened(): boolean {
    return this._isRowFormOpened;
  }

  get visibility(): Visibility {
    return this._isRowFormOpened ? Visibility.Visible : Visibility.Collapsed;
  }

  //#endregion

  //#region methods

  openForm() {
    runInAction(() => (this._isRowFormOpened = true));
  }

  closeForm() {
    runInAction(() => (this._isRowFormOpened = false));
  }

  //#endregion
}

export interface CardTableViewFormContainerProps {
  viewModel: CardTableViewFormContainerViewModel;
}

export class CardTableViewFormContainer extends React.Component<CardTableViewFormContainerProps> {
  render() {
    const { viewModel } = this.props;

    return <GridRowFormDialog viewModel={viewModel} />;
  }
}
