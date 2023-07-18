import { ViewControlTableGridViewModel, FileListViewModel } from 'tessa/ui/cards/controls';
export declare class FilesViewControlTableGridViewModel extends ViewControlTableGridViewModel {
    private _fileControl;
    get fileControl(): FileListViewModel;
    initialize(): void;
    dispose(): void;
}
