import React, { HTMLAttributes } from 'react';
declare class NumberArea extends React.PureComponent<NumberAreaProps> {
    private numberRef;
    syncHighlight(cssInputStyle: CSSStyleDeclaration, width?: number, height?: number): void;
    set scrollTop(scrollTop: number);
    render(): JSX.Element;
}
export interface NumberAreaProps extends HTMLAttributes<HTMLDivElement> {
    rows: number;
}
export default NumberArea;
