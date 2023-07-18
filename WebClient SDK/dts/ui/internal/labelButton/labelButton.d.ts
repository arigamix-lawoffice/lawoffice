import React from 'react';
declare class LabelButton extends React.PureComponent<LabelButtonProps> {
    static defaultProps: {
        labelPosition: string;
    };
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
}
export interface LabelButtonProps {
    children?: any;
    icon?: string | any;
    className?: string;
    disabled?: boolean;
    disableTouchRipple?: boolean;
    label?: string | null;
    labelPosition?: 'before' | 'after' | null;
    style?: object;
    onClick?: any;
    type?: string;
}
export default LabelButton;
