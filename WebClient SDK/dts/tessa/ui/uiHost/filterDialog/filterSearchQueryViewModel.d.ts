import { SearchQueryMetadataSealed } from 'tessa/views/searchQueries';
export declare class FilterSearchQueryViewModel {
    constructor(viewAlias: string, currentQueryChanged: (query: SearchQueryMetadataSealed | null) => void);
    private _viewAlias;
    private _queries;
    private _publicQueries;
    private _userQueries;
    private _isPublicQueriesEnabled;
    private _isUserQueriesEnabled;
    private _isPublicQueriesOpen;
    private _isUserQueriesOpen;
    private _currentQuery;
    private _currentQueryChanged;
    get isPublicQueriesEnabled(): boolean;
    set isPublicQueriesEnabled(value: boolean);
    get isUserQueriesEnabled(): boolean;
    set isUserQueriesEnabled(value: boolean);
    get isPublicQueriesOpen(): boolean;
    set isPublicQueriesOpen(value: boolean);
    get isUserQueriesOpen(): boolean;
    set isUserQueriesOpen(value: boolean);
    get publicQueries(): SearchQueryMetadataSealed[];
    get userQueries(): SearchQueryMetadataSealed[];
    get currentQuery(): SearchQueryMetadataSealed | null;
    set currentQuery(value: SearchQueryMetadataSealed | null);
    refresh(): void;
    private getPublicQueries;
    private getUserQueries;
}
