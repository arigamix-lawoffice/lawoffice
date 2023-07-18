import * as React from 'react';
import { ControlProps } from './controlProps';
import { IntegerBoxViewModel } from 'tessa/ui/cards/controls';
export interface IntegerBoxProps extends ControlProps<IntegerBoxViewModel> {
}
export declare class IntegerBox extends React.Component<IntegerBoxProps> {
    private _mainRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: IntegerBoxProps): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
    private handleInternalValidation;
}
