import { FileSorting } from './fileSorting';
import { FileViewModel } from '../fileViewModel';
export declare class FileSizeSorting extends FileSorting {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    protected compareCore(x: FileViewModel, y: FileViewModel): number;
    clone(): FileSizeSorting;
}
