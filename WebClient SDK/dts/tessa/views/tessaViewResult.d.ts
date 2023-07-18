import { IStorage } from 'tessa/platform/storage';
import { SchemeType } from 'tessa/scheme';
export interface ITessaViewResult {
    columns: string[];
    schemeTypes: SchemeType[];
    result: Map<string, any>;
    rowCount: number;
    rows: any[];
    hasTimeOut: boolean;
    info: IStorage;
    getColumnIndex(columnName: string): number;
}
export declare class TessaViewResult implements ITessaViewResult {
    constructor();
    readonly columnsKey = "columns";
    readonly rowCountKey = "rowcount";
    readonly rowsKey = "rows";
    readonly schemeTypesKey = "schemetypes";
    readonly infoKey = "info";
    private _schemeTypes;
    get columns(): string[];
    set columns(value: string[]);
    hasTimeOut: boolean;
    result: Map<string, any>;
    get rowCount(): number;
    set rowCount(value: number);
    get rows(): any[];
    set rows(value: any[]);
    get schemeTypes(): SchemeType[];
    set schemeTypes(value: SchemeType[]);
    get info(): IStorage;
    set info(value: IStorage);
    getColumnIndex(columnName: string): number;
}
