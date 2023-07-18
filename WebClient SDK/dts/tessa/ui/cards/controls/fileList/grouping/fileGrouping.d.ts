import { FileControlObject } from '../fileControlObject';
import { FileViewModel } from '../fileViewModel';
export interface GroupInfo {
    groupId: string;
    groupCaption: string;
    order?: number;
}
export declare abstract class FileGrouping extends FileControlObject {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    abstract getGroupInfo(file: FileViewModel): GroupInfo;
    abstract clone(): FileGrouping;
}
