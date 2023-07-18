import { ICardModel } from '../interfaces';
import { ITessaView } from 'tessa/views';
import { ViewParameterMetadataSealed, RequestParameter, ViewMetadataSealed, RequestCriteria } from 'tessa/views/metadata';
import { IStorage } from 'tessa/platform/storage';
import { IViewParameters } from 'tessa/ui/views/parameters';
export declare enum ViewMapColumnType {
    CardColumn = 0,
    CardID = 1,
    CardType = 2,
    CardTypeAlias = 3,
    CurrentUser = 4,
    ConstantValue = 5,
    ViewMasterLink = 6
}
export declare function getViewMapColumnTypeCaption(columnType: ViewMapColumnType): string;
export declare function addRequestParameters(viewMapping: any[] | null, model: ICardModel, view?: ITessaView | null): RequestParameter[] | null;
export interface IParametersMappingContext {
    masterContext?: IParametersMappingContext | null;
    cardModel?: ICardModel | null;
    parameters?: IViewParameters | null;
    selectedColumnName?: string | null;
    selectedRowData?: ReadonlyMap<string, any> | null;
    viewMetadata?: ViewMetadataSealed | null;
}
export declare abstract class ParameterProvider {
    provideParameters(parameters: IViewParameters, mappingData: IStorage, mappingContext: IParametersMappingContext): void;
    protected getOrCreateParameterMetadata(parametersMetadata: ReadonlyArray<ViewParameterMetadataSealed> | null | undefined, mapping: IStorage): ViewParameterMetadataSealed;
    protected getValue(_mappingData: IStorage, _mappingContext: IParametersMappingContext): any | null;
    protected provideParametersOverride(parameters: IViewParameters, mappingData: IStorage, mappingContext: IParametersMappingContext): void;
    protected getText(value: any, mappingContext: IParametersMappingContext): string;
    protected getTextOverride(value: any, _mappingContext: IParametersMappingContext): string;
    protected removeParameters(parameters: IViewParameters, parameterMetadata: ViewParameterMetadataSealed): void;
    protected tryMapCriteria(mappingContext: IParametersMappingContext, mappingData: IStorage, value: unknown): RequestCriteria | null;
}
export declare class CardColumnParameterProvider extends ParameterProvider {
    protected getValue(mappingData: IStorage, mappingContext: IParametersMappingContext): any | null;
}
export declare class CardIdParameterProvider extends ParameterProvider {
    protected getValue(_mappingData: IStorage, mappingContext: IParametersMappingContext): any | null;
    protected getTextOverride(value: any, mappingContext: IParametersMappingContext): string;
}
export declare class CardTypeIDParameterProvider extends ParameterProvider {
    protected getValue(_mappingData: IStorage, mappingContext: IParametersMappingContext): any | null;
}
export declare class CardTypeNameProvider extends ParameterProvider {
    protected getValue(_mappingData: IStorage, mappingContext: IParametersMappingContext): any | null;
}
export declare class CurrentUserParameterProvider extends ParameterProvider {
    protected getValue(_mappingData: IStorage, _mappingContext: IParametersMappingContext): any | null;
}
export declare class ConstantParameterProvider extends ParameterProvider {
    protected getValue(mappingData: IStorage, _mappingContext: IParametersMappingContext): any | null;
}
export declare class ViewMasterLinkParameterProvider extends ParameterProvider {
    protected provideParametersOverride(parameters: IViewParameters, mappingData: IStorage, mappingContext: IParametersMappingContext): void;
    protected provideMasterColumnLinkParameter(resultParameters: IViewParameters, mappingData: IStorage, mappingContext: IParametersMappingContext): void;
    protected updateParameterLinkValues(resultParameters: IViewParameters, mappingData: IStorage, mappingContext: IParametersMappingContext): void;
    protected cloneParameter(param: RequestParameter, parameterMetadata: ViewParameterMetadataSealed): RequestParameter;
    protected getValue(mappingData: IStorage, mappingContext: IParametersMappingContext): any | null;
    protected getParameter(name: string, masterParameters: IViewParameters | null | undefined): RequestParameter | null | undefined;
    protected defaultConvertParameterValue(value: any, _metadata: ViewParameterMetadataSealed): any;
}
export declare class CardViewMapperFactory {
    static createParametersProvider(columnType: ViewMapColumnType): CardColumnParameterProvider | CardIdParameterProvider | CardTypeIDParameterProvider | CardTypeNameProvider | CurrentUserParameterProvider | ConstantParameterProvider | ViewMasterLinkParameterProvider;
}
