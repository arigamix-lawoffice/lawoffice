import { IFileControlExtensionContext, IFileControl } from './interfaces';
import { FileExtensionContextBase } from './fileExtensionContextBase';
import { FileGrouping } from 'tessa/ui/cards/controls/fileList/grouping';
import { FileSorting } from 'tessa/ui/cards/controls/fileList/sorting';
export declare class FileControlExtensionContext extends FileExtensionContextBase implements IFileControlExtensionContext {
    constructor(control: IFileControl, cloneCollections: boolean);
    readonly groupings: FileGrouping[];
    readonly sortings: FileSorting[];
}
