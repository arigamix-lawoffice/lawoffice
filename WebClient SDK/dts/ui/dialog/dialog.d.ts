import React from 'react';
import { DialogInternalProps } from './dialogInternal';
declare class Dialog extends React.Component<DialogProps, DialogState> {
    private readonly _el;
    private _unmounted;
    private _dialogRef;
    constructor(props: DialogProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    render(): JSX.Element | null;
    internalRender(): JSX.Element;
    focus(): void;
    blur(): void;
}
export interface DialogState {
    mounted: boolean;
}
export interface DialogProps extends DialogInternalProps {
    noPortal?: boolean;
    isOpened?: boolean;
}
export default Dialog;
