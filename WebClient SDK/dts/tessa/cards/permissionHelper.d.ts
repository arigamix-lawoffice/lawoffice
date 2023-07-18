import { CardTypeCustomControl, CardTypeEntryControl, CardTypeTableControl } from 'tessa/cards/types';
import { ICardModel } from 'tessa/ui/cards/interfaces';
export declare class PermissionHelper {
    private constructor();
    private static _instance;
    static get instance(): PermissionHelper;
    getReadOnlyCustomControl(model: ICardModel, control: CardTypeCustomControl | null, fieldName: string, sectionName: string): boolean;
    getReadOnlyEntryControl(model: ICardModel, control: CardTypeEntryControl | null, fieldNameOrColumnNames: string | string[], sectionName: string): boolean;
    getReadOnlyTableControl(model: ICardModel, control: CardTypeTableControl | null, sectionName: string): boolean;
    getReadOnlyRowField(model: ICardModel, sectionName: string, fieldName: string, rowId: guid): boolean;
    getProhibitInsertRow(model: ICardModel, sectionName: string): boolean;
    getProhibitDeleteRow(model: ICardModel, sectionName: string, rowId: guid): boolean;
}
