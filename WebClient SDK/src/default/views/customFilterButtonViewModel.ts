import { ContentPlaceArea, ContentPlaceOrder, FilterButtonViewModel } from 'tessa/ui/views/content';

import { IWorkplaceViewComponent } from 'tessa/ui/views';

/**
 * Модель представления, позволяющая переопределить действие, выполняемое при нажатии на кнопку открытия диалога с параметрами фильтрации представления.
 */
export class CustomFilterButtonViewModel extends FilterButtonViewModel {
  //#region ctor

  constructor(
    openFilterCommand: (viewComponent: IWorkplaceViewComponent) => Promise<void>,
    viewComponent: IWorkplaceViewComponent,
    area: ContentPlaceArea = ContentPlaceArea.ToolBarPanel,
    order: number = ContentPlaceOrder.BeforeAll
  ) {
    super(viewComponent, area, order);
    this._openFilterCommand = openFilterCommand;
  }

  //#endregion

  //#region fields

  private _openFilterCommand: (viewComponent: IWorkplaceViewComponent) => Promise<void>;

  //#endregion

  //#region base overrides

  async openFilter(): Promise<void> {
    await this._openFilterCommand(this.viewComponent);
  }

  //#endregion
}
