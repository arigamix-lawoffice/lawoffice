import { IUIContext } from '../uiContext';
import { ViewColumnMetadataSealed, ViewReferenceMetadataSealed, ViewMetadataSealed } from 'tessa/views/metadata';
import { SchemeDbType } from 'tessa/platform';
export declare class DoubleClickInfo {
    column: ViewColumnMetadataSealed | null;
    columnName: string | null;
    context: IUIContext;
    reference: ViewReferenceMetadataSealed | null;
    selectedObject: ReadonlyMap<string, any> | null;
    sender: any;
    view: ViewMetadataSealed | null;
    columns: ReadonlyMap<string, SchemeDbType> | null;
}
export interface DoubleClickAction {
    (info: DoubleClickInfo): Promise<void>;
}
export declare function openCardDoubleClickAction(info: DoubleClickInfo, action: (cardId: guid, displayValue: string, context: IUIContext, info: DoubleClickInfo) => Promise<void>): Promise<void>;
export declare function openCardIntegerDoubleClickAction(info: DoubleClickInfo, action: (cardId: number, displayValue: string, context: IUIContext, info: DoubleClickInfo) => Promise<void>): Promise<void>;
