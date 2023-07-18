/// <reference types="react" />
import { GridColumnDisplayType, IGridColumnViewModel } from 'components/cardElements/grid';
import { Visibility } from 'tessa/platform';
import { ClassNameList, MenuAction } from 'tessa/ui';
import { ColumnSortDirection } from 'tessa/ui/cards/controls/columnSortDirection';
import { ITableColumnViewModel } from 'tessa/ui/views/content';
export declare class TableColumnAdapter implements IGridColumnViewModel {
    constructor(viewModel: ITableColumnViewModel);
    private _viewModel;
    get id(): string;
    get caption(): string;
    get sortDirection(): ColumnSortDirection | null;
    get canSort(): boolean;
    get displayType(): GridColumnDisplayType;
    get collapseInlineHeader(): boolean;
    get tooltip(): string;
    get isPermanent(): boolean;
    get visibility(): Visibility;
    get style(): React.CSSProperties | undefined;
    get className(): ClassNameList;
    get disableSelection(): boolean | undefined;
    onClick: (e: React.MouseEvent) => void;
    onDoubleClick: (e: React.MouseEvent) => void;
    onMouseDown: (e: React.MouseEvent) => void;
    getContextMenu: () => ReadonlyArray<MenuAction>;
}
