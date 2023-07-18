import * as React from 'react';
import { FileListSelectCertDialogViewModel } from 'tessa/ui/cards/controls';
import { CertData } from 'tessa/files';
export interface FileListSelectCertDialogProps {
    viewModel: FileListSelectCertDialogViewModel;
    onClose: (result: {
        cancel: boolean;
        cert: CertData | null;
        comment: string | null;
    }) => void;
}
export interface FileListSelectCertDialogState {
    comment: boolean;
}
export declare class FileListSelectCertDialog extends React.Component<FileListSelectCertDialogProps, FileListSelectCertDialogState> {
    constructor(props: FileListSelectCertDialogProps);
    private commentRef;
    render(): JSX.Element;
    private renderCert;
    private handleShowComment;
    private handleSelectCert;
    private handleCloseForm;
    private handleCloseFormWithData;
}
