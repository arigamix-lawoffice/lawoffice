/* eslint-disable @typescript-eslint/no-explicit-any */
import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';
import { runInAction } from 'mobx';
import { SchemeDbType } from 'tessa/platform';
import { ViewControlViewComponent } from 'tessa/ui/cards/controls';
import { ISelectionState, SelectionState } from 'tessa/ui/views';
import { Paging } from 'tessa/views';
import { ViewSelectionMode } from 'tessa/views/metadata';

export class CardTableViewComponent extends ViewControlViewComponent {
  //#region fields

  private _savedSelectionState: ISelectionState | null = null;

  //#endregion

  //#region methods

  initColumns(columns: ReadonlyMap<string, SchemeDbType>): void {
    runInAction(() => {
      this._columns = columns;
    });
  }

  protected resetData(): void {
    this._savedSelectionState = this._selection.clone();
    super.resetData();
  }

  protected updateData(
    columns: ReadonlyMap<string, SchemeDbType>,
    rows: ReadonlyArray<ReadonlyMap<string, any>>,
    rowCount: number,
    tags: Map<guid, IGridRowTagViewModel[]>,
    selectionState: ISelectionState | null = null
  ): void {
    const inPagingMode =
      this.pagingMode === Paging.Always ||
      (this.pagingMode === Paging.Optional && this.optionalPagingStatus);
    if (inPagingMode) {
      // если на текущей странице нет строк (строк мало), то листаем назад до первой страницы, на которой есть хоть одна строка
      if (this.currentPage > 1 && this.pageLimit * (this.currentPage - 1) >= rows.length) {
        this.currentPage = Math.max(Math.ceil(rows.length / this.pageLimit), 1);
      }

      rows = rows.slice(this.pageLimit * (this.currentPage - 1));
    }

    const hasNextPage = rows.length > this.pageLimit;
    if (inPagingMode && hasNextPage) {
      rows = rows.slice(0, this.pageLimit);
    }

    if (
      !selectionState &&
      this._savedSelectionState &&
      this._savedSelectionState.selectedRows &&
      this._savedSelectionState.selectedRows.length > 0 &&
      this._savedSelectionState.selectedRows.every(row => rows.includes(row))
    ) {
      selectionState = this._savedSelectionState;
    }

    runInAction(() => {
      this._hasNextPage = hasNextPage;
      this._columns = columns;
      this._data = rows;
      this._calculatedRowCount = rowCount;
      this._tags = tags;
    });

    this.updatePageCount();

    if (!selectionState) {
      const selectedRow = this.firstRowSelection ? rows[0] : null;
      const selectedRows = selectedRow ? [selectedRow] : [];
      const selectedColumn =
        selectedRow && this.selectionMode === ViewSelectionMode.Cell
          ? this.tryGetFirstVisibleColumnName(this._columns)
          : '';
      selectionState = new SelectionState(
        this.multiSelect,
        this.selectionMode,
        selectedColumn,
        selectedRow,
        selectedRows
      );
    }

    this._selection.assign(selectionState);

    this._savedSelectionState = null;
  }

  //#endregion
}
