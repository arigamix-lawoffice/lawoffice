import { IWorkplaceViewComponent } from './workplaceViewComponent';
import { PagingContext } from './pagingContext';
import { IBaseContentItem } from './content';
import { DataNodeMetadataSealed } from 'tessa/views/workplaces';
import { ITessaView } from 'tessa/views/tessaView';
import { ViewParameterMetadataSealed, ViewMetadataSealed } from 'tessa/views/metadata';
import { RequestParameter } from 'tessa/views/metadata/requestParameter';
import { SchemeDbType } from 'tessa/platform';
import { ITessaViewRequest, ITessaViewResult, Paging } from 'tessa/views';
import { SchemeType } from 'tessa/scheme';
export interface IWorkplaceViewComponentInitializationStrategy {
    initializeContent(component: IWorkplaceViewComponent, metadata: DataNodeMetadataSealed): any;
    initializeHandlers(component: IWorkplaceViewComponent, metadata: DataNodeMetadataSealed, view: ITessaView): any;
}
export declare class WorkplaceViewComponentInitializationStrategy implements IWorkplaceViewComponentInitializationStrategy {
    constructor();
    initializeContent(component: IWorkplaceViewComponent, _metadata: DataNodeMetadataSealed): void;
    initializeHandlers(component: IWorkplaceViewComponent, metadata: DataNodeMetadataSealed, view: ITessaView): void;
    protected static defaultBuildContent(model: IWorkplaceViewComponent, content: Map<string, IBaseContentItem>, contentFactories: Map<string, (c: IWorkplaceViewComponent) => IBaseContentItem | null>): void;
    protected static defaultConvertParameterValue(_model: IWorkplaceViewComponent, value: any, _metadata: ViewParameterMetadataSealed): any;
    protected static defaultDataColumnConverter(_model: IWorkplaceViewComponent, columnName: string, columnDataType: SchemeDbType): [string, SchemeDbType];
    protected static defaultDataColumnsConverter(model: IWorkplaceViewComponent, columnNames: string[], columnDataTypes: SchemeType[]): [string, SchemeDbType][];
    protected static defaultDataRowConverter(_model: IWorkplaceViewComponent, columns: Map<string, SchemeDbType>, data: any | null): Map<string, any>;
    protected static defaultDataRowsConverter(model: IWorkplaceViewComponent, columns: Map<string, SchemeDbType>, rows: any[]): Map<string, any>[];
    protected static defaultGetData(component: IWorkplaceViewComponent, request: ITessaViewRequest | null): Promise<ITessaViewResult | null>;
    protected static defaultGetPageLimit(model: IWorkplaceViewComponent): number;
    protected static defaultGetPagingMode(model: IWorkplaceViewComponent): Paging;
    protected static defaultGetRequest(component: IWorkplaceViewComponent, parameters: RequestParameter[], setupActions: Array<(c: IWorkplaceViewComponent, r: ITessaViewRequest) => void>): ITessaViewRequest | null;
    protected static defaultGetViewMetadata(model: IWorkplaceViewComponent): ViewMetadataSealed | null;
    protected static defaultSetRequestPagingParameters(_model: IWorkplaceViewComponent, parameters: RequestParameter[], pagingContext: PagingContext): void;
}
