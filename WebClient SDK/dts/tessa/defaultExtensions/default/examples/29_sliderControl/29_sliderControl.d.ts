import * as React from 'react';
import { SliderViewModel } from './29_sliderViewModel';
import { ControlProps } from 'tessa/ui/cards/components/controls';
export declare class SliderControl extends React.Component<ControlProps<SliderViewModel>> {
    private readonly _inputRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<SliderViewModel>): void;
    render(): JSX.Element | null;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
}
export declare const StyledInput: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<React.AllHTMLAttributes<HTMLInputElement> & import("tessa/ui/cards/components/controls").StyledControlProps & React.RefAttributes<any>>, any, React.AllHTMLAttributes<HTMLInputElement> & import("tessa/ui/cards/components/controls").StyledControlProps, never>;
