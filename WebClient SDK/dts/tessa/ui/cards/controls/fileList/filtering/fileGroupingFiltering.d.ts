import { FileFiltering } from './fileFiltering';
import { FileViewModel } from '../fileViewModel';
import { FileGrouping } from '../grouping/fileGrouping';
export declare class FileGroupingFiltering extends FileFiltering {
    constructor(grouping: FileGrouping, name: string, caption: string);
    readonly grouping: FileGrouping;
    isVisible(viewModel: FileViewModel): boolean;
    equals(other: FileFiltering): boolean;
    static create(grouping: FileGrouping, viewModel: FileViewModel): FileGroupingFiltering;
    static create(grouping: FileGrouping, name: string, caption: string): FileGroupingFiltering;
}
