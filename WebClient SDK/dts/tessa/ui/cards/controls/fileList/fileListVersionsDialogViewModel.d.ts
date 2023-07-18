import { FileListViewModel } from './fileListViewModel';
import { FileViewModel } from './fileViewModel';
import { ICardModel } from 'tessa/ui/cards';
import { MenuAction } from 'tessa/ui';
import { IFileVersion, FileContainer } from 'tessa/files';
export declare class FileListVersionsDialogViewModel {
    constructor(control: FileListViewModel, file: FileViewModel);
    private _getActionsFunc;
    private _closeRequest;
    readonly model: ICardModel;
    readonly fileContainer: FileContainer;
    readonly file: FileViewModel;
    getFileVersionActions(sourceFileVersion: IFileVersion, fileVersions: IFileVersion[]): ReadonlyArray<MenuAction>;
    setCloseRequest(request: (result: any) => void): void;
    close(result?: any): void;
}
