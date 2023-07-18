import { FileCategory } from 'tessa/files';
export declare class FileListCategoryDialogViewModel {
    constructor(categories: FileCategory[], isNullCategoryEnabled: boolean, isManualCategoriesCreationEnabled: boolean);
    readonly categories: ReadonlyArray<FileCategory>;
    readonly isNullCategoryEnabled: boolean;
    readonly isManualCategoriesCreationEnabled: boolean;
}
