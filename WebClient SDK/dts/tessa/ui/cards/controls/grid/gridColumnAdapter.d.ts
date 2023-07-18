import { GridColumnDisplayType, IGridColumnViewModel } from 'components/cardElements/grid';
import React from 'react';
import { Visibility } from 'tessa/platform/visibility';
import { ClassNameList } from 'tessa/ui';
import { GridColumnViewModel, GridViewModel } from 'tessa/ui/cards/controls';
import { ColumnSortDirection } from 'tessa/ui/cards/controls/columnSortDirection';
export declare class GridColumnAdapter implements IGridColumnViewModel {
    constructor(viewModel: GridColumnViewModel, control: GridViewModel);
    private _viewModel;
    private _control;
    get id(): string;
    get caption(): string;
    get sortDirection(): ColumnSortDirection;
    get canSort(): boolean;
    get tooltip(): string;
    get isPermanent(): boolean;
    get style(): React.CSSProperties | undefined;
    get className(): ClassNameList;
    get visibility(): Visibility;
    get displayType(): GridColumnDisplayType;
    onClick(e: React.MouseEvent): void;
    private getIndex;
}
