import * as React from 'react';
import { ControlProps } from './controlProps';
import { IControlViewModel } from 'tessa/ui/cards/interfaces';
import { DoubleBoxViewModel } from 'tessa/ui/cards/controls';
export interface DoubleBoxProps extends ControlProps<DoubleBoxViewModel> {
}
export declare class DoubleBox extends React.Component<DoubleBoxProps> {
    private _mainRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<IControlViewModel>): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
    private handleInternalValidation;
}
