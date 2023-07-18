import * as React from 'react';
import { BaseContentItem, ContentPlaceArea, ContentPlaceOrder } from 'tessa/ui/views/content';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { LocalizationManager } from 'tessa/localization/localizationManager';
import { useMemo } from 'react';

export class FullSizeTextBlockViewModel extends BaseContentItem {
  //#region ctor

  constructor(viewComponent: IWorkplaceViewComponent, text: string) {
    super(viewComponent, ContentPlaceArea.ContentPanel, ContentPlaceOrder.BeforeAll);
    this.text = text;
    this.setParentStyle(viewComponent);
  }

  //#endregion

  //#region methods

  private setParentStyle(viewComponent: IWorkplaceViewComponent): void {
    viewComponent.calculateMaxHeight = true;
  }

  //#endregion

  //#region props

  public text: string;

  //#endregion
}

interface IFullSizeTextBlockProps {
  viewModel: FullSizeTextBlockViewModel;
}

export function FullSizeTextBlockView(props: IFullSizeTextBlockProps): JSX.Element {
  const { viewModel: viewModel } = props;
  const text = useMemo(
    () => LocalizationManager.instance.localize(viewModel.text),
    [viewModel.text]
  );
  return <div className="full-size-text-block">{text}</div>;
}
