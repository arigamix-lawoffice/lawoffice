import * as React from 'react';
import { ControlProps } from 'tessa/ui/cards/components/controls';
import { OcrSliderViewModel as OcrSliderViewModel } from './ocrSliderViewModel';
import './ocrSliderStyle.css';
/** Контрол ползунка для ввода вещественных значений. */
export declare class OcrSlider extends React.Component<ControlProps<OcrSliderViewModel>> {
    private readonly _inputRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<OcrSliderViewModel>): void;
    render(): JSX.Element | null;
    /**
     * Выполняет установку фокуса на элементе.
     * @param options Параметры установки фокуса.
     */
    focus(options?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
}
export declare const StyledInput: import("styled-components").StyledComponent<React.ForwardRefExoticComponent<React.AllHTMLAttributes<HTMLInputElement> & import("tessa/ui/cards/components/controls").StyledControlProps & React.RefAttributes<any>>, any, React.AllHTMLAttributes<HTMLInputElement> & import("tessa/ui/cards/components/controls").StyledControlProps, never>;
