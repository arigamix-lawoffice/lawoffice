import type { IViewControlInitializationStrategy } from './viewControlInitializationStrategy';
import { IViewControlDataProvider, ViewControlDataProviderRequest } from './viewControlDataProvider';
import type { BaseViewControlItem, ViewControlColumnMenuContext } from './contents';
import { ViewControlFilterTextViewModel, ViewControlTableGridViewModel } from './contents';
import { ViewControlViewComponent } from './viewControlViewComponent';
import { ControlViewModelBase } from '../controlViewModelBase';
import type { ICardModel } from '../../interfaces';
import { IParametersMappingContext } from '../viewMappingHelper';
import type { CardTypeControl } from 'tessa/cards/types';
import type { IStorage } from 'tessa/platform/storage';
import type { ViewMetadataSealed, RequestParameter } from 'tessa/views/metadata';
import { ViewSelectionMode } from 'tessa/views/metadata';
import type { IViewSorting, IViewComponentBase, ISelectionState, DoubleClickAction } from 'tessa/ui/views';
import type { IViewParameters } from 'tessa/ui/views/parameters';
import { SortingColumn } from 'tessa/views';
import { Paging } from 'tessa/views';
import type { SchemeDbType, EventHandler } from 'tessa/platform';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { ViewControlSettingsManager } from './viewControlSettingsManager';
import { ITableColumnViewModel } from 'tessa/ui/views/content';
import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';
export declare class ViewControlViewModel extends ControlViewModelBase implements IViewComponentBase {
    constructor(control: CardTypeControl, model: ICardModel, viewComponentFactory?: () => ViewControlViewComponent);
    protected _initializedStrategy: boolean;
    protected _initialRefreshed: boolean;
    protected _refreshTimer: number | undefined;
    protected _minRowHeight: number;
    protected _tableHeight: number;
    protected _settingsAlias: string;
    protected _userSettings: Map<string, unknown>;
    protected _userSettingsManager: ViewControlSettingsManager;
    protected _enableSavingSettings: boolean;
    protected _autoSaveSettings: boolean;
    protected _hasUnsavedSettings: boolean;
    protected _defaultOrderedColumns?: string[];
    protected _disposes: Array<(() => void) | null>;
    private orderedColumnsList;
    get initialized(): boolean;
    get initializedStrategy(): boolean;
    readonly settings: IStorage;
    readonly viewComponent: ViewControlViewComponent;
    readonly cardModel: ICardModel;
    dataProvider: IViewControlDataProvider | null;
    get viewMetadata(): ViewMetadataSealed | null;
    set viewMetadata(value: ViewMetadataSealed | null);
    get masterView(): ViewControlViewComponent | null;
    set masterView(value: ViewControlViewComponent | null);
    masterControl: ViewControlViewModel | null;
    get columns(): ReadonlyMap<string, SchemeDbType>;
    get tags(): Map<guid, IGridRowTagViewModel[]>;
    get data(): ReadonlyArray<ReadonlyMap<string, unknown>> | null;
    get isDataLoading(): boolean;
    set isDataLoading(value: boolean);
    get parameters(): IViewParameters;
    set parameters(value: IViewParameters);
    get parametersSetName(): string;
    get sorting(): IViewSorting;
    set sorting(value: IViewSorting);
    get sortingColumns(): ReadonlyArray<SortingColumn>;
    get selectionState(): ISelectionState;
    get currentPage(): number;
    set currentPage(value: number);
    get optionalPagingStatus(): boolean;
    set optionalPagingStatus(value: boolean);
    get pageCount(): number;
    set pageCount(value: number);
    get pageCountStatus(): boolean;
    set pageCountStatus(value: boolean);
    get calculatedRowCount(): number;
    set calculatedRowCount(value: number);
    get actualRowCount(): number;
    get hasNextPage(): boolean;
    get hasPreviousPage(): boolean;
    get pageLimit(): number;
    set pageLimit(value: number);
    get pagingMode(): Paging;
    set pagingMode(value: Paging);
    get selectedCellValue(): any | null;
    get selectedColumn(): string | null;
    get selectedRow(): ReadonlyMap<string, any> | null;
    get selectedRows(): ReadonlyArray<ReadonlyMap<string, any>> | null;
    get refSection(): ReadonlyArray<string> | null;
    set refSection(value: ReadonlyArray<string> | null);
    get selectionMode(): ViewSelectionMode;
    set selectionMode(value: ViewSelectionMode);
    get multiSelect(): boolean;
    set multiSelect(value: boolean);
    get multiSelectEnabled(): boolean;
    set multiSelectEnabled(value: boolean);
    get quickSearchEnabled(): boolean;
    set quickSearchEnabled(value: boolean);
    get firstRowSelection(): boolean;
    set firstRowSelection(value: boolean);
    get doubleClickAction(): DoubleClickAction;
    set doubleClickAction(value: DoubleClickAction);
    get selectRowOnContextMenu(): boolean;
    set selectRowOnContextMenu(value: boolean);
    get onRefreshing(): EventHandler<() => void>;
    get onRefreshed(): EventHandler<() => void>;
    readonly topItems: BaseViewControlItem[];
    readonly bottomItems: BaseViewControlItem[];
    readonly rightItems: BaseViewControlItem[];
    readonly leftItems: BaseViewControlItem[];
    content: BaseViewControlItem | null;
    topContent: BaseViewControlItem | null;
    bottomContent: BaseViewControlItem | null;
    filterTextViewModel: ViewControlFilterTextViewModel | null;
    readonly parametersActions: Array<(parameters: RequestParameter[]) => void>;
    get table(): ViewControlTableGridViewModel | null;
    /**
     * Минимальная высота создаваемой строки. По умолчанию равна 0.
     */
    get minRowHeight(): number;
    set minRowHeight(value: number);
    /**
     * Высота таблицы. По умолчанию равна 0.
     */
    get tableHeight(): number;
    set tableHeight(value: number);
    /**
     * Глобально уникальный алиас настроек пользователя. Может быть пустой строкой.
     */
    get settingsAlias(): string;
    set settingsAlias(value: string);
    /**
     * Признак того, что пользователю разрешено сохранять настройки отображения (порядок и скрытие колонок, и др.).
     */
    get enableSavingSettings(): boolean;
    set enableSavingSettings(value: boolean);
    /**
     * Признак того, что настройки пользователя автоматически сохраняются,
     * если также включена настройка {@link enableSavingSettings}.
     */
    get autoSaveSettings(): boolean;
    set autoSaveSettings(value: boolean);
    /**
     * Признак того, что есть несохранённые настройки пользователя.
     */
    get hasUnsavedSettings(): boolean;
    set hasUnsavedSettings(value: boolean);
    initializeStrategy(initializationStrategy: IViewControlInitializationStrategy, codeGenerated?: boolean): void;
    initialize(): void;
    dispose(): void;
    protected initializeUserSettings(): void;
    protected initializeGrouping(): void;
    protected initializeColumns(): void;
    private changeOrder;
    protected initializeColumnContextMenu(context: ViewControlColumnMenuContext): void;
    private createGroupColumn;
    private createResetGrouping;
    private createHideColumn;
    private createShowColumns;
    private createSaveSettings;
    private createResetSettings;
    refresh(): Promise<void>;
    setPageAndRefresh(page: number): Promise<void>;
    refreshWithDelay(delay: number): void;
    canRefresh(): boolean;
    filter(params?: {
        focusValue?: {
            requestParam: RequestParameter;
            criteriaIndex: number;
        };
    }): Promise<void>;
    canFilter(): boolean;
    sortColumn(column: string, addOrInverse: boolean, descendingByDefault: boolean): void;
    getViewData(): Promise<{
        columns: ReadonlyMap<string, SchemeDbType>;
        rows: ReadonlyArray<ReadonlyMap<string, any>>;
        rowCount: number;
        tags: Map<guid, IGridRowTagViewModel[]>;
    } | null>;
    protected createDataRequest(validationResult: ValidationResultBuilder, selectedMasterRowData: ReadonlyMap<string, any> | null, selectedMasterColumnName: string | null): ViewControlDataProviderRequest;
    protected setupPagingParameters: (parameters: RequestParameter[]) => void;
    protected setupViewParameters: (parameters: RequestParameter[]) => void;
    protected convertRows(columns: ReadonlyMap<string, SchemeDbType>, rows: ReadonlyArray<IStorage>): ReadonlyArray<ReadonlyMap<string, any>>;
    getParametersMappingContext(): IParametersMappingContext;
    initialRefresh(): Promise<void>;
    /**
     * Уведомляет систему о том, что настройки изменены. Реализация по умолчанию отмечает их для отправки на сервер,
     * при этом сама отправка выполняется позже.
     * Метод не проверяет возможность для пользователя сохранить настройки {@link enableSavingSettings}.
     */
    protected notifyUserSettingsChanged(): void;
    /**
     * Отмечает настройки пользователя {@link _userSettings} для текущего элемента управления
     * как изменённые и отправляет их для сохранения на сервере,
     * если установлен флаг {@link enableSavingSettings} или параметр {@link force},
     * в противном случае настройки не будут сохранены.
     * Фактическое сохранение настроек будут выполнено позже в фоновом обработчике.
     * @param force Признак того, что настройки будут сохранены, даже если пользователю запрещено их сохранять.
     */
    saveUserSettings(force?: boolean): void;
    /**
     * Возвращает признак того, что в элементе управления отсутствуют применённые настройки пользователя.
     * @returns Признак того, что в элементе управления отсутствуют применённые настройки пользователя.
     */
    userSettingsAreDefault(): boolean;
    /**
     * Сбрасывает все настройки пользователя к значениям по умолчанию. Возвращает признак того, что настройки пользователя были изменены
     * (необязательно сохранены, это определяется свойствами {@link enableSavingSettings}, {@link autoSaveSettings},
     * и параметром {@link skipAutoSave}.
     * @param skipAutoSave Признак того, что не следует автоматически сохранять настройки пользователя, даже если указано свойство {@link autoSaveSettings}.
     * @returns `true`, если настройки пользователя были изменены; `false` в противном случае.
     */
    resetUserSettings(skipAutoSave?: boolean): Promise<boolean>;
    /**
     * Отображает или скрывает указанную колонку. Возвращает признак того, что настройки пользователя были изменены
     * (необязательно сохранены, это определяется свойствами {@link enableSavingSettings}, {@link autoSaveSettings},
     * и параметром {@link skipAutoSave}.
     * @param column Колонка, видимость которой устанавливается.
     * @param value `true`, если колонка должна быть отображена; `false`, если колонка должна быть скрыта.
     * @param skipAutoSave Признак того, что не следует автоматически сохранять настройки пользователя, даже если указано свойство {@link autoSaveSettings}.
     * @returns `true`, если настройки пользователя были изменены; `false` в противном случае.
     */
    setColumnVisibility(column: ITableColumnViewModel, value: boolean, skipAutoSave?: boolean): boolean;
    /**
     * Выполняет группировку по указанной колонке. Возвращает признак того, что настройки пользователя были изменены
     * (необязательно сохранены, это определяется свойствами {@link enableSavingSettings}, {@link autoSaveSettings},
     * и параметром {@link skipAutoSave}).
     * @param column Колонка, группировка по которой выполняется.
     * @param skipAutoSave Признак того, что не следует автоматически сохранять настройки пользователя, даже если указано свойство {@link autoSaveSettings}.
     * @returns `true`, если настройки пользователя были изменены; `false` в противном случае.
     */
    setGroupingColumn(column: ITableColumnViewModel, skipAutoSave?: boolean): boolean;
    /**
     * Сбрасывает группировку. Возвращает признак того, что настройки пользователя были изменены
     * (необязательно сохранены, это определяется свойствами {@link enableSavingSettings}, {@link autoSaveSettings},
     * и параметром {@link skipAutoSave}).
     * @param skipAutoSave Признак того, что не следует автоматически сохранять настройки пользователя, даже если указано свойство {@link autoSaveSettings}.
     * @returns `true`, если настройки пользователя были изменены; `false` в противном случае.
     */
    resetGrouping(skipAutoSave?: boolean): boolean;
    /**
     * Обновляет настройки пользователя по указанному порядку следования алиасов колонок.
     * Устанавливает признак того, что в элементе управления имеются несохранённые настройки.
     * Возвращает признак того, что настройки пользователя были изменены
     * (необязательно сохранены, это определяется свойствами {@link enableSavingSettings}, {@link autoSaveSettings},
     * и параметром {@link skipAutoSave}).
     * @param orderedColumns Упорядоченный список алиасов колонок.
     * @param skipAutoSave Признак того, что не следует автоматически сохранять настройки пользователя, даже если указано свойство {@link autoSaveSettings}.
     * @returns `true`, если настройки пользователя были изменены; `false` в противном случае.
     */
    updateOrderedColumns(orderedColumns: readonly string[], skipAutoSave?: boolean): boolean;
    /**
     * Обновляет настройки пользователя по сортировке в соответствии с текущим состоянием элемента управления.
     * Устанавливает признак того, что в элементе управления имеются несохранённые настройки.
     * Возвращает признак того, что настройки пользователя были изменены
     * (необязательно сохранены, это определяется свойствами {@link enableSavingSettings}, {@link autoSaveSettings},
     * и параметром {@link skipAutoSave}).
     * @param skipAutoSave Признак того, что не следует автоматически сохранять настройки пользователя, даже если указано свойство {@link autoSaveSettings}.
     * @returns `true`, если настройки пользователя были изменены; `false` в противном случае.
     */
    updateSorting(skipAutoSave?: boolean): boolean;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
