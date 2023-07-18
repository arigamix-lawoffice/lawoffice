import { SchemeType } from 'tessa/scheme/schemeType';
export interface IAutoCompletePopupItem {
    fields: any[];
    displayFields: string[];
    schemeTypes: SchemeType[];
    columns: string[];
}
export declare class AutoCompletePopupItem implements IAutoCompletePopupItem {
    constructor(fields: any[], schemeTypes?: SchemeType[], columns?: string[]);
    fields: any[];
    displayFields: string[];
    schemeTypes: SchemeType[];
    columns: string[];
    private generateDisplayFields;
}
