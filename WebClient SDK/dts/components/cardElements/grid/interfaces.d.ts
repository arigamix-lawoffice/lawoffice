import { ClassNameList, MenuAction } from 'tessa/ui';
import React, { SyntheticEvent, KeyboardEvent, CSSProperties } from 'react';
import { Visibility } from 'tessa/platform';
import { GridColumnDisplayType, GridMouseEventHandler, HeaderDropHandler } from './common';
import { CellTag } from './cellTag';
import { ColumnSortDirection } from 'tessa/ui/cards/controls/columnSortDirection';
export interface IGridViewBag {
    [key: string]: any;
}
export interface IGridState {
    layout: IGridInternalLayout;
    triggers: IGridTriggers;
}
export declare type GridResizeHandler = (metrics: Readonly<IGridMetrics>, layout: Readonly<IGridLayout>, columnInfo: Readonly<IGridColumnInfo>) => void;
export declare type GridLayoutChangeHandler = (layout: Readonly<IGridLayout>, oldLayout: Readonly<IGridLayout>, metrics: Readonly<IGridMetrics>) => void;
export interface IGridProps {
    blocks?: ReadonlyArray<IGridBlockViewModel>;
    columns: ReadonlyArray<IGridColumnViewModel>;
    rows: ReadonlyArray<IGridRowViewModel>;
    autohideOverflow?: boolean;
    autohideOrder?: string[];
    autoWrap?: boolean;
    canSelectMultipleItems?: boolean;
    canReorderHeaders?: boolean;
    cellSelectionMode?: boolean;
    className?: string;
    columnOrder?: ReadonlyArray<string>;
    measureCyclingProtectionLookback?: number;
    measureCyclingProtectionStep?: number;
    layouts?: IGridLayout[];
    multiSelectEnabled?: boolean;
    noDataText?: string | null;
    onBlur?: () => void;
    onFocus?: () => void;
    onKeyDown?: (event: React.KeyboardEvent) => void;
    onMouseClick?: GridMouseEventHandler;
    onMouseDoubleClick?: GridMouseEventHandler;
    onMouseDown?: GridMouseEventHandler;
    onHeaderDrop?: HeaderDropHandler;
    onResize?: GridResizeHandler;
    onLayoutChange?: GridLayoutChangeHandler;
    saveTableHeightWhenEmpty?: boolean;
    selectRowOnContextMenu?: boolean;
    showHorizontalScrollbar?: boolean;
    style?: CSSProperties;
    tabIndex?: number;
    trackColumnWidth?: boolean;
    viewBag?: IGridViewBag;
    containerRef?: React.RefObject<HTMLElement>;
    tagsPosition?: 'before' | 'after' | 'override';
}
export interface IGridObservationData {
    columnWidths: Map<string, number>;
    unwrappedColumnWidths: Map<string, number>;
    containerWidth: number;
    containerHeight: number;
}
export interface IGridMetrics {
    columnWidths: Map<string, number>;
    containerWidth: number;
    containerHeight: number;
    overflowWidth: number;
    permanentColumnWidths: Map<string, number>;
    unwrappedColumnWidths: Map<string, number>;
    wrappedWidth: number;
}
export interface IGridColumnInfo {
    autoWrap: boolean;
    columnOrder: ReadonlyArray<string>;
    hasUnavoidableOverflow: boolean;
    overflownColumns: ReadonlyArray<string>;
    normalColumns: ReadonlyArray<string>;
    inlineTopColumns: ReadonlyArray<string>;
    inlineBottomColumns: ReadonlyArray<string>;
}
export interface IGridTriggers {
    measure: Record<string, unknown>;
    focusOptions?: FocusOptions;
}
export interface IGridHandlers {
    onClick: GridMouseEventHandler;
    onDoubleClick: GridMouseEventHandler;
    onBlockContextMenu: (e: React.SyntheticEvent, block: IGridBlockViewModel) => void;
    onCellContextMenu: (e: React.SyntheticEvent, cell: IGridCellViewModel) => void;
    onColumnContextMenu: (e: React.SyntheticEvent, column: IGridColumnViewModel) => void;
    onHeaderDragStart: (event: React.DragEvent<HTMLElement>, header: IGridColumnViewModel) => void;
    onHeaderDragOver: (event: React.DragEvent<HTMLElement>) => void;
    onHeaderDragEnd: (event: React.DragEvent<HTMLElement>) => void;
    onHeaderDragDrop: (e: React.DragEvent<HTMLElement>, header: IGridColumnViewModel) => void;
    onKeyDown: (event: KeyboardEvent, columnInfo: IGridColumnInfo) => void;
    onMouseDown: GridMouseEventHandler;
    onDoubleMouseDown: (e: React.SyntheticEvent, cell: IGridCellViewModel) => void;
    onRowSelect: (e: React.SyntheticEvent | React.ChangeEvent, row: IGridRowViewModel) => void;
    onRowContextMenu: (e: React.SyntheticEvent, row: IGridRowViewModel) => void;
    onRowActivate: (row: IGridRowViewModel, getRowBoundingClientRect: () => DOMRect | null) => void;
    onRowTagDropdownContextMenu: (e: React.SyntheticEvent, tags: IGridRowTagViewModel[]) => void;
}
export interface IGridStylableComponent {
    className?: ClassNameList;
    style?: React.CSSProperties;
}
export interface IGridColumnViewModel extends IGridStylableComponent {
    canSort?: boolean;
    displayType: GridColumnDisplayType;
    caption: string;
    id: string;
    getContextMenu?: (event: SyntheticEvent) => ReadonlyArray<MenuAction>;
    isPermanent?: boolean;
    onClick?: (e: React.SyntheticEvent) => void;
    onDoubleClick?: (e: React.SyntheticEvent) => void;
    onMouseDown?: (e: React.SyntheticEvent) => void;
    sortDirection?: ColumnSortDirection | null;
    tooltip?: string;
    visibility: Visibility;
    collapseInlineHeader?: boolean;
    disableSelection?: boolean;
}
export declare function isGridColumnViewModel(vm: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel): vm is IGridColumnViewModel;
export interface IGridBlockViewModel extends IGridStylableComponent {
    caption: string;
    count?: number;
    id: string;
    getContextMenu?: (event: SyntheticEvent) => ReadonlyArray<MenuAction>;
    showChildren: boolean;
    onClick?: (e: React.SyntheticEvent) => void;
    onDoubleClick?: (e: React.SyntheticEvent) => void;
    onMouseDown?: (e: React.SyntheticEvent) => void;
    parentId: string | null;
}
export declare function isGridBlockViewModel(vm: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel): vm is IGridBlockViewModel;
export interface IGridRowViewModel extends IGridStylableComponent {
    cells: ReadonlyArray<IGridCellViewModel>;
    getContextMenu?: (e: React.SyntheticEvent | React.KeyboardEvent, columnId?: string) => ReadonlyArray<MenuAction>;
    id: string;
    isLastSelected: boolean;
    showChildren: boolean;
    showOverflow: boolean;
    isSelected: boolean;
    onClick?: (e: React.SyntheticEvent | React.KeyboardEvent, columnIndex?: number) => void;
    onDoubleClick?: (e: React.SyntheticEvent | React.KeyboardEvent, columnIndex?: number) => void;
    onMouseDown?: (e: React.SyntheticEvent) => void;
    parentId?: string | null;
    toolTip?: string;
    tags?: IGridRowTagViewModel[];
}
export declare function isGridRowViewModel(vm: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel): vm is IGridRowViewModel;
export interface IGridCellViewModel extends IGridStylableComponent {
    columnId: string;
    content: string | ((ctx: GridCellContext) => unknown);
    getContextMenu?: (event: SyntheticEvent) => ReadonlyArray<MenuAction>;
    isSelected?: boolean;
    isLastSelected?: boolean;
    parent: IGridRowViewModel;
    leftTags?: CellTag[];
    rightTags?: CellTag[];
    onClick?: (e: React.SyntheticEvent) => void;
    onDoubleClick?: (e: React.SyntheticEvent | React.KeyboardEvent) => void;
    onMouseDown?: (e: React.SyntheticEvent) => void;
    tooltip?: string;
}
export interface GridCellContext {
    gridProps: Readonly<Omit<IGridProps, 'onBlur' | 'onFocus' | 'onKeyDown' | 'onMouseClick' | 'onMouseDoubleClick' | 'onMouseDown' | 'onHeaderDrop' | 'onResize' | 'onLayoutChange'>>;
    value?: unknown;
    convertedValue?: unknown;
}
export declare function isGridCellViewModel(vm: IGridColumnViewModel | IGridBlockViewModel | IGridRowViewModel | IGridCellViewModel): vm is IGridCellViewModel;
export interface IGridTableProps {
    gridProps: IGridProps;
    handlers: IGridHandlers;
    layout: IGridInternalLayout;
    onResize: (metrics: IGridMetrics, columnInfo: IGridColumnInfo, layout: IGridLayout) => void;
    triggers: IGridTriggers;
}
export interface IGridComponentProps {
    className?: ClassNameList;
    columnInfo: IGridColumnInfo;
    handlers: IGridHandlers;
    gridProps: IGridProps;
    layout: IGridInternalLayout;
    metrics?: IGridMetrics;
    style?: React.CSSProperties;
    tags?: string[];
}
export interface IGridColumnProps extends IGridComponentProps {
    draggable: boolean;
    column: IGridColumnViewModel;
    contentOverride?: JSX.Element;
    contentPrefix?: JSX.Element;
    contentSuffix?: JSX.Element;
}
export interface IGridBlockProps extends IGridComponentProps {
    block: IGridBlockViewModel;
    depth: number;
}
export interface IGridRowProps extends IGridComponentProps {
    depth: number;
    row: IGridRowViewModel;
}
export interface IGridHeaderRowProps extends IGridComponentProps {
}
export interface IGridCellProps extends IGridComponentProps {
    cell: IGridCellViewModel;
    columnIndex: number;
    contentOverride?: JSX.Element;
    contentPrefix?: JSX.Element;
    contentSuffix?: JSX.Element;
    depth: number;
    disableSelection?: boolean;
}
export interface IGridInternalLayout {
    name: string;
    start: number;
    TableComponent: React.FC<IGridTableProps>;
    ColumnComponent: React.FC<IGridColumnProps>;
    BlockComponent: React.FC<IGridBlockProps>;
    RowComponent: React.FC<IGridRowProps>;
    HeaderRowComponent: React.FC<IGridHeaderRowProps>;
    CellComponent: React.FC<IGridCellProps>;
}
export interface IGridLayout {
    name: string;
    start?: number;
    table?: React.FC<IGridTableProps>;
    column?: React.FC<IGridColumnProps>;
    block?: React.FC<IGridBlockProps>;
    row?: React.FC<IGridRowProps>;
    headerRow?: React.FC<IGridHeaderRowProps>;
    cell?: React.FC<IGridCellProps>;
}
export interface IGridRowTagViewModel {
    id: string;
    name: string;
    caption: string;
    icon?: string;
    columnId?: string;
    tooltip?: string;
    style?: React.CSSProperties;
    associatedObject?: ITaggableObject;
    onClick?: (e: React.MouseEvent) => void;
    getContextMenu?: (event: SyntheticEvent) => MenuAction[];
}
export interface ITaggableObject {
    notifyOnTagUpdated(tag: IGridRowTagViewModel): Promise<void> | void;
}
