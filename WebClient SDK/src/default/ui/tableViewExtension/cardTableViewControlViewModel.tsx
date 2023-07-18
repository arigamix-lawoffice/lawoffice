import { observable, reaction, runInAction } from 'mobx';
import classNames from 'classnames';
import { CardTableViewComponent } from './cardTableViewComponent';
import { CardTableViewCellViewModel } from './cardTableViewCellViewModel';
import { CardTableViewFlagCellViewModel } from './cardTableViewFlagCellViewModel';
import { CardTableViewPanelViewModel } from './cardTableViewPanel';
import { CardTableViewRowData } from './cardTableViewRowData';
import { CardTableViewFormContainerViewModel } from './cardTableViewFormContainer';
import { CardMetadataColumnType, CardMetadataSection } from 'tessa/cards/metadata';
import {
  CardTypeColumn,
  CardTypeColumnFlags,
  CardTypeControl,
  CardTypeTableControl
} from 'tessa/cards/types';
import { LocalizationManager } from 'tessa/localization';
import {
  SchemeDbType,
  DotNetType,
  EventHandler,
  hasNotFlag,
  unseal,
  Visibility
} from 'tessa/platform';
import { ArrayStorage, IStorage } from 'tessa/platform/storage';
import { MenuAction, SeparatorMenuAction, tryGetFromSettings, UIButton } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
import {
  ViewControlViewModel,
  GridColumnInfo,
  GridRowAddingEventArgs,
  GridRowEventArgs,
  GridRowValidationEventArgs,
  GridViewModelBase,
  GridRowAction
} from 'tessa/ui/cards/controls';
import { ViewColumnMetadata } from 'tessa/views/metadata';
import { TableColumnViewModel, ITableRowViewModel } from 'tessa/ui/views/content';
import { CardRow, CardRowsListener, CardRowState, PermissionHelper } from 'tessa/cards';
import { Highlighter } from 'ui';
import { DoubleClickInfo } from 'tessa/ui/views';
import { Paging, SortDirection } from 'tessa/views';
import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';

export class CardTableViewControlViewModel extends ViewControlViewModel {
  //#region ctor

  constructor(
    control: CardTypeControl,
    model: ICardModel,
    tableControl: CardTypeTableControl,
    extensionSettings: IStorage | null
  ) {
    super(control, model, () => new CardTableViewComponent());

    if (!tableControl.sectionId) {
      throw new Error('Control section id is not defined.');
    }
    const metadataSectionSealed = model.cardMetadata.getSectionById(tableControl.sectionId);
    if (!metadataSectionSealed) {
      throw new Error(`Can not find section metadata with id - ${tableControl.sectionId}.`);
    }

    if (!metadataSectionSealed.id || !metadataSectionSealed.name) {
      throw new Error('Section id or section name not defined in metadata.');
    }

    this._tableSettings = tableControl;
    this._extensionSettings = extensionSettings;
    this.pagingMode = tryGetFromSettings<boolean>(extensionSettings, 'AllowPaging', false)
      ? Paging.Always
      : Paging.No;
    this.pageLimit = tryGetFromSettings<number>(extensionSettings, 'PagingRowsLimit', 20);
    this.multiSelect = tryGetFromSettings<boolean>(
      extensionSettings,
      'AllowMultiSelectSetting',
      false
    );
    this._alwaysShowBottomToolbar = tryGetFromSettings(
      extensionSettings,
      'AlwaysShowBottomToolbar',
      false
    );
    this._canSort = tryGetFromSettings<boolean>(extensionSettings, 'CanSort', false);
    this._orderedColumns = this._tableSettings.columns.filter(x =>
      hasNotFlag(x.flags, CardTypeColumnFlags.Hidden)
    );
    this._isReadonly =
      tryGetFromSettings<boolean>(extensionSettings, 'IsReadOnly', false) ||
      PermissionHelper.instance.getReadOnlyTableControl(
        model,
        tableControl,
        metadataSectionSealed.name
      );

    this.rowAdding = new EventHandler();
    this.rowInvoked = new EventHandler();
    this.rowInitializing = new EventHandler();
    this.rowInitialized = new EventHandler();
    this.rowEditorClosing = new EventHandler();
    this.rowEditorClosed = new EventHandler();
    this.rowValidating = new EventHandler();
    this._refreshOnRowDeletion = true;
  }

  //#endregion

  //#region fields

  private _tableSettings: CardTypeTableControl;

  private _extensionSettings: IStorage | null;

  private _orderedColumns: CardTypeColumn[];

  private _columnInfos: Map<string, GridColumnInfo>;

  private _sectionName: string;

  private _flagColumnName: string;

  private _orderColumnName: string;

  private _referenceColumnNames: string[];

  private _canSort: boolean;

  private _sectionRows: ArrayStorage<CardRow>;

  private _rowsListener: CardRowsListener | null;

  private _rowsData: Array<CardTableViewRowData>;

  private _formContainer: CardTableViewFormContainerViewModel;

  private _gridBase: GridViewModelBase<
    CardTableViewControlViewModel,
    CardTableViewCellViewModel
  > | null = null;

  @observable
  private _searchText: string;

  @observable
  protected _alwaysShowBottomToolbar: boolean;

  private _orderReactions: Map<CardTableViewRowData, Function> = new Map();

  //#endregion

  //#region props

  get sectionName(): string {
    return this._sectionName;
  }

  get searchText(): string {
    return this._searchText;
  }

  public get alwaysShowBottomToolbar(): boolean {
    return this._alwaysShowBottomToolbar;
  }
  public set alwaysShowBottomToolbar(value: boolean) {
    this._alwaysShowBottomToolbar = value;
  }

  addRowButton: UIButton;

  deleteRowsButton: UIButton;

  moveUpButton: UIButton;

  moveDownButton: UIButton;

  private _refreshOnRowDeletion: boolean;

  //#endregion

  //#region methods

  initialize(): void {
    if (this._initialized || !this._initializedStrategy) {
      return;
    }

    super.initialize();

    this.initializeForm();
    this.initializeCardColumnInfo();
    this.initializeReferenceColumns();
    this.initializeRowContexMenuGenerators();
    this.initializeViewColumns();
    this.initializeRows();
    // this.subscribeToOwnedSections(); ???
    this.initializeClicks();
    this.initializeKeyDownHandlers();
    this.initializeButtons();
  }

  dispose(): void {
    super.dispose();

    if (this._rowsListener) {
      this._rowsListener.stop();
      this._rowsListener = null;
    }

    this.rowAdding.clear();
    this.rowInvoked.clear();
    this.rowInitializing.clear();
    this.rowInitialized.clear();
    this.rowEditorClosing.clear();
    this.rowEditorClosed.clear();
    this.rowValidating.clear();

    for (const dispose of this._orderReactions.values()) {
      if (dispose) {
        dispose();
      }
    }
    this._orderReactions.clear();
  }

  private initializeForm() {
    this._formContainer = new CardTableViewFormContainerViewModel(this);
    this.bottomItems.push(this._formContainer);
  }

  private initializeCardColumnInfo() {
    const metadataSection = this.cardModel.cardMetadata.getSectionById(
      this._tableSettings.sectionId ?? ''
    )!;
    this._sectionName = metadataSection?.name ?? '';

    const flagColumnId = tryGetFromSettings<guid>(this._extensionSettings, 'FlagColumnID');
    if (flagColumnId != null) {
      this._flagColumnName = metadataSection?.columns.find(x => x.id === flagColumnId)?.name ?? '';
    }

    const orderColumnId = tryGetFromSettings<guid>(this._extensionSettings, 'OrderColumnName');
    if (orderColumnId != null) {
      this._orderColumnName =
        metadataSection?.columns.find(x => x.id === orderColumnId)?.name ?? '';
    }

    this._columnInfos = new Map(
      this._orderedColumns.map((x, i) => [
        i.toString(),
        new GridColumnInfo(
          x,
          metadataSection.seal(),
          x.ownedSectionId != null
            ? unseal<CardMetadataSection>(
                this.cardModel.cardMetadata.getSectionById(x.ownedSectionId)
              )
            : null
        )
      ])
    );
  }

  private initializeReferenceColumns() {
    if (this.masterControl) {
      const masterControl = this.masterControl as CardTableViewControlViewModel;
      this._referenceColumnNames = this.cardModel.cardMetadata
        .getSectionByName(this._sectionName)!
        .columns.filter(
          x =>
            x.parentRowSection != null &&
            x.columnType === CardMetadataColumnType.Physical &&
            x.parentRowSection.name === masterControl.sectionName
        )
        .map(x => x.name ?? '');
    } else if (this.cardModel.table) {
      const table = this.cardModel.table!;
      this._referenceColumnNames = this.cardModel.cardMetadata
        .getSectionByName(this._sectionName)!
        .columns.filter(
          x =>
            x.parentRowSection != null &&
            x.columnType === CardMetadataColumnType.Physical &&
            x.parentRowSection.name === table.section.name
        )
        .map(x => x.name ?? '');
    } else {
      this._referenceColumnNames = [];
    }
  }

  private initializeRowContexMenuGenerators() {
    const table = this.table;
    if (!table) {
      return;
    }

    table.rowContextMenuGenerators.push(ctx => {
      ctx.menuActions.push(
        new SeparatorMenuAction(false, 'Separator'),
        new MenuAction(
          'Remove',
          '$UI_Controls_GridControl_Delete',
          'ta icon-thin-058',
          () => this.deleteRowsAction(),
          null,
          !this.onCanExecuteDeleteRows()
        )
      );
    });
  }

  private initializeViewColumns() {
    if (!this.table) {
      return;
    }

    this.table.createColumnAction = opt => {
      if (this._flagColumnName != null && opt.columnName === 'FlagColumn') {
        const columnCaption = tryGetFromSettings<string>(
          this._extensionSettings,
          'FlagColumnCaption'
        );
        const columnMetadata = new ViewColumnMetadata();
        columnMetadata.caption = !columnCaption
          ? ' '
          : LocalizationManager.instance.localize(columnCaption);
        columnMetadata.sortBy = null;
        columnMetadata.alias = 'FlagColumn';
        opt.metadata = columnMetadata;
        opt.canSort = false;
        opt.isReferencedColumn = false;
        opt.header = columnMetadata.caption;
        opt.referenceMetadata = undefined;
        opt.viewMetadata = this.viewMetadata!;
        opt.visibility = true;

        return new TableColumnViewModel(opt);
      }

      const columnInfo = this._columnInfos.get(opt.columnName);
      if (!columnInfo) {
        throw new Error(`Can not find ColumnInfo with name ${opt.columnName}.`);
      }
      let caption = LocalizationManager.instance.localize(columnInfo.type.caption);
      if (!caption) {
        caption = opt.columnName;
      }

      const columnMetadata = new ViewColumnMetadata();
      columnMetadata.caption = caption;
      columnMetadata.sortBy = this._canSort ? opt.columnName : null;
      opt.metadata = columnMetadata;
      opt.canSort = this._canSort;
      opt.isReferencedColumn = false;
      opt.header = caption;
      opt.referenceMetadata = undefined;
      opt.viewMetadata = this.viewMetadata!;
      opt.visibility = true;

      return new TableColumnViewModel(opt);
    };

    const columns = new Map<string, SchemeDbType>();
    if (this._flagColumnName != null) {
      columns.set('FlagColumn', SchemeDbType.Boolean);
    }
    for (let i = 0; i < this._columnInfos.size; i++) {
      columns.set(i.toString(), SchemeDbType.String);
    }
    (this.viewComponent as CardTableViewComponent).initColumns(columns);
  }

  private initializeRows() {
    if (!this.table) {
      return;
    }

    const card = this.cardModel.card;

    const mainSection = this.cardModel.card.sections.get(this._sectionName)!;
    this._sectionRows = mainSection.rows;

    this.table.createCellAction = opt => {
      if (opt.column.metadata?.alias === 'FlagColumn') {
        return new CardTableViewFlagCellViewModel(opt);
      }

      const cell = new CardTableViewCellViewModel(opt);
      cell.getContent = () => {
        const searchText = this.searchText;
        const value = cell.convertedValue;
        if (!searchText) {
          return value;
        }

        return (
          <Highlighter
            searchWords={searchText}
            textToHighlight={value}
            caseSensitive={false}
            highlightClassName="highlighted-text"
          />
        );
      };
      return cell;
    };

    const rows = this._sectionRows.map(row => {
      const rowData = new CardTableViewRowData(
        card,
        row,
        this._columnInfos,
        this._orderColumnName,
        this._flagColumnName,
        this.sectionName
      );
      if (this._orderColumnName) {
        this._orderReactions.set(
          rowData,
          reaction(
            () => rowData.order,
            () => {
              this.refreshWithDelay(50);
            }
          )
        );
      }
      return rowData;
    });
    this._rowsData = rows;

    this._rowsListener = new CardRowsListener();
    this._rowsListener.rowInserted.add((storage, row) => {
      const dataIndex = storage.indexOf(row);
      if (dataIndex === -1) {
        return;
      }
      const newRowData = new CardTableViewRowData(
        card,
        row,
        this._columnInfos,
        this._orderColumnName,
        this._flagColumnName,
        this.sectionName
      );
      this._rowsData.splice(dataIndex, 0, newRowData);

      if (this._orderColumnName) {
        this._orderReactions.set(
          newRowData,
          reaction(
            () => newRowData.order,
            () => {
              this.refreshWithDelay(50);
            }
          )
        );
      }

      this.refresh();
    });

    this._rowsListener.rowDeleted.add((_, row) => {
      const dataIndex = this._rowsData.findIndex(x => x.rowId === row.rowId);
      if (dataIndex === -1) {
        return;
      }
      const rowData = this._rowsData.splice(dataIndex, 1)[0];
      if (rowData && this._orderColumnName) {
        const dispose = this._orderReactions.get(rowData);
        if (dispose) {
          dispose();
        }
        this._orderReactions.delete(rowData);
      }
      if (this._refreshOnRowDeletion) {
        this.refresh();
      }
    });

    this._rowsListener.start(this._sectionRows);
  }

  private initializeClicks() {
    this.viewComponent.doubleClickAction = async (info: DoubleClickInfo) => {
      const row = info.selectedObject as CardTableViewRowData;
      const table = this.table;
      if (!row || !table) {
        return;
      }

      const columnName = info.columnName;
      let cell: CardTableViewCellViewModel | null = null;
      let columnInfo: GridColumnInfo | null = null;
      if (columnName) {
        const tableRow = table.rows.find(x => x.data === row)!;
        cell = tableRow.getByName(columnName) as CardTableViewCellViewModel;
        columnInfo = cell.columnInfo;
      }
      await this.getGridViewModelBase().editRow(
        GridRowAction.Opening,
        row.cardRow,
        cell,
        columnInfo
      );
    };

    const table = this.table;
    if (table) {
      this._disposes.push(
        table.modifyRowActions.addWithDispose(
          row =>
            (row.onMouseDown = async e => {
              if (e.button === 1) {
                const rowData = row.data as CardTableViewRowData;
                await this.getGridViewModelBase().editRow(GridRowAction.Opening, rowData.cardRow);
              }
            })
        )
      );
    }
  }

  private initializeKeyDownHandlers() {
    if (!this.table) {
      return;
    }
    this.table.keyDown.add(args => {
      const { event } = args;
      const code = event.keyCode || event.charCode;
      if (code === 46) {
        event.stopPropagation();
        event.preventDefault();
        if (this.onCanExecuteDeleteRows()) {
          this.deleteRowsAction();
        }
      }
    });
  }

  private initializeButtons() {
    const panel = new CardTableViewPanelViewModel(this);

    this.addRowButton = UIButton.create({
      buttonAction: () => {
        if (this.onCanExecuteAddRow()) {
          this.addRowAction();
        }
      },
      name: 'add-row-button',
      className: 'button-plain',
      icon: 'icon-grid-plus',
      isEnabled: () => this.onCanExecuteAddRow(),
      visibility: () => (!this.isReadOnly ? Visibility.Visible : Visibility.Collapsed),
      contextExecutor: this.cardModel.executeInContext
    });
    this.deleteRowsButton = UIButton.create({
      buttonAction: () => {
        if (this.onCanExecuteDeleteRows()) {
          this.deleteRowsAction();
        }
      },
      name: 'delete-rows-button',
      className: 'button-plain',
      icon: 'icon-grid-trashcan',
      isEnabled: () => this.onCanExecuteDeleteRows(),
      visibility: () => (!this.isReadOnly ? Visibility.Visible : Visibility.Collapsed),
      contextExecutor: this.cardModel.executeInContext
    });

    if (this._orderColumnName) {
      this.moveUpButton = UIButton.create({
        buttonAction: () => {
          if (this.onCanExecuteMoveSelectedRowsUp()) {
            this.moveSelectedRowsUp();
          }
        },
        name: 'order-column-name-button',
        isEnabled: () => this.onCanExecuteMoveSelectedRowsUp(),
        visibility: () =>
          !this.isReadOnly && !!this._orderColumnName ? Visibility.Visible : Visibility.Collapsed,
        className: 'button-plain',
        icon: 'icon-grid-up',
        contextExecutor: this.cardModel.executeInContext
      });
      this.moveDownButton = UIButton.create({
        buttonAction: () => {
          if (this.onCanExecuteMoveSelectedRowsDown()) {
            this.moveSelectedRowsDown();
          }
        },
        name: 'move-down-button',
        isEnabled: () => this.onCanExecuteMoveSelectedRowsDown(),
        visibility: () =>
          !this.isReadOnly && !!this._orderColumnName ? Visibility.Visible : Visibility.Collapsed,
        className: 'button-plain',
        icon: 'icon-grid-down',
        contextExecutor: this.cardModel.executeInContext
      });

      const sortByOrderButton = UIButton.create({
        buttonAction: () => {
          this.sorting.clear();
          const inPagingMode =
            this.pagingMode === Paging.Always ||
            (this.pagingMode === Paging.Optional && this.optionalPagingStatus);
          if (inPagingMode && this.currentPage !== 1) {
            this.currentPage = 1;
          } else {
            this.refresh();
          }
        },
        name: 'sort-by-order-button',
        icon: 'icon-thin-351',
        className: 'button-plain',
        isEnabled: () => !this.isDataLoading && this.sorting.columns.length > 0,
        contextExecutor: this.cardModel.executeInContext
      });

      panel.leftButtons.push(this.moveUpButton, this.moveDownButton, sortByOrderButton);
    }

    const multiSelectButton = UIButton.create({
      buttonAction: () => {
        this.multiSelectEnabled = !this.multiSelectEnabled;
      },
      name: 'multiselect',
      icon: 'icon-grid-multiselect',
      isEnabled: () => this.multiSelect,
      visibility: () =>
        this.multiSelect || this.alwaysShowBottomToolbar
          ? Visibility.Visible
          : Visibility.Collapsed,
      className: () =>
        classNames('button-plain', {
          active: this.multiSelectEnabled
        }),
      contextExecutor: this.cardModel.executeInContext
    });

    panel.rightButtons.push(multiSelectButton, this.addRowButton, this.deleteRowsButton);
    this.bottomContent = panel;
  }

  private getGridViewModelBase() {
    if (this._gridBase) {
      return this._gridBase;
    }
    this._gridBase = new GridViewModelBase(
      this.cardModel,
      this,
      this._sectionRows,
      this.sectionName,
      this._referenceColumnNames,
      () => {
        const masterSelectedRow = this.masterControl?.selectedRow;
        return (
          (masterSelectedRow as CardTableViewRowData)?.rowId ?? this.cardModel.table?.row.rowId
        );
      },
      this._tableSettings.sectionId!,
      this._orderColumnName,
      this._tableSettings.form!,
      unseal<CardTypeControl>(this.cardTypeControl),
      this._formContainer,
      this.rowAdding,
      this.rowInvoked,
      this.rowInitializing,
      this.rowInitialized,
      this.rowEditorClosing,
      this.rowEditorClosed,
      this.rowValidating
    );
    return this._gridBase;
  }

  protected override initializeColumns(): void {}

  async addRowAction(): Promise<void> {
    await this.getGridViewModelBase().addRow();
  }

  onCanExecuteAddRow(): boolean {
    const emptyMaster = this.masterControl && !this.masterControl.selectedRow;
    return !this.isReadOnly && this.getGridViewModelBase().isCanAddRow() && !emptyMaster;
  }

  async deleteRowsAction(): Promise<void> {
    if (this.selectedRows) {
      const rows = this.selectedRows.map(x => (x as CardTableViewRowData).cardRow);

      // В случае, когда строк несколько блокируем обновление
      // чтобы оно выполнилось один раз с полным набором удаленных строк,
      // а не по разу для каждой. В противном случае попытки обновления
      // следующие за первой могут быть проигнорированы.
      this._refreshOnRowDeletion = false;
      await this.getGridViewModelBase().removeRow(rows);
      this._refreshOnRowDeletion = true;
      await this.refresh();
    }
  }

  onCanExecuteDeleteRows(): boolean {
    return (
      !this.isReadOnly &&
      this.getGridViewModelBase().isCanRemoveRow(
        this.selectedRows?.map(x => (x as CardTableViewRowData).cardRow)
      )
    );
  }

  onCanExecuteMoveSelectedRowsUp(): boolean {
    return (
      !this.isReadOnly &&
      !this.searchText &&
      this.getGridViewModelBase().isCanMoveSelectedRowsUp(
        this.selectedRows?.map(x => (x as CardTableViewRowData).cardRow)
      )
    );
  }

  moveSelectedRowsUp(): void {
    this.getGridViewModelBase().moveSelectedRowsUp(
      this.selectedRows?.map(x => (x as CardTableViewRowData).cardRow)
    );
    if (
      this.selectedRows &&
      this.selectedRows.length > 0 &&
      this.selectedRows.some(x => !this.inCurrentPage(x as CardTableViewRowData))
    ) {
      this.isDataLoading = true;
      this.currentPage--;
      this.isDataLoading = false;
    }
  }

  onCanExecuteMoveSelectedRowsDown(): boolean {
    return (
      !this.isReadOnly &&
      !this.searchText &&
      this.getGridViewModelBase().isCanMoveSelectedRowsDown(
        this.selectedRows?.map(x => (x as CardTableViewRowData).cardRow)
      )
    );
  }

  moveSelectedRowsDown(): void {
    this.getGridViewModelBase().moveSelectedRowsDown(
      this.selectedRows?.map(x => (x as CardTableViewRowData).cardRow)
    );
    if (
      this.selectedRows &&
      this.selectedRows.length > 0 &&
      this.selectedRows.some(x => !this.inCurrentPage(x as CardTableViewRowData))
    ) {
      this.isDataLoading = true;
      this.currentPage++;
      this.isDataLoading = false;
    }
  }

  public getSearchBoxVisibility(): Visibility {
    return !this.isReadOnly || this.alwaysShowBottomToolbar
      ? Visibility.Visible
      : Visibility.Collapsed;
  }

  async getViewData(): Promise<{
    columns: ReadonlyMap<string, SchemeDbType>;
    rows: ReadonlyArray<ReadonlyMap<string, any>>;
    rowCount: number;
    tags: Map<guid, IGridRowTagViewModel[]>;
  } | null> {
    const masterSelectedRow = this.masterControl?.selectedRow;

    // masterRowID - ссылка на родительскую таблицу (секцию) в окне редактирования строки или в master-detail
    let masterRowId = this.cardModel.table?.row.rowId;
    if (masterSelectedRow) {
      masterRowId = (masterSelectedRow as CardTableViewRowData).rowId;
    }

    const rows = this._rowsData.filter(x => {
      const row = x as CardTableViewRowData;
      for (const refColumn of this._referenceColumnNames) {
        if (row.cardRow.get(refColumn) !== masterRowId) {
          return false;
        }
      }

      if (row.cardRow.state === CardRowState.Deleted) {
        return;
      }

      if (this.searchText) {
        const st = this.searchText.toLowerCase();
        for (const i of row.columnInfos.values()) {
          const value: string = i.getValue(row.cardRow, row.card).formattedValue;
          const includes = value != null && value.toLowerCase && value.toLowerCase().includes(st);
          if (includes) {
            return true;
          }
        }

        return false;
      }

      return true;
    });

    // сортируем строки. Если есть ордеринг и нет сортировки по столбцу, то сортируем по ордерингу
    const sortingColumn = this.sorting.columns[0];
    if (sortingColumn && rows.length > 0) {
      // from GridViewModel rows computed
      const sortDirectionNumber = sortingColumn.sortDirection === SortDirection.Ascending ? 1 : -1;
      let sortRowsFunc = (a: CardTableViewRowData, b: CardTableViewRowData) => {
        const aValue = a.get(sortingColumn.alias);
        const bValue = b.get(sortingColumn.alias);
        if (typeof aValue === 'string' || typeof bValue === 'string') {
          return sortDirectionNumber * (aValue ?? '').localeCompare(bValue ?? '');
        }
        return sortDirectionNumber * ((aValue ?? 0) - (bValue ?? 0));
      };
      const sortColumnInfo = rows[0].columnInfos.get(sortingColumn.alias)!;
      const physColumnIds = sortColumnInfo.type.physicalColumnIdList;
      if (physColumnIds.length === 1) {
        const physColumn = this.cardModel.cardMetadata
          .getSectionById(this._tableSettings.sectionId!)!
          .getColumnById(physColumnIds[0])!;
        const netType = physColumn.metadataType!.dotNetType;
        if (netType === DotNetType.DateTime || netType === DotNetType.DateTimeOffset) {
          sortRowsFunc = (a: CardTableViewRowData, b: CardTableViewRowData) => {
            const avalue = new Date(a.cardRow.get(physColumn.name!) ?? null);
            const bvalue = new Date(b.cardRow.get(physColumn.name!) ?? null);
            return sortDirectionNumber * (avalue.getTime() - bvalue.getTime());
          };
        }
      }

      rows.sort(sortRowsFunc);
    } else if (this._orderColumnName) {
      rows.sort((a, b) => a.order - b.order);
    }

    return {
      columns: this.columns,
      rows: rows,
      rowCount: rows.length,
      tags: new Map<guid, IGridRowTagViewModel[]>()
    };
  }

  public canSetSearchText(): boolean {
    return true;
  }

  public setSearchText(text: string): void {
    runInAction(() => {
      if (text) {
        text = text.trim();
      }
      this._searchText = text;
    });
    this.refresh();
  }

  public async editRow(row: ITableRowViewModel | null): Promise<void> {
    if (!row) {
      return;
    }

    const rowData = row.data as CardTableViewRowData;
    await this.getGridViewModelBase().editRow(GridRowAction.Opening, rowData.cardRow);
  }

  private inCurrentPage(row: CardTableViewRowData): boolean {
    const index = this._orderColumnName ? row.order : this._rowsData.indexOf(row);
    const maxIndex = this.pageLimit * this.currentPage;
    const minIndex = maxIndex - this.pageLimit;
    return index >= minIndex && index < maxIndex;
  }

  //#endregion

  //#region events

  readonly rowAdding: EventHandler<
    (args: GridRowAddingEventArgs<CardTableViewControlViewModel>) => void
  >;

  readonly rowInvoked: EventHandler<
    (args: GridRowEventArgs<CardTableViewControlViewModel, CardTableViewCellViewModel>) => void
  >;

  readonly rowInitializing: EventHandler<
    (args: GridRowEventArgs<CardTableViewControlViewModel, CardTableViewCellViewModel>) => void
  >;

  readonly rowInitialized: EventHandler<
    (args: GridRowEventArgs<CardTableViewControlViewModel, CardTableViewCellViewModel>) => void
  >;

  readonly rowEditorClosing: EventHandler<
    (args: GridRowEventArgs<CardTableViewControlViewModel, CardTableViewCellViewModel>) => void
  >;

  readonly rowEditorClosed: EventHandler<
    (args: GridRowEventArgs<CardTableViewControlViewModel, CardTableViewCellViewModel>) => void
  >;

  readonly rowValidating: EventHandler<
    (args: GridRowValidationEventArgs<CardTableViewControlViewModel>) => void
  >;

  //#endregion
}
