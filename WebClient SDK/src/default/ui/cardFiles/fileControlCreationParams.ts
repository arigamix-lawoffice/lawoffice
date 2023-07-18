import { IStorage } from 'tessa/platform/storage';

export class FileControlCreationParams {
  categoriesViewAlias = 'FileCategoriesFiltered';

  previewControlName = '';

  isCategoriesEnabled = false;

  isManualCategoriesCreationDisabled = false;

  isNullCategoryCreationDisabled = false;

  isIgnoreExistingCategories = false;

  categoriesViewMapping: Array<IStorage> = [];
}
