export interface DropDownInfoSealed {
    readonly popupColumns: ReadonlyArray<number> | null;
    readonly viewAlias: string;
    readonly refPrefix: string;
    seal<T = DropDownInfoSealed>(): T;
}
export declare class DropDownInfo {
    constructor();
    popupColumns: number[] | null;
    viewAlias: string;
    refPrefix: string;
    seal<T = DropDownInfoSealed>(): T;
}
