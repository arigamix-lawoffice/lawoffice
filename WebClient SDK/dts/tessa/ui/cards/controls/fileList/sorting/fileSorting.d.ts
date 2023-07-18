import { FileControlObject } from '../fileControlObject';
import { FileViewModel } from '../fileViewModel';
export declare abstract class FileSorting extends FileControlObject {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    protected abstract compareCore(x: FileViewModel, y: FileViewModel): number;
    compare(x: FileViewModel, y: FileViewModel): number;
    abstract clone(): FileSorting;
}
