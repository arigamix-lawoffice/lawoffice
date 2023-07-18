import * as React from 'react';
import { ControlProps } from './controlProps';
import { IControlViewModel } from 'tessa/ui/cards/interfaces';
export declare class DateTimeControl extends React.Component<ControlProps<IControlViewModel>> {
    private readonly _mainRef;
    constructor(props: ControlProps<IControlViewModel>);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<IControlViewModel>): void;
    render(): JSX.Element | null;
    focus(opt?: FocusOptions): void;
    private handleChange;
    private handleKeyDown;
    private handleFocus;
    private handleBlur;
}
