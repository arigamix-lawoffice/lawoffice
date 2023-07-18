import { IStorage } from 'tessa/platform/storage';
export declare class FileControlCreationParams {
    categoriesViewAlias: string;
    previewControlName: string;
    isCategoriesEnabled: boolean;
    isManualCategoriesCreationDisabled: boolean;
    isNullCategoryCreationDisabled: boolean;
    isIgnoreExistingCategories: boolean;
    categoriesViewMapping: Array<IStorage>;
}
