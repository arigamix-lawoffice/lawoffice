import * as React from 'react';
import { FileListCategoryDialogViewModel } from 'tessa/ui/cards/controls';
import { FileCategory } from 'tessa/files';
export interface FileListCategoryDialogProps {
    viewModel: FileListCategoryDialogViewModel;
    onClose: (result: {
        cancel: boolean;
        category: FileCategory | null;
    }) => void;
}
export declare class FileListCategoryDialog extends React.Component<FileListCategoryDialogProps> {
    private _inputRef;
    render(): JSX.Element;
    private renderInput;
    private renderCategories;
    private handleInputRef;
    private handleManualInput;
    private handleCategorySelection;
    private handleCloseForm;
}
