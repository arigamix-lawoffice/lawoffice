import { FileViewModel } from './fileViewModel';
import { FileListViewModel } from './fileListViewModel';
export declare abstract class FileControlObject {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    readonly name: string;
    readonly caption: string;
    isCollapsed: boolean;
    attach(_viewModel: FileViewModel): void;
    detach(_viewModel: FileViewModel): void;
    initialize(control: FileListViewModel): void;
    finalize(control: FileListViewModel): void;
    notifyAdded(viewModel: FileViewModel): void;
    notifyRemoved(viewModel: FileViewModel): void;
}
