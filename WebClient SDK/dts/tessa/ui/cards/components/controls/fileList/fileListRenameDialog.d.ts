import * as React from 'react';
import { FileListRenameDialogViewModel } from 'tessa/ui/cards/controls';
export interface FileListRenameDialogProps {
    viewModel: FileListRenameDialogViewModel;
    onClose: (result: {
        cancel: boolean;
        name: string;
    }) => void;
}
export declare class FileListRenameDialog extends React.Component<FileListRenameDialogProps> {
    private _inputRef;
    render(): JSX.Element;
    private handleInputRef;
    private handleRename;
    private handleCloseForm;
}
