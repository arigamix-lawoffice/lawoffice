import { SearchQueryMetadataSealed, ISearchQueryMetadata } from './searchQueries';
import { ValidationResult } from 'tessa/platform/validation';
export interface ISearchQueryService {
    allSearchQueries: ReadonlyArray<SearchQueryMetadataSealed>;
    getById(id: guid): SearchQueryMetadataSealed | null;
    initializeSearchQueries(searchQueries: ISearchQueryMetadata[] | SearchQueryMetadataSealed[]): any;
    save(metadata: ISearchQueryMetadata | SearchQueryMetadataSealed): Promise<ValidationResult | null>;
    delete(ids: guid[]): Promise<ValidationResult | null>;
    import(metadata: ISearchQueryMetadata[]): any;
}
export declare class SearchQueryService implements ISearchQueryService {
    private constructor();
    private static _instance;
    static get instance(): ISearchQueryService;
    private _allSearchQueries;
    get allSearchQueries(): SearchQueryMetadataSealed[];
    getById(id: guid): SearchQueryMetadataSealed | null;
    initializeSearchQueries(searchQueries: ISearchQueryMetadata[] | SearchQueryMetadataSealed[]): void;
    save(metadata: ISearchQueryMetadata | SearchQueryMetadataSealed): Promise<ValidationResult | null>;
    delete(ids: guid[]): Promise<ValidationResult | null>;
    import(metadata: ISearchQueryMetadata[]): Promise<ValidationResult | null>;
}
