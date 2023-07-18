import * as React from 'react';
import { SyntaxHighlighting } from 'tessa/cards/syntaxHighlighting';
import { AvalonTextBoxFontType } from 'tessa/cards/avalonTextBoxFontType';
import { TextBoxMode } from './textBoxMode';
import 'highlight.js/styles/vs.css';
declare class EnhancedTextArea extends React.PureComponent<EnhancedTextAreaProps, EnhancedTextAreaState> {
    private inputAreaRef;
    private outputAreaRef;
    private inputDivRef;
    private numbersAreaRef;
    private resizeTimout;
    static defaultProps: {
        type: string;
        rows: number;
        rowsMax: number;
    };
    constructor(props: any);
    componentDidMount(): void;
    componentDidUpdate(prevProps: EnhancedTextAreaProps): void;
    componentWillUnmount(): void;
    focus(opt?: FocusOptions): void;
    blur(): void;
    select(): void;
    get value(): string;
    set value(value: string);
    syncHeight(): void;
    syncRows(value?: string): {
        rows: number;
        rowsCount: number;
    };
    setHighlightOutput: () => void;
    refreshHighlighting: () => void;
    handleResize: () => void;
    inputAreaOnChangeHandler: () => void;
    inputScrollingHandler: () => void;
    onFocus: (event: any) => void;
    onBlur: (event: any) => void;
    onChange: (event: any) => void;
    onKeyDown: (event: any) => void;
    render(): JSX.Element;
}
export interface EnhancedTextAreaProps {
    id?: string;
    type?: string;
    style?: object;
    className?: string;
    defaultValue?: string | undefined;
    value?: string | undefined;
    disabled?: boolean;
    rows: number;
    rowsMax: number;
    onChange?: (event: Event) => void;
    onFocus?: (event: Event) => void;
    onBlur?: (event: Event) => void;
    onKeyDown?: (event: Event) => void;
    syncOnResize?: boolean;
    textBoxMode?: TextBoxMode;
    avalonFontType?: AvalonTextBoxFontType;
    avalonShowLineNumbers?: boolean;
    avalonSyntaxType?: SyntaxHighlighting;
}
export interface EnhancedTextAreaState {
    rows: number;
    rowsCount: number;
}
export default EnhancedTextArea;
