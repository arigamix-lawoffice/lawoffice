import { SchemeType } from 'tessa/scheme';
export interface IViewColumnMetadata {
    alias: string;
    appearance: string | null;
    caption: string | null;
    treatValueAsUtc: boolean;
    disableGrouping: boolean;
    hidden: boolean;
    localizable: boolean;
    maxLength: number;
    sortBy: string | null;
    schemeType: SchemeType | null;
    seal<T = ViewColumnMetadataSealed>(): T;
}
export interface ViewColumnMetadataSealed {
    readonly alias: string;
    readonly appearance: string | null;
    readonly caption: string | null;
    readonly treatValueAsUtc: boolean;
    readonly disableGrouping: boolean;
    readonly hidden: boolean;
    readonly localizable: boolean;
    readonly maxLength: number;
    readonly sortBy: string | null;
    readonly schemeType: SchemeType | null;
    seal<T = ViewColumnMetadataSealed>(): T;
}
export declare class ViewColumnMetadata implements IViewColumnMetadata {
    constructor();
    alias: string;
    appearance: string | null;
    caption: string | null;
    treatValueAsUtc: boolean;
    disableGrouping: boolean;
    hidden: boolean;
    localizable: boolean;
    maxLength: number;
    sortBy: string | null;
    schemeType: SchemeType | null;
    seal<T = ViewColumnMetadataSealed>(): T;
}
