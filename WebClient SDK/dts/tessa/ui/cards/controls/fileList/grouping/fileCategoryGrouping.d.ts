import { FileGrouping, GroupInfo } from './fileGrouping';
import { FileViewModel } from '../fileViewModel';
export declare class FileCategoryGrouping extends FileGrouping {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    readonly noCategoryGroupName = "NoCategoryGroup";
    readonly noCategoryGroupCaption = "$UI_Controls_FilesControl_NoCategory";
    getGroupInfo(file: FileViewModel): GroupInfo;
    clone(): FileCategoryGrouping;
}
