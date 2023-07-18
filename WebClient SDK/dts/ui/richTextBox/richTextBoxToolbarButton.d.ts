import React, { SyntheticEvent } from 'react';
export interface RichTextBoxToolbarButtonProps {
    icon: string;
    onMouseDown: (event: SyntheticEvent) => void;
    className: string;
    title?: string;
    renderIcon?: (svgIconPath: string) => JSX.Element | null;
}
export declare class RichTextBoxToolbarButton extends React.PureComponent<RichTextBoxToolbarButtonProps> {
    render(): JSX.Element;
}
