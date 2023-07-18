import * as React from 'react';
declare class FlatButton extends React.PureComponent<FlatButtonProps> {
    static defaultProps: {
        labelPosition: string;
    };
    render(): JSX.Element;
}
export interface FlatButtonProps {
    children?: any;
    icon?: string | any;
    className?: string;
    disabled?: boolean;
    disableTouchRipple?: boolean;
    label?: string | null;
    labelPosition?: 'before' | 'after' | null;
    style?: object;
    onClick?: any;
    [key: string]: any;
}
export default FlatButton;
