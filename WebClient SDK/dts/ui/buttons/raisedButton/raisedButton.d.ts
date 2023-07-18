import * as React from 'react';
declare class RaisedButton extends React.PureComponent<RaisedButtonProps> {
    static defaultProps: {
        labelPosition: string;
    };
    private _labelButtonRef;
    render(): JSX.Element;
    focus(opt?: FocusOptions): void;
}
export interface RaisedButtonProps {
    children?: any;
    icon?: string | any;
    className?: string;
    disabled?: boolean;
    disableTouchRipple?: boolean;
    label?: string | null;
    labelPosition?: 'before' | 'after' | null;
    onClick?: Function;
    type?: string;
    [key: string]: any;
}
export default RaisedButton;
