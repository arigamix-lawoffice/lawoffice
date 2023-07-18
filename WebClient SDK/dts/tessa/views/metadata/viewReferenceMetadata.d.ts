export interface IViewReferenceMetadata {
    cardType: string | null;
    cardTypeColumn: string | null;
    colPrefix: string | null;
    displayValueColumn: string | null;
    isCard: boolean;
    openOnDoubleClick: boolean;
    refSection: string[] | null;
    seal<T = ViewReferenceMetadataSealed>(): T;
}
export interface ViewReferenceMetadataSealed {
    readonly cardType: string | null;
    readonly cardTypeColumn: string | null;
    readonly colPrefix: string | null;
    readonly displayValueColumn: string | null;
    readonly isCard: boolean;
    readonly openOnDoubleClick: boolean;
    readonly refSection: ReadonlyArray<string> | null;
    seal<T = ViewReferenceMetadataSealed>(): T;
}
export declare class ViewReferenceMetadata implements IViewReferenceMetadata {
    constructor();
    cardType: string | null;
    cardTypeColumn: string | null;
    colPrefix: string | null;
    displayValueColumn: string | null;
    isCard: boolean;
    openOnDoubleClick: boolean;
    refSection: string[] | null;
    seal<T = ViewReferenceMetadataSealed>(): T;
}
