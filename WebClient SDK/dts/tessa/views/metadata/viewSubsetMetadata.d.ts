import { SubsetKind } from './subsetKind';
export interface IViewSubsetMetadata {
    alias: string;
    caption: string;
    captionColumn: string;
    countColumn: string;
    hideZeroCount: boolean;
    kind: SubsetKind;
    refColumn: string;
    refParam: string;
    treeHasChildrenColumn: string;
    treeRefParam: string;
    seal<T = ViewSubsetMetadataSealed>(): T;
}
export interface ViewSubsetMetadataSealed {
    readonly alias: string;
    readonly caption: string;
    readonly captionColumn: string;
    readonly countColumn: string;
    readonly hideZeroCount: boolean;
    readonly kind: SubsetKind;
    readonly refColumn: string;
    readonly refParam: string;
    readonly treeHasChildrenColumn: string;
    readonly treeRefParam: string;
    seal<T = ViewSubsetMetadataSealed>(): T;
}
export declare class ViewSubsetMetadata implements IViewSubsetMetadata {
    constructor();
    alias: string;
    caption: string;
    captionColumn: string;
    countColumn: string;
    hideZeroCount: boolean;
    kind: SubsetKind;
    refColumn: string;
    refParam: string;
    treeHasChildrenColumn: string;
    treeRefParam: string;
    seal<T = ViewSubsetMetadataSealed>(): T;
}
