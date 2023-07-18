import * as React from 'react';
import { ControlProps } from './controlProps';
import { TextBoxViewModel } from 'tessa/ui/cards/controls';
export interface TextBoxProps extends ControlProps<TextBoxViewModel> {
}
export declare class TextBox extends React.Component<TextBoxProps> {
    private _mainRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: TextBoxProps): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
    private handleInternalValidation;
}
