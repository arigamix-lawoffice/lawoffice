import { TableRowViewModel, ITableRowViewModelCreateOptions, ITableCellViewModel } from 'tessa/ui/views/content';
import { FileViewModel, FileListViewModel } from 'tessa/ui/cards/controls';
export declare class TableFileRowViewModel extends TableRowViewModel {
    constructor(args: ITableRowViewModelCreateOptions, fileControl: FileListViewModel);
    private readonly _disposes;
    readonly fileViewModel: FileViewModel;
    readonly fileControl: FileListViewModel;
    initialize(cells: ReadonlyMap<string, ITableCellViewModel>): void;
    dispose(): void;
    private getStyleForFile;
}
