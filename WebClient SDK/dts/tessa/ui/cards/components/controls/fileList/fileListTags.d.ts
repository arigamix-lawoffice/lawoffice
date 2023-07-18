import React, { HTMLAttributes } from 'react';
import { FileViewModel } from 'tessa/ui/cards/controls';
export interface FileListTagsProps extends HTMLAttributes<HTMLDivElement> {
    viewModel: FileViewModel;
}
export declare class FileListTags extends React.Component<FileListTagsProps> {
    render(): JSX.Element | null;
}
