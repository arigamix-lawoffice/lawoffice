import { FileGrouping, GroupInfo } from './fileGrouping';
import { FileViewModel } from '../fileViewModel';
export declare class FileDelegateGrouping extends FileGrouping {
    constructor(name: string, caption: string, func: (file: FileViewModel) => GroupInfo, isCollapsed?: boolean);
    private _func;
    getGroupInfo(file: FileViewModel): GroupInfo;
    clone(): FileDelegateGrouping;
}
