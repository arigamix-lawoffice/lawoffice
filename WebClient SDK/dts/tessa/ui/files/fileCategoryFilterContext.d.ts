import { FileCategory } from 'tessa/files';
import { FileInfo } from './fileInfo';
import { IFileCategoryFilterContext } from './interfaces';
export declare class FileCategoryFilterContext implements IFileCategoryFilterContext {
    constructor(categories: (FileCategory | null)[], fileInfos: FileInfo[]);
    readonly categories: (FileCategory | null)[];
    readonly fileInfos: FileInfo[];
    isManualCategoriesCreationDisabled: boolean;
}
