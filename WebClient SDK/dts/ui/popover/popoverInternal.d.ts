import * as React from 'react';
import * as Helpers from 'common/utility';
declare class PopoverInternal extends React.Component<PopoverInternalProps> {
    layerIndex: number;
    openDirection: 'right' | 'left';
    closeEvent: 'mousedown' | 'click';
    static defaultProps: {
        borderOffset: number;
        rootOffset: number;
        horizontalOffset: number;
        openPosition: string;
        openDirection: string;
        up: boolean;
        onlyMountPositionUpdate: boolean;
    };
    constructor(props: PopoverInternalProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: PopoverInternalProps): void;
    updatePosition(popover: any, autoDirection?: boolean): void;
    getPopoverOffests(openDirection: 'right' | 'left', rootOffsetElement: Helpers.IHtmlElementPosition | null): {
        hOffset: number;
        vOffset: number;
    };
    setPopoverOffsets(popover: any, openDirection: 'right' | 'left', offsets: {
        hOffset: number;
        vOffset: number;
    }): void;
    isOverflowPopoverWidth(width: number, hOffset: number): boolean;
    getOpenDirection(): "left" | "right";
    getHorizontalOffset(openPosition: any, openDirection: any, horizontalOffset: any): any;
    handleOutsideClick: (event: any) => void;
    handleContextOutsideClick: (event: any) => void;
    render(): JSX.Element;
}
export interface PopoverInternalProps {
    children?: any;
    className?: string;
    style?: object;
    rootElement?: React.ReactInstance | null;
    borderOffset?: number;
    rootOffset?: number;
    horizontalOffset?: number | any;
    onOutsideClick: any;
    openPosition?: 'left' | 'right';
    openDirection?: 'left' | 'right';
    up?: boolean;
    autoDirection?: boolean;
    onlyMountPositionUpdate?: boolean;
    contextMenu?: boolean;
    updateStyle?: (style: CSSStyleDeclaration) => void;
    setStyledComponents?: (props: any) => string;
    closeEvent?: 'mousedown' | 'click';
}
export default PopoverInternal;
