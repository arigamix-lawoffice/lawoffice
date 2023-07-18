import { computed, observable, runInAction } from 'mobx';
import { Visibility } from 'tessa/platform';
import { IFilterableGrid, ViewControlQuickSearchViewModel } from 'tessa/ui/cards/controls';
import {
  ITableBlockViewModel,
  ITableCellViewModel,
  ITableRowViewModel
} from 'tessa/ui/views/content';
import { Highlighter } from 'ui';

export class TaskHistoryViewSearchViewModel
  extends ViewControlQuickSearchViewModel
  implements IFilterableGrid
{
  //#region fields

  @observable
  private _visibility: Visibility = Visibility.Visible;

  private _refreshDispose: Function | null = null;

  // tslint:disable-next-line: no-any
  private _cellsCache: Map<ITableCellViewModel, () => any> = new Map();

  private _rowsCache: Set<ITableRowViewModel> = new Set();
  //#endregion

  //#region props

  @computed
  get visibility(): Visibility {
    return this._visibility;
  }
  set visibility(value: Visibility) {
    this._visibility = value;
  }

  //#endregion

  //#region methods

  initialize() {
    super.initialize();

    this._refreshDispose = this.viewComponent.onRefreshed.addWithDispose(() => {
      runInAction(() => {
        this._searchText = '';
      });
      this._cellsCache.clear();
      this._rowsCache.clear();
    });

    this.expand = false;
  }

  dispose() {
    super.dispose();

    if (this._refreshDispose) {
      this._refreshDispose();
      this._refreshDispose = null;
    }
    this._cellsCache.clear();
    this._rowsCache.clear();
  }

  canSetSearchText() {
    return true;
  }

  setSearchText(text: string) {
    runInAction(() => {
      if (text) {
        text = text.trim();
      }
      this._searchText = text;

      const viewComponent = this.viewComponent;
      const table = viewComponent.table;
      if (!table) {
        return;
      }

      const blocks = table.blocks;
      const rows = table.rows;
      this.restoreAll(blocks, rows);

      if (text) {
        for (const row of rows) {
          const st = this.searchText.toLowerCase();
          let hasHighlightedCell = false;
          for (const cell of row.cells) {
            const value: string = cell.convertedValue;
            const includes = value != null && value.toLowerCase && value.toLowerCase().includes(st);
            if (includes) {
              hasHighlightedCell = true;
              this._cellsCache.set(cell, cell.getContent);
              cell.getContent = () => (
                <Highlighter
                  searchWords={text}
                  textToHighlight={value}
                  caseSensitive={false}
                  highlightClassName="highlighted-text"
                />
              );
            }
          }

          if (hasHighlightedCell) {
            this.processSearchRows(rows, row);
          } else {
            row.visibility = this._rowsCache.has(row);
          }
        }

        if (this._rowsCache.size > 0) {
          for (const block of blocks) {
            block.visibility = !rows.filter(x => x.blockId === block.id).every(x => !x.visibility);
          }
        }
      }
    });
  }

  private restoreAll(
    blocks: ReadonlyArray<ITableBlockViewModel>,
    rows: ReadonlyArray<ITableRowViewModel>
  ) {
    runInAction(() => {
      for (const cache of this._cellsCache) {
        const cell = cache[0];
        const contentFunc = cache[1];
        cell.getContent = contentFunc;
      }
      this._cellsCache.clear();

      for (const block of blocks) {
        block.visibility = true;
      }

      for (const row of rows) {
        row.visibility = true;
      }
      this._rowsCache.clear();
    });
  }

  private processSearchRows(rows: ReadonlyArray<ITableRowViewModel>, row: ITableRowViewModel) {
    if (this._rowsCache.has(row)) {
      return;
    }

    this._rowsCache.add(row);
    row.visibility = true;
    if (!row.isToggled) {
      row.isToggled = true;
    }
    const parentRowId = row.parentRowId;
    if (parentRowId == null) {
      return;
    }

    const parentRow = rows.find(x => x.id === parentRowId);
    if (parentRow && this._rowsCache.has(parentRow)) {
      return;
    }

    if (parentRow) {
      this.processSearchRows(rows, parentRow);
    }
  }

  //#endregion
}
