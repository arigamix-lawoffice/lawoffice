import * as React from 'react';
declare class DialogInternal extends React.Component<DialogInternalProps> {
    layerIndex: number;
    overlayClick: boolean;
    static defaultProps: {
        isAutoSize: boolean;
        isOverlayHidden: boolean;
        closeByEsc: boolean;
        okByEnter: boolean;
        autofocus: boolean;
    };
    windowRef: React.RefObject<HTMLDivElement>;
    layoutRef: React.RefObject<HTMLDivElement>;
    constructor(props: DialogInternalProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    handleDialogClick: (event: React.MouseEvent<HTMLDivElement>) => void;
    handleOverlayClick: (event: any) => void;
    handleKeyDown: (event: React.KeyboardEvent<HTMLDivElement>) => void;
    handleMouseDown: (event: React.MouseEvent<HTMLDivElement>) => void;
    render(): JSX.Element | null;
    focus(): void;
    blur(): void;
}
export interface DialogInternalProps extends React.HTMLAttributes<HTMLDivElement> {
    isAutoSize?: boolean;
    isOverlayHidden?: boolean;
    closeByEsc?: boolean;
    okByEnter?: boolean;
    autofocus?: boolean;
    onCloseRequest?: (e: React.KeyboardEvent, success: boolean) => void;
}
export default DialogInternal;
