import * as React from 'react';
import { ControlProps } from './controlProps';
import { ButtonViewModel } from 'tessa/ui/cards/controls';
export interface ButtonProps extends ControlProps<ButtonViewModel> {
}
export declare class ButtonControl extends React.Component<ButtonProps> {
    private _mainRef;
    constructor(props: ButtonProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ButtonProps): void;
    render(): JSX.Element | null;
    focus(opt?: FocusOptions): void;
    private static getDivStyle;
    private handleClick;
    private handleFocus;
    private handleBlur;
}
