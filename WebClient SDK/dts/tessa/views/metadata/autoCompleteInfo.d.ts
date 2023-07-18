export interface AutoCompleteInfoSealed {
    readonly popupColumns: ReadonlyArray<number> | null;
    readonly viewAlias: string;
    readonly refPrefix: string;
    readonly paramAlias: string;
    seal<T = AutoCompleteInfoSealed>(): T;
}
export declare class AutoCompleteInfo {
    constructor();
    popupColumns: number[] | null;
    viewAlias: string;
    refPrefix: string;
    paramAlias: string;
    seal<T = AutoCompleteInfoSealed>(): T;
}
