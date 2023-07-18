import { IFileExtensionContext, IFileControl } from './interfaces';
import { FileExtensionContextBase } from './fileExtensionContextBase';
import { FileViewModel } from 'tessa/ui/cards/controls';
export declare class FileExtensionContext extends FileExtensionContextBase implements IFileExtensionContext {
    constructor(file: FileViewModel, files: FileViewModel[], control: IFileControl, cloneCollections: boolean);
    readonly file: FileViewModel;
    readonly files: ReadonlyArray<FileViewModel>;
}
