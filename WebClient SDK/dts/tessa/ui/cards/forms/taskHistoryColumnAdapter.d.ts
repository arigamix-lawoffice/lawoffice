import { GridColumnDisplayType, IGridColumnViewModel } from 'components/cardElements/grid';
import React from 'react';
import { Visibility } from 'tessa/platform/visibility';
import { ColumnSortDirection } from 'tessa/ui/cards/controls/columnSortDirection';
import { TaskHistoryColumnViewModel } from '../tasks';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class TaskHistoryColumnAdapter implements IGridColumnViewModel {
    constructor(viewModel: TaskHistoryColumnViewModel, setSortingColumn: (id: string, altKey: boolean) => void, sortDirection: ColumnSortDirection);
    private _viewModel;
    private _setSortingColumn;
    private _sortDirection;
    get id(): string;
    get caption(): string;
    get displayType(): GridColumnDisplayType;
    get sortDirection(): ColumnSortDirection;
    set sortDirection(value: ColumnSortDirection);
    get canSort(): boolean;
    get tooltip(): string;
    onClick(e: React.MouseEvent): void;
    get style(): React.CSSProperties | undefined;
    get className(): ClassNameList;
    get visibility(): Visibility;
    get isPermanent(): boolean | undefined;
}
