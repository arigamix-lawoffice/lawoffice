import * as React from 'react';
import { ControlProps } from '../controlProps';
import { IControlViewModel } from 'tessa/ui/cards/interfaces';
export declare class AutoCompleteTableControl extends React.Component<ControlProps<IControlViewModel>> {
    private _mainRef;
    constructor(props: ControlProps<IControlViewModel>);
    componentDidMount(): void;
    componentWillUnmount(): void;
    componentDidUpdate(prevProps: ControlProps<IControlViewModel>): void;
    render(): JSX.Element | null;
    focus(opt?: FocusOptions): void;
    private getItems;
    private getDropdownItems;
    private handleAddItem;
    private handleDeleteItem;
    private handleDropdownItemsRequest;
    private handleDotsModeToggle;
    private handleReferenceItem;
    private handleItemSelect;
    private handleKeyDown;
    private handleFocus;
    private handleBlur;
}
