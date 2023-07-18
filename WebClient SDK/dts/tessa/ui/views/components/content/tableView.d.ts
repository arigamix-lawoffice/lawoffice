import * as React from 'react';
import { TableGridViewModelBase } from '../../content';
import { IViewComponentBase } from '../../viewComponentBase';
import { IWorkplaceViewComponent } from '../../workplaceViewComponent';
export interface TableViewProps<T extends IViewComponentBase> {
    viewModel: TableGridViewModelBase<T>;
}
export declare class TableView<T extends IViewComponentBase = IWorkplaceViewComponent> extends React.Component<TableViewProps<T>> {
    constructor(props: TableViewProps<T>);
    render(): JSX.Element;
    private get _columns();
    private get _rows();
    private get _blocks();
    private renderRowCounter;
    private handleHeaderDrop;
    private handleKeyDown;
}
