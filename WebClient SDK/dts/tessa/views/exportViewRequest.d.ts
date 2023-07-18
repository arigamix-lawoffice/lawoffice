import { ViewPlaceholderInfo } from 'tessa/platform/placeholders';
export declare class ExportViewRequest {
    constructor();
    private _viewAlias;
    private _exportAll;
    private _title;
    get viewAlias(): string;
    set viewAlias(value: string);
    get exportAll(): boolean;
    set exportAll(value: boolean);
    get title(): string;
    set title(value: string);
    columnsOrdering: string[];
    placeholderInfo: ViewPlaceholderInfo;
}
