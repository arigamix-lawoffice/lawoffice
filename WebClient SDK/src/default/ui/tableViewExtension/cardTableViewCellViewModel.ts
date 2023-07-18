import { autorun, computed } from 'mobx';
import { CardTableViewRowData } from './cardTableViewRowData';
import { Card, CardRow } from 'tessa/cards';
import { GridColumnInfo } from 'tessa/ui/cards/controls/grid/gridColumnInfo';
import { ITableCellViewModelCreateOptions, TableCellViewModel } from 'tessa/ui/views/content';

export class CardTableViewCellViewModel extends TableCellViewModel {
  //#region ctor

  constructor(args: ITableCellViewModelCreateOptions) {
    super(args);

    const rowData = args.row.data as CardTableViewRowData;
    const columnName = args.column.columnName;

    this.card = rowData.card;
    this.cardRow = rowData.cardRow;
    this.columnInfo = rowData.columnInfos.get(columnName)!;

    this._value = '';
    this._convertedValue = '';
    this._toolTip = '';

    // имитируем computed({ keepAlive: true }) с возможностью dispose
    this._disposes.push(
      autorun(() => {
        this.value;
        this.convertedValue;
        this.toolTip;
      })
    );
  }

  //#endregion

  //#region fields

  private _disposes: Array<() => void> = [];

  //#endregion

  //#region props

  readonly card: Card;

  readonly cardRow: CardRow;

  readonly columnInfo: GridColumnInfo;

  @computed
  public get value(): any {
    const { value } = this.columnInfo.getValue(this.cardRow, this.card);
    return value;
  }

  @computed
  public get convertedValue(): any {
    const { formattedValue } = this.columnInfo.getValue(this.cardRow, this.card);
    return this.columnInfo.clipValue(formattedValue);
  }

  @computed
  public get toolTip(): string {
    const { value } = this.columnInfo.getValue(this.cardRow, this.card);
    const clip = this.columnInfo.isClipValueNeed(value);
    return clip ? value : '';
  }

  //#endregion

  //#region methods

  public dispose(): void {
    super.dispose();

    for (const d of this._disposes) {
      d();
    }
    this._disposes.length = 0;
  }

  //#endregion
}
