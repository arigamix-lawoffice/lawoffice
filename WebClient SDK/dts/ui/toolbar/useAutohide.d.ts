import { IToolbarGroup, IToolbarMetrics, IToolbarContainerProps, IToolbarBreakpointInfo } from './interfaces';
export declare function useAutohide(props: IToolbarContainerProps, metrics?: IToolbarMetrics, breakpointInfo?: IToolbarBreakpointInfo[]): IToolbarGroup[];
export declare function getAutohideOrder(groups: IToolbarGroup[], autohideOrder?: string[]): string[];
export declare function getAutohidePage(groups: IToolbarGroup[], spacing: number, autohideOrder: string[], metrics: IToolbarMetrics): IToolbarGroup[];
