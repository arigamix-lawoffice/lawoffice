import { FileSorting } from './fileSorting';
import { FileViewModel } from '../fileViewModel';
export declare class FileDelegateSorting extends FileSorting {
    constructor(name: string, caption: string, func: (x: FileViewModel, y: FileViewModel) => number, isCollapsed?: boolean);
    private _func;
    protected compareCore(x: FileViewModel, y: FileViewModel): number;
    clone(): FileDelegateSorting;
}
