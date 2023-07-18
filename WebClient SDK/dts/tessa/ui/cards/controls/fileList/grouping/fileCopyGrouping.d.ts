import { FileGrouping, GroupInfo } from './fileGrouping';
import { FileViewModel } from '../fileViewModel';
import { FileListViewModel } from '../fileListViewModel';
export declare class FileCopyGrouping extends FileGrouping {
    constructor(name: string, caption: string, getFiles: () => ReadonlyArray<FileViewModel>, isCollapsed?: boolean);
    private _getFiles;
    private _sortingBeforeInitialization;
    private _sortDirectionBeforeInitialization;
    readonly noCopyGroupName = "NoCopyGroup";
    readonly noCopyGroupCaption = "$UI_Controls_FilesControl_NoCopy";
    getGroupInfo(viewModel: FileViewModel): GroupInfo;
    clone(): FileCopyGrouping;
    private static getCaption;
    attach(file: FileViewModel): void;
    detach(file: FileViewModel): void;
    initialize(control: FileListViewModel): void;
    finalize(control: FileListViewModel): void;
}
