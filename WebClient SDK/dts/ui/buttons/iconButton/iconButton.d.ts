import * as React from 'react';
declare class IconButton extends React.PureComponent<IconButtonProps> {
    render(): JSX.Element;
}
export interface IconButtonProps {
    children?: any;
    icon?: string;
    className?: string;
    disabled?: boolean;
    disableTouchRipple?: boolean;
    onClick?: any;
    [key: string]: any;
}
export default IconButton;
