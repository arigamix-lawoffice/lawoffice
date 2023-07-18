import { IStorage } from 'tessa/platform/storage';
import { ViewMapColumnType } from '../viewMappingHelper';
export declare enum ConstantValueType {
    String = 0,
    Integer = 1,
    Double = 2,
    Decimal = 3,
    Boolean = 4,
    Date = 5,
    Guid = 6
}
export declare enum ViewMasterLinkType {
    Column = 0,
    Parameter = 1
}
export declare class ViewMappingSettings {
    private _data;
    constructor(_data: IStorage);
    get column(): guid | null;
    set column(value: guid | null);
    get columnType(): ViewMapColumnType;
    set columnType(value: ViewMapColumnType);
    get constantValue(): unknown | null;
    set constantValue(value: unknown | null);
    get constantValueType(): ConstantValueType | null;
    set constantValueType(value: ConstantValueType | null);
    get skipNullParams(): boolean;
    set skipNullParams(value: boolean);
    get viewMasterLinkName(): string;
    set viewMasterLinkName(value: string);
    get viewMasterLinkType(): ViewMasterLinkType;
    set viewMasterLinkType(value: ViewMasterLinkType);
    get viewParamName(): string;
    set viewParamName(value: string);
    get viewParamSetName(): string;
    set viewParamSetName(value: string);
    get section(): guid | null;
    set section(value: guid | null);
}
