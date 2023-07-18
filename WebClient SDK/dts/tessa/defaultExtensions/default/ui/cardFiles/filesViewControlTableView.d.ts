import * as React from 'react';
import { FilesViewControlTableGridViewModel } from './filesViewControlTableGridViewModel';
export interface FilesViewControlTableViewProps {
    viewModel: FilesViewControlTableGridViewModel;
}
export declare class FilesViewControlTableView extends React.Component<FilesViewControlTableViewProps> {
    render(): JSX.Element;
    private handleDrop;
}
