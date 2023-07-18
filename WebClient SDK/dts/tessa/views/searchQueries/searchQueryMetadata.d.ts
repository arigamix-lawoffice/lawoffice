import { ExtensionMetadataSealed, IExtensionMetadata } from '../workplaces/extensionMetadata';
import { IJsonSearchQueryMetadata } from 'tessa/views/searchQueries/jsonSearchQueryMetadata';
import { RequestParameter } from 'tessa/views/metadata';
export interface ISearchQueryMetadata {
    alias: string;
    createdByUserId: guid;
    extensions: Map<string, IExtensionMetadata>;
    id: guid;
    isPublic: boolean;
    items: Array<ISearchQueryMetadata>;
    modificationDateTime: string;
    parameters: Array<RequestParameter>;
    parametersByState: Map<string, Array<RequestParameter>>;
    templateCompositionId: guid;
    formatVersion: number;
    viewAlias: string;
    seal<T = SearchQueryMetadataSealed>(): T;
}
export interface SearchQueryMetadataSealed {
    readonly alias: string;
    readonly createdByUserId: guid;
    readonly extensions: ReadonlyMap<string, ExtensionMetadataSealed>;
    readonly id: guid;
    readonly isPublic: boolean;
    readonly items: ReadonlyArray<SearchQueryMetadataSealed>;
    readonly modificationDateTime: string;
    readonly parameters: ReadonlyArray<RequestParameter>;
    readonly parametersByState: ReadonlyMap<string, ReadonlyArray<RequestParameter>>;
    readonly templateCompositionId: guid;
    readonly formatVersion: number;
    readonly viewAlias: string;
    seal<T = SearchQueryMetadataSealed>(): T;
}
export declare class SearchQueryMetadata implements ISearchQueryMetadata {
    constructor();
    alias: string;
    createdByUserId: guid;
    extensions: Map<string, IExtensionMetadata>;
    id: guid;
    isPublic: boolean;
    items: Array<ISearchQueryMetadata>;
    modificationDateTime: string;
    parameters: Array<RequestParameter>;
    parametersByState: Map<string, Array<RequestParameter>>;
    templateCompositionId: guid;
    formatVersion: number;
    viewAlias: string;
    seal<T = SearchQueryMetadataSealed>(): T;
    getObjectForSerialize(): IJsonSearchQueryMetadata;
}
