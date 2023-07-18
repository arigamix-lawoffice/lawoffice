import * as React from 'react';
import { ControlProps } from './controlProps';
import { DecimalBoxViewModel } from 'tessa/ui/cards/controls';
export interface DecimalBoxProps extends ControlProps<DecimalBoxViewModel> {
}
export declare class DecimalBox extends React.Component<DecimalBoxProps> {
    private _mainRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: DecimalBoxProps): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
    private handleInternalValidation;
}
