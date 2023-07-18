import { FileGrouping, GroupInfo } from './fileGrouping';
import { FileViewModel } from '../fileViewModel';
export declare class FileTypeGrouping extends FileGrouping {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    getGroupInfo(file: FileViewModel): GroupInfo;
    clone(): FileTypeGrouping;
}
