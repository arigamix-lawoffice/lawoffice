import React from 'react';
import { ITableCellViewModel } from 'tessa/ui/views/content';
import { FileViewModel } from 'tessa/ui/cards/controls';
export interface FileCaptionCellProps {
    cell: ITableCellViewModel;
    file: FileViewModel;
}
export declare class FileCaptionCell extends React.Component<FileCaptionCellProps> {
    render(): JSX.Element;
}
