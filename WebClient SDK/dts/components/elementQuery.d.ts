import * as React from 'react';
declare class ElementQuery extends React.Component<IElementQueryProps, {}> {
    detector: any;
    cachedElement: HTMLElement;
    componentDidMount(): void;
    componentDidUpdate(): void;
    componentWillUnmount(): void;
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
    static getDefaultSize(): {
        name: string;
        width: number;
    }[];
    static sizeComponent(element: any, sizes: any, isMaxWidth: any, onResize: (() => void) | null, onBreakpointResize: ((layouts?: string) => void) | null): void;
    size(): {
        width: number;
        height: number;
    };
    render(): React.DetailedReactHTMLElement<React.HTMLAttributes<HTMLElement>, HTMLElement>;
}
export interface IElementQueryProps {
    children?: any;
    isMaxWidth?: boolean;
    sizes: {
        name: string;
        width: number;
    }[];
    onResize?: any;
    onBreakpointResize?: any;
}
export default ElementQuery;
