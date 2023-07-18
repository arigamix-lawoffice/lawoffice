import { IFileVersionExtensionContext, IFileControl } from './interfaces';
import { FileExtensionContextBase } from './fileExtensionContextBase';
import { IFileVersion } from 'tessa/files';
import { FileViewModel, FileListVersionsDialogViewModel } from 'tessa/ui/cards/controls';
export declare class FileVersionExtensionContext extends FileExtensionContextBase implements IFileVersionExtensionContext {
    constructor(file: FileViewModel, version: IFileVersion, versions: IFileVersion[], control: IFileControl, dialog: FileListVersionsDialogViewModel, cloneCollections: boolean);
    readonly file: FileViewModel;
    readonly version: IFileVersion;
    readonly versions: ReadonlyArray<IFileVersion>;
    readonly dialog: FileListVersionsDialogViewModel;
}
