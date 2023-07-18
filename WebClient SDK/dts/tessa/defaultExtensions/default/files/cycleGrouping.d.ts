import { FileGrouping, FileViewModel, GroupInfo } from 'tessa/ui/cards/controls';
export declare class CycleGrouping extends FileGrouping {
    constructor(name: string, caption: string, isCollapsed?: boolean);
    getGroupInfo(file: FileViewModel): GroupInfo;
    clone(): CycleGrouping;
    attach(file: FileViewModel): void;
    detach(file: FileViewModel): void;
    private static getCaption;
}
