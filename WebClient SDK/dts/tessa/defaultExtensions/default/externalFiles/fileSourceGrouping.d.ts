import { FileGrouping, FileViewModel, GroupInfo } from 'tessa/ui/cards/controls';
export declare class FileSourceGrouping extends FileGrouping {
    readonly cardSourceGroupName = "CardSourceGroup";
    readonly externalSourceGroupName = "ExternalSourceGroup";
    getGroupInfo(file: FileViewModel): GroupInfo;
    clone(): FileSourceGrouping;
}
