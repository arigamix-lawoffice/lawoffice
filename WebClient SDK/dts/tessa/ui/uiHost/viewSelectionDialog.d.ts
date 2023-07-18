import * as React from 'react';
declare class ViewSelectionDialog extends React.Component<ViewSelectionDialogProps> {
    render(): JSX.Element | null;
}
export interface ViewSelectionDialogProps {
    noPortal?: boolean;
    isOpen?: boolean;
    values: any[];
    onClose: any;
    onSelection: any;
}
export default ViewSelectionDialog;
