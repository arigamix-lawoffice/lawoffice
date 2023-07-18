import * as React from 'react';
import { FileListTypeDialogViewModel } from 'tessa/ui/cards/controls';
import { FileType } from 'tessa/files';
export interface FileListTypeDialogProps {
    viewModel: FileListTypeDialogViewModel;
    onClose: (result: {
        cancel: boolean;
        type: FileType | null;
    }) => void;
}
export declare class FileListTypeDialog extends React.Component<FileListTypeDialogProps> {
    render(): JSX.Element;
    private handleTypeSelection;
    private handleCloseForm;
}
