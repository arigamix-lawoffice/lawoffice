/// <reference types="react" />
import { IBaseContentItem } from './content';
import { IViewParameters } from './parameters';
import { IWorkplaceViewModel } from './workplaceViewModel';
import { IViewContext } from './viewContext';
import { PagingContext } from './pagingContext';
import { SelectAction } from './selectFromViewContext';
import { IViewSorting } from './viewSorting';
import { IViewComponentBase, ViewComponentBase } from './viewComponentBase';
import { IRowCounter } from './rowCounter';
import { IReactRefProvider, ReactRef } from '../reactRefProvider';
import { MenuAction } from '../menuAction';
import { IUIContext } from '../uiContext';
import { IExtensionExecutor } from 'tessa/extensions';
import { DataNodeMetadataSealed, WorkplaceViewParameterLinkMetadataSealed, ParameterSourceSealed } from 'tessa/views/workplaces';
import { SortingColumn } from 'tessa/views/sortingColumn';
import { SchemeDbType } from 'tessa/platform';
import { ViewParameterMetadataSealed, ViewMetadataSealed } from 'tessa/views/metadata';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
import { ITessaViewResult, ITessaViewRequest, Paging, ITessaView } from 'tessa/views';
import { SchemeType } from 'tessa/scheme';
import { IGridRowTagViewModel } from 'components/cardElements/grid/interfaces';
export interface BuildContentDelegate {
    (model: IWorkplaceViewComponent, content: Map<string, IBaseContentItem>, contentFactories: Map<string, (c: IWorkplaceViewComponent) => IBaseContentItem | null>): void;
}
export interface ConvertParameterValueDelegate {
    (model: IWorkplaceViewComponent, sourceValue: any, metadata: ViewParameterMetadataSealed): any;
}
export interface DataColumnConverterDelegate {
    (model: IWorkplaceViewComponent, columnName: string, columnDataType: SchemeDbType): [
        string,
        SchemeDbType
    ];
}
export interface DataColumnsConverterDelegate {
    (model: IWorkplaceViewComponent, columnNames: string[], columnDataTypes: SchemeType[]): [
        string,
        SchemeDbType
    ][];
}
export interface DataRowConverterDelegate {
    (model: IWorkplaceViewComponent, columns: Map<string, SchemeDbType>, data: any): Map<string, any>;
}
export interface DataRowsConverterDelegate {
    (model: IWorkplaceViewComponent, columns: Map<string, SchemeDbType>, objects: any[]): Map<string, any>[];
}
export interface GetDataDelegate {
    (model: IWorkplaceViewComponent, request: ITessaViewRequest | null): Promise<ITessaViewResult | null>;
}
export interface GetPageLimitDelegate {
    (model: IWorkplaceViewComponent): number;
}
export interface GetPagingModeDelegate {
    (model: IWorkplaceViewComponent): Paging;
}
export interface GetRequestDelegate {
    (model: IWorkplaceViewComponent, parameters: RequestParameter[], setupActions: Array<(c: IWorkplaceViewComponent, r: ITessaViewRequest) => void>): ITessaViewRequest | null;
}
export interface GetViewDelegate {
    (model: IWorkplaceViewComponent): ITessaView | null;
}
export interface GetViewMetadataDelegate {
    (model: IWorkplaceViewComponent): ViewMetadataSealed | null;
}
export interface GetWorkplaceViewMetadataDelegate {
    (model: IWorkplaceViewComponent): DataNodeMetadataSealed | null;
}
export interface SetRequestPagingParametersDelegate {
    (model: IWorkplaceViewComponent, parameters: RequestParameter[], pagingContext: PagingContext): void;
}
export interface SelectFromViewDelegate extends SelectAction {
}
export interface IViewContextMenuContext {
    readonly viewContext: IViewContext;
    readonly menuActions: MenuAction[];
    readonly uiContextExecutor: (action: (context: IUIContext) => void) => void;
}
export interface IWorkplaceViewComponent extends IViewContext, IReactRefProvider, IViewComponentBase, IRowCounter {
    readonly uiId: guid;
    readonly extensionExecutor: IExtensionExecutor;
    readonly viewContext: IViewContext;
    canAutoFocus: boolean;
    readonly content: ReadonlyMap<string, IBaseContentItem>;
    readonly contentByArea: ReadonlyMap<number, IBaseContentItem[]>;
    readonly contentFactories: Map<string, (c: IWorkplaceViewComponent) => IBaseContentItem | null>;
    readonly dataNodeMetadata: DataNodeMetadataSealed;
    readonly contextMenuGenerators: Array<(ctx: IViewContextMenuContext) => void>;
    calculateMaxHeight: boolean;
    buildContent: BuildContentDelegate;
    convertParameterValue: ConvertParameterValueDelegate;
    dataColumnConverter: DataColumnConverterDelegate;
    dataColumnsConverter: DataColumnsConverterDelegate;
    dataRowConverter: DataRowConverterDelegate;
    dataRowsConverter: DataRowsConverterDelegate;
    getData: GetDataDelegate;
    getPageLimit: GetPageLimitDelegate;
    getPagingMode: GetPagingModeDelegate;
    getRequest: GetRequestDelegate;
    getView: GetViewDelegate;
    getViewMetadata: GetViewMetadataDelegate;
    getWorkplaceViewMetadata: GetWorkplaceViewMetadataDelegate;
    setRequestPagingParameters: SetRequestPagingParametersDelegate;
    selectAction: SelectFromViewDelegate | null;
    initialize(): any;
    dispose(): any;
    attach(observer: IViewContext): any;
    detach(observer: IViewContext): any;
    setTableVisibleColumnOrdering(func: () => ReadonlyArray<string>): any;
}
export declare class WorkplaceViewComponent extends ViewComponentBase<IWorkplaceViewComponent> implements IWorkplaceViewComponent {
    constructor(workplace: IWorkplaceViewModel, masterContext: IWorkplaceViewComponent | null, parametersFactory: (c: IWorkplaceViewComponent) => {
        parameters: IViewParameters;
        needDispose: boolean;
    }, sortingFactory: (m: ViewMetadataSealed | null) => IViewSorting);
    protected _parametersFactory: (c: IWorkplaceViewComponent) => {
        parameters: IViewParameters;
        needDispose: boolean;
    };
    protected _sortingFactory: (m: ViewMetadataSealed | null) => IViewSorting;
    protected _observers: IViewContext[];
    protected _content: Map<string, IBaseContentItem>;
    protected _contentByArea: Map<number, IBaseContentItem[]>;
    protected _parametersNeedDispose: boolean;
    protected _existedLinks: Map<any, RequestParameter>;
    protected _reactRef: ReactRef | null;
    protected _tableVisibleColumnOrderingFunc: (() => ReadonlyArray<string>) | null;
    private _defferedRefreshCallback;
    get children(): ReadonlyArray<IViewContext>;
    get id(): guid;
    get parentContext(): IViewContext | null;
    get view(): ITessaView | null;
    readonly workplace: IWorkplaceViewModel;
    readonly uiId: string;
    readonly extensionExecutor: IExtensionExecutor;
    canAutoFocus: boolean;
    get content(): ReadonlyMap<string, IBaseContentItem>;
    get contentByArea(): ReadonlyMap<number, IBaseContentItem[]>;
    readonly contentFactories: Map<string, (c: IWorkplaceViewComponent) => IBaseContentItem>;
    get dataNodeMetadata(): DataNodeMetadataSealed;
    get isCounterAvailable(): boolean;
    get rowCounterVisible(): boolean;
    get viewContext(): IViewContext;
    readonly contextMenuGenerators: Array<(ctx: IViewContextMenuContext) => void>;
    calculateMaxHeight: boolean;
    buildContent: BuildContentDelegate;
    convertParameterValue: ConvertParameterValueDelegate;
    dataColumnConverter: DataColumnConverterDelegate;
    dataColumnsConverter: DataColumnsConverterDelegate;
    dataRowConverter: DataRowConverterDelegate;
    dataRowsConverter: DataRowsConverterDelegate;
    getData: GetDataDelegate;
    getPageLimit: GetPageLimitDelegate;
    getPagingMode: GetPagingModeDelegate;
    getRequest: GetRequestDelegate;
    getView: GetViewDelegate;
    getViewMetadata: GetViewMetadataDelegate;
    getWorkplaceViewMetadata: GetWorkplaceViewMetadataDelegate;
    setRequestPagingParameters: SetRequestPagingParametersDelegate;
    selectAction: SelectFromViewDelegate | null;
    refreshView(): Promise<void>;
    canRefreshView(): boolean;
    filterView(): Promise<void>;
    canFilterView(): boolean;
    clearFilterView(): void;
    canClearFilterView(): boolean;
    getSortedColumns(): ReadonlyArray<SortingColumn>;
    sortColumn(column: string, addOrInverse: boolean, descendingByDefault: boolean): void;
    getTableVisibleColumnOrdering(): ReadonlyArray<string>;
    inCellSelectionMode(): boolean;
    inSelectionMode(): boolean;
    initialize(): void;
    dispose(): void;
    refresh(): Promise<void>;
    protected initializeFields(): void;
    protected initializeSelection(): void;
    protected initializeParameters(): void;
    protected initializeSorting(): void;
    protected initializeContent(): void;
    protected updateParameters(): void;
    protected updateParameterLinkValues(viewContext: IViewContext | null, parameters: IViewParameters | null, resultParameters: IViewParameters): void;
    protected updateColumnLinkValues(viewContext: IViewContext | null, resultParameters: IViewParameters): void;
    protected getLinkedColumnInfo(viewContext: IViewContext, columnSource: ParameterSourceSealed, param: ViewParameterMetadataSealed, link: WorkplaceViewParameterLinkMetadataSealed): [any, string];
    attach(observer: IViewContext): void;
    detach(observer: IViewContext): void;
    setReactRef<R = any>(ref: React.RefObject<R> | null): void;
    getReactRef(): ReactRef | null;
    setTableVisibleColumnOrdering(func: () => ReadonlyArray<string>): void;
    protected getViewData(): Promise<{
        columns: ReadonlyMap<string, SchemeDbType>;
        rows: ReadonlyArray<ReadonlyMap<string, any>>;
        rowCount: number;
        tags: Map<guid, IGridRowTagViewModel[]>;
    } | null>;
    protected getSetupRequestActions(pageNumber: number): Array<(c: IWorkplaceViewComponent, r: ITessaViewRequest) => void>;
}
