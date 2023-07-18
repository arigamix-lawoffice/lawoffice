import { IFile } from 'tessa/files';
import { IFileSelectContext } from './interfaces';
export declare class FileSelectContext implements IFileSelectContext {
    constructor(replaceFile: IFile | null);
    selectFileDialogAccept?: string;
    readonly replaceFile: IFile | null;
}
