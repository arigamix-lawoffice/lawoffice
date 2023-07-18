import { computed } from 'mobx';
import { Card, CardRow } from 'tessa/cards';
import { createTypedField, DotNetType } from 'tessa/platform';
import { GridColumnInfo } from 'tessa/ui/cards/controls';

// tslint:disable: no-any

export class CardTableViewRowData implements ReadonlyMap<string, any> {
  //#region ctor

  constructor(
    readonly card: Card,
    readonly cardRow: CardRow,
    readonly columnInfos: Map<string, GridColumnInfo>,
    readonly orderColumnName: string | null,
    readonly flagColumnName: string,
    readonly sectionName: string
  ) {}

  //#endregion

  //#region props

  @computed
  get rowId(): guid {
    return this.cardRow.rowId;
  }

  @computed
  get order(): number {
    return this.orderColumnName ? this.cardRow.get(this.orderColumnName) : 0;
  }

  @computed
  get flag(): boolean {
    return this.flagColumnName ? !!this.cardRow.get(this.flagColumnName) : false;
  }
  set flag(value: boolean) {
    if (this.flagColumnName) {
      this.cardRow.set(this.flagColumnName, createTypedField(value, DotNetType.Boolean));
    }
  }

  //#endregion

  //#region Map members

  public readonly [Symbol.toStringTag]: 'Map';

  public forEach(
    callbackfn: (value: any, key: string, map: ReadonlyMap<string, any>) => void,
    thisArg?: any
  ) {
    this.columnInfos.forEach(callbackfn, thisArg);
  }

  public get(key: string): any | undefined {
    if (key === 'RowID') {
      return this.rowId;
    }

    if (this.orderColumnName && key === this.orderColumnName) {
      return this.order;
    }

    const columnInfo = this.columnInfos.get(key);
    if (columnInfo) {
      return columnInfo.getValue(this.cardRow, this.card).value;
    }
    return undefined;
  }

  public has(key: string): boolean {
    return this.columnInfos.has(key);
  }

  public get size(): number {
    return this.columnInfos.size;
  }

  public *[Symbol.iterator](): IterableIterator<[string, any]> {
    for (let i of this.columnInfos) {
      yield [i[0], i[1].getValue(this.cardRow, this.card).value];
    }
  }

  public *entries(): IterableIterator<[string, any]> {
    for (let i of this.columnInfos.entries()) {
      yield [i[0], i[1].getValue(this.cardRow, this.card).value];
    }
  }

  public *keys(): IterableIterator<string> {
    for (let i of this.columnInfos.keys()) {
      yield i;
    }
  }

  public *values(): IterableIterator<any> {
    for (let i of this.columnInfos.values()) {
      yield i.getValue(this.cardRow, this.card).value;
    }
  }

  //#endregion
}
