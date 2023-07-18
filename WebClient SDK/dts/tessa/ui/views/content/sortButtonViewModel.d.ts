import { IReactionDisposer } from 'mobx';
import { BaseContentItem } from './baseContentItem';
import { ContentPlaceArea } from './contentPlaceArea';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IViewComponentBase } from '../viewComponentBase';
import { SchemeDbType, Visibility } from 'tessa/platform';
import { SortDirection } from 'tessa/views/sortingColumn';
export interface SortButtonColumn {
    alias: string;
    caption: string;
    sortDirection: SortDirection | null;
}
export declare class SortButtonViewModel<T extends IViewComponentBase = IWorkplaceViewComponent> extends BaseContentItem<T> {
    constructor(viewComponent: T, area?: ContentPlaceArea, order?: number);
    protected _toolTip: string;
    protected _visibility: Visibility;
    protected _columnsCache: Map<string, SchemeDbType> | null;
    protected _columns: SortButtonColumn[];
    protected _columnReaction: IReactionDisposer | null;
    get toolTip(): string;
    set toolTip(value: string);
    get visibility(): Visibility;
    set visibility(value: Visibility);
    dropdownLeftDirection: boolean;
    initialize(): void;
    dispose(): void;
    get isLoading(): boolean;
    protected initColumns(): void;
    getSortingColumns(): SortButtonColumn[];
    sortColumn(column: SortButtonColumn, addOrInverse?: boolean, descendingByDefault?: boolean): void;
    protected isColumnsEquals(columns: ReadonlyMap<string, SchemeDbType>): boolean;
}
