import React from 'react';
import { FileListSignsDialogViewModel } from 'tessa/ui/cards/controls';
export interface FileListSignsDialogProps {
    viewModel: FileListSignsDialogViewModel;
    onClose: () => void;
}
export declare class FileListSignsDialog extends React.Component<FileListSignsDialogProps> {
    constructor(props: FileListSignsDialogProps);
    render(): JSX.Element;
    private handleCloseForm;
    private handleVerifySignatures;
}
