import { Visibility } from 'tessa/platform';
import { IFilterableGrid, ViewControlQuickSearchViewModel } from 'tessa/ui/cards/controls';
export declare class TaskHistoryViewSearchViewModel extends ViewControlQuickSearchViewModel implements IFilterableGrid {
    private _visibility;
    private _refreshDispose;
    private _cellsCache;
    private _rowsCache;
    get visibility(): Visibility;
    set visibility(value: Visibility);
    initialize(): void;
    dispose(): void;
    canSetSearchText(): boolean;
    setSearchText(text: string): void;
    private restoreAll;
    private processSearchRows;
}
