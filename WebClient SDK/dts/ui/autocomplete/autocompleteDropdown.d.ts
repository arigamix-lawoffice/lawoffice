import * as React from 'react';
import { AutocompleteDropDownItemsCollection } from './autocomplete';
declare class AutocompleteDropdown extends React.Component<IAutocompleteDropdownProps> {
    static defaultProps: {
        focusedItemIndex: number;
    };
    render(): JSX.Element;
    renderList(): JSX.Element[];
}
export interface IAutocompleteDropdownProps {
    dropdownItems: AutocompleteDropDownItemsCollection | null;
    focusedItemIndex?: number;
    onItemSelect: (rowIndex: number) => void;
}
export default AutocompleteDropdown;
