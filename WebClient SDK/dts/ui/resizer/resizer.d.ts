import React from 'react';
declare const defaultProps: {
    vertical: boolean;
    size: number;
    throttle: number;
};
declare type ResizerProps = typeof defaultProps & {
    style?: React.CSSProperties;
    className?: string;
    onResizeStart?: () => void;
    onResize?: (diff: number) => boolean;
    onResizeEnd?: () => void;
};
declare type ResizerStyleProps = {
    vertical: boolean;
    size: number;
};
export declare const ResizerStyle: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<React.HTMLAttributes<HTMLDivElement> & ResizerStyleProps & React.RefAttributes<HTMLDivElement>>, any, ResizerStyleProps, never>;
export declare class Resizer extends React.Component<ResizerProps> {
    static readonly defaultProps: {
        vertical: boolean;
        size: number;
        throttle: number;
    };
    private _moveStartPos;
    private _moveThrottleTimeout;
    private _moveThrottlePos;
    componentWillUnmount(): void;
    render(): JSX.Element;
    private _addEventHandlers;
    private _removeEventHandlers;
    private _clearMoveThrottle;
    private _handleMouseDown;
    private _handleMouseUp;
    private _handleMouseMove;
    private _resize;
}
export {};
