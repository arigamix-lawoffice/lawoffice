import * as React from 'react';
export declare function openFolderProperyDialog(dialogViewModel: FolderPropertyDialogViewModel): Promise<FolderPropertyDialogViewModel>;
export declare class FolderPropertyDialogViewModel {
    folderName: string;
    constructor(folderName: string);
}
export interface FolderPropertyDialogProps {
    viewModel: FolderPropertyDialogViewModel;
    onClose: (result: boolean) => void;
}
export declare class FolderPropertyDialog extends React.Component<FolderPropertyDialogProps> {
    private _inputRef;
    render(): JSX.Element;
    private handleInputRef;
    private handleRename;
    private handleCloseForm;
}
