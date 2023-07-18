/// <reference types="react" />
import { GridColumnViewModel } from './gridColumnViewModel';
import { GridRowViewModel } from './gridRowViewModel';
import { GridColumnInfo } from './gridColumnInfo';
import { GridCellViewModel } from './gridCellViewModel';
import { GridCellFormatContext } from './gridCellFormatContext';
import { ControlViewModelBase } from '../controlViewModelBase';
import { ColumnSortDirection } from '../columnSortDirection';
import { ControlKeyDownEventArgs, ICardModel, IControlViewModel } from '../../interfaces';
import { CardTypeControl, CardTypeForm, CardTypeTableControl } from 'tessa/cards/types';
import { CardRow, CardRowState } from 'tessa/cards';
import { Visibility } from 'tessa/platform';
import { EventHandler } from 'tessa/platform/eventHandler';
import { ValidationResult, ValidationResultBuilder } from 'tessa/platform/validation';
import { UIButton } from 'tessa/ui/uiButton';
import { CardValidationContext } from 'tessa/cards/validations';
import { MenuAction } from 'tessa/ui/menuAction';
import { MediaStyle } from 'ui/mediaStyle';
import { GridLayoutChangeHandler, GridResizeHandler, IGridLayout } from 'components/cardElements/grid/interfaces';
import { IGridControlViewBag } from '../../components/controls/grid';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class GridViewModel extends ControlViewModelBase implements IFilterableGrid, IGridFormContainer {
    constructor(control: CardTypeTableControl, model: ICardModel);
    private _cardModel;
    private _control;
    private _form;
    private _sectionId;
    private _sectionName;
    private _referenceColumnNames;
    private _orderColumnName;
    private _rows;
    private _rowModels;
    private _isRowFormOpened;
    private _horizontalScroll;
    private _rowsDisposer;
    private _canSelectMultipleItems;
    private _sortingColumn;
    private _sortDirection;
    private _selectionState;
    private _multiSelectButtonEnabled;
    private _columns;
    private _alwaysShowBottomToolbar;
    private _firstPageButtonEnabled;
    private _gridBase;
    private _layouts?;
    private _autohideOrder?;
    private _style?;
    private _gridViewBag?;
    private _onResize?;
    private _onLayoutChange?;
    get cardModel(): ICardModel;
    get sectionId(): string;
    get firstPageButtonEnabled(): boolean;
    set firstPageButtonEnabled(value: boolean);
    get columns(): ReadonlyArray<GridColumnViewModel>;
    /**
     * Горизонтальный скролл таблиц. По умолчанию false.
     */
    get horizontalScroll(): boolean;
    set horizontalScroll(value: boolean);
    get rows(): ReadonlyArray<GridRowViewModel>;
    get sortingColumn(): number | undefined;
    setSortingColumn(value: number | undefined, descendingByDefault?: boolean): void;
    get sortDirection(): ColumnSortDirection;
    set sortDirection(value: ColumnSortDirection);
    get selectedRow(): GridRowViewModel | null;
    get selectedRows(): GridRowViewModel[];
    get isRowFormOpened(): boolean;
    readonly leftButtons: UIButton[];
    readonly rightButtons: UIButton[];
    searchText: string;
    get canSelectMultipleItems(): boolean;
    readonly contextMenuGenerators: ((ctx: CardRowMenuContext) => void)[];
    get multiSelectButtonEnabled(): boolean;
    set multiSelectButtonEnabled(value: boolean);
    readonly addButton: UIButton;
    readonly removeButton: UIButton;
    readonly moveUpButton: UIButton;
    readonly moveDownButton: UIButton;
    readonly multiSelectButton: UIButton;
    get alwaysShowBottomToolbar(): boolean;
    set alwaysShowBottomToolbar(value: boolean);
    get layouts(): IGridLayout[] | undefined;
    set layouts(value: IGridLayout[] | undefined);
    get autohideOrder(): string[] | undefined;
    set autohideOrder(value: string[] | undefined);
    get style(): React.CSSProperties | undefined;
    set style(value: React.CSSProperties | undefined);
    readonly className: ClassNameList;
    get gridViewBag(): IGridControlViewBag | undefined;
    set gridViewBag(value: IGridControlViewBag | undefined);
    get onResize(): GridResizeHandler | undefined;
    set onResize(value: GridResizeHandler | undefined);
    get onLayoutChange(): GridLayoutChangeHandler | undefined;
    set onLayoutChange(value: GridLayoutChangeHandler | undefined);
    private searchFilterFunc;
    private getColumns;
    private rowVisibilityFilter;
    getAddRemoveButtonsVisibility(): Visibility;
    getMoveButtonsVisibility(): Visibility;
    getSearchBoxVisibility(): Visibility;
    isCanAddRow(): boolean;
    addRow(): Promise<void>;
    isCanRemoveRow(): boolean;
    removeRow(): Promise<void>;
    isCanMoveSelectedRowsUp(): boolean;
    moveSelectedRowsUp(): void;
    isCanMoveSelectedRowsDown(): boolean;
    moveSelectedRowsDown(): void;
    canSetSearchText(): boolean;
    setSearchText(text: string): void;
    editRow(rowViewModel: GridRowViewModel | null, columnIndex?: number): Promise<void>;
    getRowContextMenu: (rowViewModel: GridRowViewModel, columnIndex: number) => ReadonlyArray<MenuAction>;
    private defaultContextMenuGenerator;
    private addDefaultKeyDownHandler;
    getCaptionStyle(): MediaStyle | null;
    getControlStyle(): MediaStyle | null;
    changeColumnOrderByTarget(sourceIndex: number, targetIndex: number): void;
    private _cellFormatFunc;
    get gridCellFormatFunc(): ((context: GridCellFormatContext) => any) | null;
    set gridCellFormatFunc(value: ((context: GridCellFormatContext) => any) | null);
    openForm(): void;
    closeForm(): void;
    private getGridViewModelBase;
    readonly rowAdding: EventHandler<(args: GridRowAddingEventArgs) => void>;
    readonly rowInvoked: EventHandler<(args: GridRowEventArgs) => void>;
    readonly rowInitializing: EventHandler<(args: GridRowEventArgs) => void>;
    readonly rowInitialized: EventHandler<(args: GridRowEventArgs) => void>;
    readonly rowEditorClosing: EventHandler<(args: GridRowEventArgs) => void>;
    readonly rowEditorClosed: EventHandler<(args: GridRowEventArgs) => void>;
    readonly rowValidating: EventHandler<(args: GridRowValidationEventArgs) => void>;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
export interface RowOrderState {
    row: CardRow;
    state: CardRowState;
    order: number;
    orderChanged: boolean;
}
export declare enum GridRowAction {
    Inserted = 0,
    Opening = 1,
    Deleting = 2
}
export interface GridRowAddingEventArgs<T = GridViewModel> {
    readonly row: CardRow;
    readonly rows: ReadonlyArray<CardRow>;
    rowIndex: number;
    readonly control: T;
    readonly cardModel: ICardModel;
    cancel: boolean;
}
export interface GridRowEventArgs<T = GridViewModel, C = GridCellViewModel> {
    readonly action: GridRowAction;
    readonly control: T;
    readonly row: CardRow;
    readonly rowModel: ICardModel | null;
    readonly cardModel: ICardModel;
    cancel: boolean;
    readonly cell: C | null;
    readonly columnIndex: number;
}
export interface GridRowValidationEventArgs<T = GridViewModel> {
    readonly row: CardRow;
    readonly rowModel: ICardModel;
    readonly cardModel: ICardModel;
    readonly validationResult: ValidationResultBuilder;
    readonly control: T;
}
export interface IFilterableGrid {
    canSetSearchText: () => boolean;
    setSearchText: (text: string) => void;
}
export interface CardRowMenuContext<T = GridViewModel> {
    readonly row: GridRowViewModel;
    readonly control: T;
    readonly menuActions: MenuAction[];
    readonly cell: GridCellViewModel | null;
    readonly columnIndex: number;
}
export interface IGridFormContainer {
    cardModel: ICardModel;
    readonly isRowFormOpened: boolean;
    openForm(): any;
    closeForm(): any;
}
export declare class GridViewModelBase<T extends IControlViewModel, C> {
    readonly cardModel: ICardModel;
    readonly control: T;
    readonly rows: Array<CardRow>;
    readonly sectionName: string;
    readonly referenceColumnNames: ReadonlyArray<string>;
    readonly getParentRowId: () => guid;
    readonly parentRowSectionId: guid;
    readonly orderColumnName: string | null;
    readonly formType: CardTypeForm;
    readonly controlType: CardTypeControl;
    readonly formContainer: IGridFormContainer;
    readonly rowAdding: EventHandler<(args: GridRowAddingEventArgs<T>) => void>;
    readonly rowInvoked: EventHandler<(args: GridRowEventArgs<T, C>) => void>;
    readonly rowInitializing: EventHandler<(args: GridRowEventArgs<T, C>) => void>;
    readonly rowInitialized: EventHandler<(args: GridRowEventArgs<T, C>) => void>;
    readonly rowEditorClosing: EventHandler<(args: GridRowEventArgs<T, C>) => void>;
    readonly rowEditorClosed: EventHandler<(args: GridRowEventArgs<T, C>) => void>;
    readonly rowValidating: EventHandler<(args: GridRowValidationEventArgs<T>) => void>;
    constructor(cardModel: ICardModel, control: T, rows: Array<CardRow>, sectionName: string, referenceColumnNames: ReadonlyArray<string>, getParentRowId: () => guid, parentRowSectionId: guid, orderColumnName: string | null, formType: CardTypeForm, controlType: CardTypeControl, formContainer: IGridFormContainer, rowAdding: EventHandler<(args: GridRowAddingEventArgs<T>) => void>, rowInvoked: EventHandler<(args: GridRowEventArgs<T, C>) => void>, rowInitializing: EventHandler<(args: GridRowEventArgs<T, C>) => void>, rowInitialized: EventHandler<(args: GridRowEventArgs<T, C>) => void>, rowEditorClosing: EventHandler<(args: GridRowEventArgs<T, C>) => void>, rowEditorClosed: EventHandler<(args: GridRowEventArgs<T, C>) => void>, rowValidating: EventHandler<(args: GridRowValidationEventArgs<T>) => void>);
    private _childSectionNamesAndParentColumns;
    isCanAddRow(): boolean;
    addRow(): Promise<void>;
    editRow(action: GridRowAction, row: CardRow, cell?: C | null, columnInfo?: GridColumnInfo | null, columnIndex?: number, rowOrderStates?: RowOrderState[] | null): Promise<void>;
    isCanRemoveRow(selectedRows?: ReadonlyArray<CardRow>): boolean;
    removeRow(selectedRows: ReadonlyArray<CardRow>): Promise<void>;
    restoreRowsAfterCancel(rowOrderStates: RowOrderState[]): void;
    setPermissionsForNewRow(row: CardRow): void;
    removePermissionsForRow(row: CardRow): void;
    validateRow(e: GridRowEventArgs<T, C>): Promise<boolean>;
    validateRowWithValidators(e: GridRowEventArgs<T, C>): ValidationResult;
    modifyValidationContext: (context: CardValidationContext, e: GridRowEventArgs<T, C>) => void;
    getChildSectionNamesAndParentColumns(): [string, string][];
    isCanMoveSelectedRowsUp(selectedRows?: ReadonlyArray<CardRow>): boolean;
    moveSelectedRowsUp(selectedRows?: ReadonlyArray<CardRow>): void;
    isCanMoveSelectedRowsDown(selectedRows?: ReadonlyArray<CardRow>): boolean;
    moveSelectedRowsDown(selectedRows?: ReadonlyArray<CardRow>): void;
}
