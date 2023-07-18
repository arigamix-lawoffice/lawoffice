import * as React from 'react';
import { PopoverProps } from '../popover/popover';
declare class Dropdown extends React.Component<DropdownProps> {
    shouldComponentUpdate(nextProps: DropdownProps): boolean;
    render(): JSX.Element;
}
export interface DropdownProps extends PopoverProps {
}
export default Dropdown;
