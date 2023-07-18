import * as React from 'react';
import { ControlProps } from '../controlProps';
import { IControlViewModel } from 'tessa/ui/cards/interfaces';
export declare class NumeratorControl extends React.Component<ControlProps<IControlViewModel>> {
    private readonly _mainRef;
    constructor(props: ControlProps<IControlViewModel>);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<IControlViewModel>): void;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleFocus;
    private handleBlur;
    private handleKeyDown;
}
