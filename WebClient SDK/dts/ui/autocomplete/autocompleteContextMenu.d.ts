import * as React from 'react';
declare class AutocompleteContextMenu extends React.Component<IAutocompleteContextMenuProps, {}> {
    handleDeleteClick: () => void;
    handleReferenceClick: () => void;
    render(): JSX.Element;
}
export interface IAutocompleteContextMenuProps {
    itemIndex?: number;
    isDeleteAllowed?: boolean;
    deleteCommand?: any;
    isReferenceAllowed?: boolean;
    referenceCommand?: any;
    onCloseRequest: any;
}
export default AutocompleteContextMenu;
