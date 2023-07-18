import * as React from 'react';
declare class DropdownItem extends React.Component<IDropdownItemProps, {}> {
    render(): JSX.Element;
}
export interface IDropdownItemProps {
    children?: any;
    className?: string;
    style?: object;
    onClick?: any;
    [key: string]: any;
}
export default DropdownItem;
