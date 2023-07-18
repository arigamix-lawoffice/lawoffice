import React from 'react';
import { PopoverInternalProps } from './popoverInternal';
declare class Popover extends React.Component<PopoverProps, PopoverState> {
    private readonly _el;
    private _unmounted;
    constructor(props: PopoverProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element;
}
export interface PopoverState {
    mounted: boolean;
}
export interface PopoverProps extends PopoverInternalProps {
    isOpened?: boolean;
    instantUnmount?: boolean;
    cssTransitionEnabled?: boolean;
    id?: string;
}
declare const _default: React.MemoExoticComponent<typeof Popover>;
export default _default;
