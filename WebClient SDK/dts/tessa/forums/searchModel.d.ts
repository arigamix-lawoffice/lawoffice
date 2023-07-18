import { SearchResult } from './searchResult';
export declare class SearchModel {
    constructor();
    private _searchText;
    private _isSearchModeEnabled;
    private _showBackToResultsButton;
    private _searchResult;
    get searchText(): string | null;
    set searchText(value: string | null);
    get isSearchModeEnabled(): boolean;
    set isSearchModeEnabled(value: boolean);
    get showBackToResultsButton(): boolean;
    set showBackToResultsButton(value: boolean);
    get searchResult(): SearchResult | null;
    set searchResult(value: SearchResult | null);
    clean: () => void;
}
