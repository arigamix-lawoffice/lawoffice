import * as React from 'react';
import { ControlProps } from './controlProps';
import { ColorPickerViewModel } from 'tessa/ui/cards/controls';
export interface ColorPickerProps extends ControlProps<ColorPickerViewModel> {
}
export declare class ColorPickerControl extends React.Component<ColorPickerProps> {
    private _mainRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ColorPickerProps): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
    private handleInternalValidation;
}
export declare const toRGBAhexFromARGBhex: (str: string) => string;
