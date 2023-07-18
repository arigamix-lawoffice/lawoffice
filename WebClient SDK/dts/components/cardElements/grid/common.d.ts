import React from 'react';
import { ColumnSortDirection } from 'tessa/ui/cards/controls/columnSortDirection';
import { SortDirection } from 'tessa/views';
import { IGridColumnInfo, IGridProps } from '.';
import { IGridBlockViewModel, IGridCellViewModel, IGridColumnViewModel, IGridRowViewModel } from './interfaces';
export declare const depthLevelWidth = 20;
export declare type GridMouseEventHandler = (event: React.MouseEvent, item: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel) => void;
export declare type GridKeyboardEventHandler = (event: React.KeyboardEvent, item: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel) => void;
export declare type GridEventHandler = (event: React.KeyboardEvent | React.MouseEvent, item: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel) => void;
export declare type HeaderDropHandler = (args: {
    source: IGridColumnViewModel;
    target: IGridColumnViewModel;
    sourceIndex: number;
    targetIndex: number;
}) => void;
export declare function convertFromSortDirection(direction: SortDirection | null): ColumnSortDirection;
export declare const maxWordLength = 30;
export declare function compareArrays<T>(a: ReadonlyArray<T>, b: ReadonlyArray<T>): boolean;
export declare function getColumnCount(props: IGridProps, columnInfo: IGridColumnInfo): number;
export declare enum GridColumnDisplayType {
    normal = "normal",
    inlineTop = "top",
    inlineBottom = "bottom"
}
