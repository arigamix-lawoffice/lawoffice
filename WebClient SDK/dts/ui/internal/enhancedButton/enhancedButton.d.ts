import React from 'react';
declare class EnhancedButton extends React.Component<IEnhancedButtonProps, {}> {
    static defaultProps: {
        disableTouchRipple: boolean;
        containerElement: string;
        type: string;
        tabIndex: number;
        touchRippleColor: string;
        touchRippleOpacity: number;
        onBlur: () => void;
        onClick: () => void;
        onFocus: () => void;
        onKeyDown: () => void;
        onKeyUp: () => void;
        onMouseDown: () => void;
        onMouseUp: () => void;
        onMouseEnter: () => void;
        onMouseLeave: () => void;
        onTouchStart: () => void;
        onTouchEnd: () => void;
    };
    createButtonChildren(): JSX.Element;
    focus(opt?: FocusOptions): void;
    handleMouseDown: (event: any) => void;
    handleMouseUp: (event: any) => void;
    handleMouseLeave: (event: any) => void;
    handleTouchStart: (event: any) => void;
    handleTouchEnd: (event: any) => void;
    render(): React.DOMElement<{
        ref: string;
        style: {
            /**
             * This is needed so that ripples do not bleed past border radius.
             * See: http://stackoverflow.com/questions/17298739/
             * css-overflow-hidden-not-working-in-chrome-when-parent-has-border-radius-and-chil
             */
            transform: string | null;
            outline: string;
        } & object;
        disabled: boolean | undefined;
        onMouseDown: (event: any) => void;
        onMouseUp: (event: any) => void;
        onMouseLeave: (event: any) => void;
        onTouchStart: (event: any) => void;
        onTouchEnd: (event: any) => void;
        type?: string | undefined;
        className?: string | undefined;
        tabIndex?: number | undefined;
        onBlur?: any;
        onClick?: any;
        onFocus?: any;
        onKeyDown?: any;
        onKeyUp?: any;
        onMouseEnter?: any;
    }, Element>;
}
export interface IEnhancedButtonProps {
    children?: any;
    containerElement?: string | HTMLElement;
    type?: string;
    style?: object;
    className?: string;
    tabIndex?: number;
    disabled?: boolean;
    disableTouchRipple?: boolean;
    centerRipple?: boolean;
    touchRippleColor?: string;
    touchRippleOpacity?: number;
    onBlur?: any;
    onClick?: any;
    onFocus?: any;
    onKeyDown?: any;
    onKeyUp?: any;
    onMouseDown?: any;
    onMouseUp?: any;
    onMouseEnter?: any;
    onMouseLeave?: any;
    onTouchStart?: any;
    onTouchEnd?: any;
}
export default EnhancedButton;
