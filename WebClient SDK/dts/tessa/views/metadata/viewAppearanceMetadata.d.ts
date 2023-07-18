export declare enum AppearanceFontStyle {
    Normal = 0,
    Oblique = 1,
    Italic = 2
}
export interface IViewAppearanceMetadata {
    seal<T = ViewAppearanceMetadataSealed>(): T;
}
export interface ViewAppearanceMetadataSealed {
    seal<T = ViewAppearanceMetadataSealed>(): T;
}
export declare class ViewAppearanceMetadata implements IViewAppearanceMetadata {
    constructor();
    background: string;
    fontFamily: string;
    fontFamilyUri: string;
    fontSize: number | null;
    fontStretch: number | null;
    fontStyle: AppearanceFontStyle | null;
    fontWeight: number | null;
    foreground: string;
    horizontalAlignment: string;
    textAlignment: string;
    toolTip: string;
    verticalAlignment: string;
    seal<T = ViewAppearanceMetadataSealed>(): T;
}
