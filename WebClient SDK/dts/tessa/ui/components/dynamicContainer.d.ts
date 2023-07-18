import * as React from 'react';
export interface DynamicContainerProps {
    isMaxWidth?: boolean;
    sizes: {
        name: string;
        width: number;
    }[];
    onResize?: Function;
    onBreakpointResize?: Function;
}
export declare class DynamicContainer extends React.Component<DynamicContainerProps> {
    static get layout(): {
        xs: string;
        sm: string;
        md: string;
        lg: string;
        xl: string;
    };
    static get layoutOrder(): {
        xs: number;
        sm: number;
        md: number;
        lg: number;
        xl: number;
    };
    private detector;
    private cachedElement;
    get size(): {
        width: number;
        height: number;
    };
    componentDidMount(): void;
    componentDidUpdate(): void;
    componentWillUnmount(): void;
    render(): React.DetailedReactHTMLElement<React.HTMLAttributes<HTMLElement>, HTMLElement> | null;
    static getDefaultSize(): {
        name: string;
        width: number;
    }[];
    static sizeComponent(element: HTMLElement, sizes: {
        name: string;
        width: number;
    }[], isMaxWidth?: boolean, onResize?: Function, onBreakpointResize?: Function): void;
}
