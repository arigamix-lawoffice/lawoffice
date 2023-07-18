/// <reference types="react" />
import { ToolbarItemsAlignment, ToolbarViewOrientation, ToolbarOverflowHandling, ToolbarGroupDisplayType, ToolbarGroupSpreadMode, ToolbarPagingControls } from './common';
export interface IToolbarContainerProps {
    autohideOrder?: string[];
    groups: IToolbarGroup[];
    breakpoints?: IToolbarBreakpoint[];
    items: JSX.Element[];
    menuItems?: JSX.Element[];
    overflow?: ToolbarOverflowHandling;
    spacing?: number;
    padding?: number;
    display?: ToolbarGroupDisplayType;
    monoGroupKey?: string;
    spreadProps?: IToolbarSpreadProps;
    autocloseSpread?: boolean;
    onResize?: (metrics: IToolbarMetrics) => void;
    pagingControls?: ToolbarPagingControls;
    className?: string;
}
export interface IToolbarProps extends IToolbarContainerProps {
}
export interface IToolbarGroup {
    align?: ToolbarItemsAlignment;
    id: string;
    items: string[];
    isExpanded?: boolean;
    isPermanent?: boolean;
    isOverlay?: boolean;
    groupInSpread?: boolean;
    spreadMode?: ToolbarGroupSpreadMode;
    stretch?: boolean;
    minLength?: number;
    orientation?: ToolbarViewOrientation;
    spacing?: number;
    className?: string;
}
export interface IToolbarBreakpoint {
    minLength?: string;
    groups: string[];
}
export interface IToolbarBreakpointInfo {
    length: number;
    breakpoint: IToolbarBreakpoint;
    isActive: boolean;
}
export interface IToolbarGroupProps {
    group: IToolbarGroup;
    display?: ToolbarGroupDisplayType;
}
export interface IToolbarGroupTheme {
    spacing: number;
}
export interface IToolbarObservationData {
    itemMetrics: Map<string, IToolbarItemMetrics>;
    internalItemMetrics: Map<string, IToolbarItemMetrics>;
    containerLength: number;
    containerCrossLength: number;
}
export interface IToolbarItemMetrics {
    length: number;
    crossLength: number;
    position: number;
}
export interface IToolbarGroupMetrics {
    length: number;
    crossLength: number;
    position: number;
}
export interface IToolbarMetrics {
    groupMetrics: Map<string, IToolbarGroupMetrics>;
    internalItemMetrics: Map<string, IToolbarItemMetrics>;
    containerLength: number;
    containerCrossLength: number;
    wrappedLength: number;
}
export interface IToolbarPagingNavigation {
    selectedPage: number;
    prevButton?: JSX.Element;
    nextButton?: JSX.Element;
    markers?: JSX.Element[];
    handleTouchStart: (e: React.TouchEvent) => void;
    handleTouchMove: (e: React.TouchEvent) => void;
}
export interface IToolbarLayout {
    group?: () => JSX.Element;
    splitter?: () => JSX.Element;
    wrapper?: () => JSX.Element;
}
export interface IToolbarVisibilityInfo {
    visible: string[];
    hidden: string[];
}
export interface IToolbarTheme {
    spacing: number;
    padding: number;
}
export interface IToolbarSpreadProps {
    icon?: string;
    className?: string;
    style?: React.CSSProperties;
    popover?: (children: JSX.Element[], isOpen: boolean, onOutsideClick: () => void, root: React.ReactInstance | null | undefined) => JSX.Element;
}
export interface IToolbarItemIdMapper {
    clear: () => void;
    set: (group: IToolbarGroup, itemId: string) => void;
    get: (node: Node) => string | undefined;
}
