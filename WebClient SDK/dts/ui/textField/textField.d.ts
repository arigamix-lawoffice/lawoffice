import React, { InputHTMLAttributes } from 'react';
import { IMaskInputProps } from 'react-imask/dist/mixin';
import { TextBoxMode } from './textBoxMode';
import { AvalonTextBoxFontType } from 'tessa/cards/avalonTextBoxFontType';
import { SyntaxHighlighting } from 'tessa/cards/syntaxHighlighting';
export interface TextFieldProps extends InputHTMLAttributes<HTMLInputElement> {
    useValueState?: boolean;
    maskOptions?: IMaskInputProps | null;
    onValidate?: (rawValue: string | null) => string;
    divContainer?: boolean;
    multiLine?: boolean;
    rows?: number;
    rowsMax?: number;
    textBoxMode?: TextBoxMode;
    avalonFontType?: AvalonTextBoxFontType;
    avalonShowLineNumbers?: boolean;
    avalonSyntaxType?: SyntaxHighlighting;
    bindReactComponentRef?: (ref: React.RefObject<HTMLInputElement>) => void;
    unbindReactComponentRef?: () => void;
}
interface TextFieldState {
    isFocused: boolean;
}
export default class TextField extends React.PureComponent<TextFieldProps, TextFieldState> {
    constructor(props: TextFieldProps);
    private _input;
    componentDidMount(): void;
    componentWillUnmount(): void;
    focus(opt?: FocusOptions): void;
    blur(): void;
    select(): void;
    getValue(): string;
    get isFocused(): boolean;
    private onFocusHandler;
    private onBlurHandler;
    private onSelectHandler;
    private onBeforeInputHandler;
    render(): JSX.Element;
}
export {};
