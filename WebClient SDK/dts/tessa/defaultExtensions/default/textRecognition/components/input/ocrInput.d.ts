import * as React from 'react';
import { ControlProps } from 'tessa/ui/cards/components/controls';
import { OcrInputViewModel } from './ocrInputViewModel';
/** Пропсы для контрола редактируемого поля. */
interface OcrInputProps extends ControlProps<OcrInputViewModel> {
}
export declare class OcrInput extends React.Component<OcrInputProps> {
    private _mainRef;
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: OcrInputProps): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
}
export {};
