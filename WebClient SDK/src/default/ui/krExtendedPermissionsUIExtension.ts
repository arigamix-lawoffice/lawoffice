import {
  CardUIExtension,
  IBlockViewModel,
  ICardModel,
  ICardUIExtensionContext,
  IControlViewModel,
  IFormWithBlocksViewModel
} from 'tessa/ui/cards';
import {
  AddFiles,
  ControlType,
  KrPermissionsFileSettings,
  KrPermissionSectionSettings,
  KrPermissionsFileExtensionSettings,
  KrPermissionsFilesSettings,
  KrPermissionVisibilitySettings,
  KrToken,
  SignFiles
} from 'tessa/workflow';
import {
  FileListViewModel,
  GridRowEventArgs,
  GridViewModel,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';
import { isTopLevelForm, showNotEmpty, tryGetFromInfo } from 'tessa/ui';
import { ValidationResult } from 'tessa/platform/validation';
import { TaskViewModel } from 'tessa/ui/cards/tasks';
import { unseal, Visibility } from 'tessa/platform';
import { LocalizationManager } from 'tessa/localization';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import { CardRowState } from 'tessa/cards';
import {
  FileCategoryFilterContext,
  FileInfo,
  IFileCategoryFilterContext,
  IFileControl,
  IFileSelectContext,
  IFileValidateCategoryContext,
  IFileValidateContentContext
} from 'tessa/ui/files';
import { FileCategory, getExtension, IFile, IFilePermissions } from 'tessa/files';
import {
  addFileValidationError,
  KrPermissionsErrorAction,
  KrPermissionsErrorType,
  noCategoryFilesCategoryId
} from 'tessa/workflow';
import { reaction } from 'mobx';

class VisibilitySetting {
  public text: string;
  public isHidden: boolean;
  public isPattern: boolean;

  constructor(text: string, isHidden: boolean, isPattern: boolean) {
    this.text = text;
    this.isHidden = isHidden;
    this.isPattern = isPattern;
  }
}

class VisibilitySettings {
  public blockSettings: Array<VisibilitySetting> = [];
  public controlSettings: Array<VisibilitySetting> = [];
  public tabSettings: Array<VisibilitySetting> = [];

  public fill(visibilitySettings: KrPermissionVisibilitySettings[]) {
    for (const visibilitySetting of visibilitySettings) {
      let patternList: Array<VisibilitySetting>;
      switch (visibilitySetting.controlType) {
        case ControlType.Tab:
          patternList = this.tabSettings;
          break;

        case ControlType.Block:
          patternList = this.blockSettings;
          break;

        case ControlType.Control:
          patternList = this.controlSettings;
          break;

        default:
          continue;
      }
      this.fillSettings(patternList, visibilitySetting);
    }
  }

  private fillSettings(
    patternList: Array<VisibilitySetting>,
    visibilitySetting: KrPermissionVisibilitySettings
  ) {
    const alias: string = visibilitySetting.alias;
    const length: number = alias.length;
    const wildStart: boolean = alias[0] === '*';
    const wildEnd: boolean = alias[length - 1] === '*';
    if (wildStart || wildEnd) {
      const startIndex = wildStart ? 1 : 0;
      const endIndex = Math.max(0, length - (wildStart ? 1 : 0) - (wildEnd ? 1 : 0));
      const escapedAlias =
        // TODO: escape
        // endIndex + 1: substring возвращает подстроку от startIndex до endIndex не включительно (+1 для того чтобы включить последний элемент)
        alias.substring(startIndex, endIndex + 1);
      patternList.push(new VisibilitySetting(escapedAlias, visibilitySetting.isHidden, true));
    } else {
      patternList.push(new VisibilitySetting(alias, visibilitySetting.isHidden, false));
    }
  }
}

class PermissionsControlsVisitor {
  constructor(disposes: Array<(() => void) | null>) {
    this._disposes = disposes;
  }

  public hideSections: Set<guid> = new Set();
  public hideFields: Set<guid> = new Set();
  public showFields: Set<guid> = new Set();
  public mandatorySections: Set<guid> = new Set();
  public mandatoryFields: Set<guid> = new Set();
  public disallowedSections = new Set();
  private _visibilitySettings: VisibilitySettings;
  private fileSettings: KrPermissionsFileSettings[] | undefined;
  private ownFilesSettings: KrPermissionsFilesSettings | null | undefined;
  private otherFilesSettings: KrPermissionsFilesSettings | null | undefined;
  private canAddFiles: boolean;
  private canSignFiles: boolean;

  private _disposes: Array<(() => void) | null>;

  public visit(cardModel: ICardModel, parentGrids: Array<GridViewModel>) {
    let disableGrids = false;
    if (cardModel.table && cardModel.table.row.state !== CardRowState.Inserted) {
      for (const grid of parentGrids) {
        // Если хотя бы одна из родительских таблиц недоступна для редактирования через KrPermissions, то и все таблицы текущей строки должны быть недоступны.
        const parentGridSource = grid.cardTypeControl.getSourceInfo();
        if (this.disallowedSections.has(parentGridSource.sectionId)) {
          disableGrids = true;
          break;
        }
      }
    }

    if (this.ownFilesSettings || this.otherFilesSettings) {
      this._disposes.push(
        cardModel.fileContainer.containerFileChanged.addWithDispose(e => {
          if (!e.added) {
            return;
          }

          const file = e.added;
          (file.permissions as IFilePermissions).canSign = this.isSignAllowed(file);

          this._disposes.push(
            reaction(
              () => file.name,
              () => ((file.permissions as IFilePermissions).canSign = this.isSignAllowed(file))
            )
          );

          this._disposes.push(
            reaction(
              () => file.category,
              () => ((file.permissions as IFilePermissions).canSign = this.isSignAllowed(file))
            )
          );
        })
      );
    }

    for (const control of cardModel.controlsBag) {
      this.visitControl(control, cardModel);
      let grid: GridViewModel | null = null;
      if (control instanceof GridViewModel) {
        grid = control as GridViewModel;
      }
      if (disableGrids && grid) {
        grid.isReadOnly = true;
      }
    }

    for (const block of cardModel.blocksBag) {
      this.visitBlock(block);
    }

    for (const form of cardModel.formsBag) {
      this.visitForm(cardModel, form);
    }
  }

  public fill(
    token: KrToken,
    sectionSettings: KrPermissionSectionSettings[] | undefined,
    visibilitySettings: VisibilitySettings,
    fileSettings: KrPermissionsFileSettings[] | undefined,
    ownFilesSettings: KrPermissionsFilesSettings | null | undefined,
    otherFilesSettings: KrPermissionsFilesSettings | null | undefined
  ) {
    if (sectionSettings) {
      for (const sectionSetting of sectionSettings) {
        if (sectionSetting.isHidden) {
          this.hideSections.add(sectionSetting.id);
        } else {
          sectionSetting.hiddenFields.forEach(x => this.hideFields.add(x));
        }

        sectionSetting.visibleFields.forEach(x => this.showFields.add(x));

        if (sectionSetting.isMandatory) {
          this.mandatorySections.add(sectionSetting.id);
        } else {
          sectionSetting.mandatoryFields.forEach(x => this.mandatoryFields.add(x));
        }
        if (sectionSetting.isDisallowed) {
          this.disallowedSections.add(sectionSetting.id);
        }
      }
    }

    this._visibilitySettings = visibilitySettings;
    this.fileSettings = fileSettings;
    this.ownFilesSettings = ownFilesSettings;
    this.otherFilesSettings = otherFilesSettings;
    this.canAddFiles = token.permissions.has(AddFiles);
    this.canSignFiles = token.permissions.has(SignFiles);
  }

  private visitForm(model: ICardModel, form: IFormWithBlocksViewModel) {
    if (form.name) {
      const hidden: boolean | null = this.checkIsHidden(
        this._visibilitySettings.tabSettings,
        form.name
      );
      if (hidden) {
        this.hideForm(model, form);
      }
      // Нет возможности добавлять скрытую форму, т.к. она не была сгенерирована
    }
  }

  private visitBlock(block: IBlockViewModel) {
    if (block.name) {
      const hidden: boolean | null = this.checkIsHidden(
        this._visibilitySettings.blockSettings,
        block.name
      );
      if (hidden !== null) {
        if (hidden) {
          this.hideBlock(block);
        } else {
          this.showBlock(block);
        }
      }
    }
  }

  private visitControl(control: IControlViewModel, cardModel: ICardModel) {
    const sourceInfo = control.cardTypeControl.getSourceInfo();
    if (
      (this.hideSections.has(sourceInfo.sectionId) &&
        !sourceInfo.columnIds.some(x => this.showFields.has(x))) ||
      sourceInfo.columnIds.some(x => this.hideFields.has(x))
    ) {
      this.hideControl(control);
    }
    if (control.name) {
      const hidden: boolean | null = this.checkIsHidden(
        this._visibilitySettings.controlSettings,
        control.name
      );
      if (hidden !== null) {
        if (hidden) {
          this.hideControl(control);
        } else {
          this.showControl(control);
        }
      }
    }

    if (
      this.mandatorySections.has(sourceInfo.sectionId) ||
      sourceInfo.columnIds.some(x => this.mandatoryFields.has(x))
    ) {
      this.makeControlMandatory(control);
    }

    if (
      !this.canAddFiles ||
      this.fileSettings ||
      this.ownFilesSettings ||
      this.otherFilesSettings
    ) {
      const fileControl = control as FileListViewModel;
      if (fileControl) {
        this.setFileControlActions(fileControl);
      }

      const viewControl = control as ViewControlViewModel;
      if (viewControl?.name) {
        const fileControl2 = tryGetFromInfo<FileListViewModel | null>(
          cardModel.info,
          viewControl.name
        );
        if (fileControl2) {
          this.setFileControlActions(fileControl2);
        }
      }
    }
  }

  private setFileControlActions(fileControl: IFileControl): void {
    const prevModifyFileSelect = fileControl.modifyFileSelect;
    fileControl.modifyFileSelect = prevModifyFileSelect
      ? async context => {
          await prevModifyFileSelect(context);
          await this.modifyFileSelect(context);
        }
      : async context => {
          await this.modifyFileSelect(context);
        };

    const prevValidateFileCategory = fileControl.validateFileCategory;
    fileControl.validateFileCategory = prevValidateFileCategory
      ? async context => {
          await prevValidateFileCategory(context);
          this.validateFileCategory(context);
        }
      : context => {
          this.validateFileCategory(context);
          return Promise.resolve();
        };

    const prevValidateFileContent = fileControl.validateFileContent;
    fileControl.validateFileContent = prevValidateFileContent
      ? async context => {
          await prevValidateFileContent(context);
          this.validateFileContent(context);
        }
      : context => {
          this.validateFileContent(context);
          return Promise.resolve();
        };

    const prevCategoryFilter = fileControl.categoryFilter;
    fileControl.categoryFilter = prevCategoryFilter
      ? async context => {
          const result = await prevCategoryFilter(context);
          const nextContext = new FileCategoryFilterContext([...result], context.fileInfos);

          nextContext.isManualCategoriesCreationDisabled =
            context.isManualCategoriesCreationDisabled;
          const nextResult = await this.categoryFilter(nextContext);
          context.isManualCategoriesCreationDisabled =
            nextContext.isManualCategoriesCreationDisabled;

          return nextResult;
        }
      : this.categoryFilter;
  }

  private async modifyFileSelect(context: IFileSelectContext): Promise<void> {
    const filesSettings = this.selectFilesSettings(context.replaceFile);

    const allowedExtensions: Set<string> = new Set<string>();
    if (!context.replaceFile) {
      if (
        !this.canAddFiles &&
        !!filesSettings &&
        (!filesSettings.globalSettings ||
          (!filesSettings.globalSettings.addAllowed &&
            filesSettings.globalSettings.allowedCategories.length === 0))
      ) {
        filesSettings.tryGetExtensionSettings()?.forEach((value, key) => {
          if (value.addAllowed || value.allowedCategories.length > 0) {
            allowedExtensions.add(key);
          }
        });
      }
    } else {
      const categoryID = PermissionsControlsVisitor.getCategoryID(context.replaceFile.category);
      let needFilter = false;

      if (filesSettings?.globalSettings) {
        if (filesSettings.globalSettings.addAllowed) {
          needFilter =
            !!categoryID && filesSettings.globalSettings.disallowedCategories.includes(categoryID);
        } else if (filesSettings.globalSettings.addDisallowed) {
          needFilter =
            !categoryID || !filesSettings.globalSettings.allowedCategories.includes(categoryID);
        } else {
          needFilter = categoryID
            ? !filesSettings.globalSettings.allowedCategories.includes(categoryID)
            : !this.canAddFiles;
        }
      } else {
        needFilter = !this.canAddFiles;
      }

      if (needFilter) {
        const filterFunc = categoryID
          ? function (x: KrPermissionsFileExtensionSettings): boolean {
              return (
                (x.addAllowed && !x.disallowedCategories.includes(categoryID)) ||
                x.allowedCategories.includes(categoryID)
              );
            }
          : function (x: KrPermissionsFileExtensionSettings): boolean {
              return x.addAllowed;
            };

        filesSettings?.tryGetExtensionSettings()?.forEach((value, key) => {
          if (filterFunc(value)) {
            allowedExtensions.add(key);
          }
        });

        allowedExtensions.add(getExtension(context.replaceFile.initialState.name));
      }
    }

    if (allowedExtensions.size > 0) {
      context.selectFileDialogAccept = '.' + [...allowedExtensions].join(', .');
    }
  }

  private async categoryFilter(
    context: IFileCategoryFilterContext
  ): Promise<(FileCategory | null)[]> {
    if (!this.ownFilesSettings && !this.otherFilesSettings) {
      return context.categories;
    }

    const ownFiles: FileInfo[] = [];
    const otherFiles: FileInfo[] = [];

    for (const fileInfo of context.fileInfos) {
      if (this.isOwnFile(fileInfo.file)) {
        ownFiles.push(fileInfo);
      } else {
        otherFiles.push(fileInfo);
      }
    }

    const checkExtensionSettings: KrPermissionsFileExtensionSettings[] = [];

    if (this.ownFilesSettings && ownFiles.length > 0) {
      const extensionsSettings = this.ownFilesSettings.tryGetExtensionSettings();
      if (extensionsSettings) {
        const extensions = ownFiles.map(x => getExtension(x.fileName));

        for (const extension of extensions) {
          const extensionSettings = extensionsSettings.tryGet(extension);
          if (extensionSettings) {
            checkExtensionSettings.push(extensionSettings);
          }
        }

        if (this.ownFilesSettings.globalSettings) {
          checkExtensionSettings.push(this.ownFilesSettings.globalSettings);
        }
      }
    }

    if (this.otherFilesSettings && otherFiles.length > 0) {
      const extensionsSettings = this.otherFilesSettings.tryGetExtensionSettings();
      if (extensionsSettings) {
        const extensions = otherFiles.map(x => getExtension(x.fileName));

        for (const extension of extensions) {
          const extensionSettings = extensionsSettings.tryGet(extension);
          if (extensionSettings) {
            checkExtensionSettings.push(extensionSettings);
          }
        }

        if (this.otherFilesSettings.globalSettings) {
          checkExtensionSettings.push(this.otherFilesSettings.globalSettings);
        }
      }
    }

    const result = context.categories.filter(category => {
      let result = this.canAddFiles;
      for (const extensionSettings of checkExtensionSettings) {
        const categoryAllowed = this.isCategoryAllowed(extensionSettings, category);
        if (categoryAllowed === false) {
          return false;
        } else if (categoryAllowed === true) {
          result = true;
        }
      }

      return result;
    });

    // Если ручной ввод разрешен и есть хотя бы одна запрещённая категория для добавления, то запрещаем ручной ввод категории
    if (!context.isManualCategoriesCreationDisabled && context.categories.length != result.length) {
      context.isManualCategoriesCreationDisabled = true;
    }

    return result;
  }

  private validateFileCategory(context: IFileValidateCategoryContext): void {
    const filesSettings = this.selectFilesSettings(context.fileInfo.file);
    if (!filesSettings) {
      return;
    }

    const fileExtension = getExtension(context.fileInfo.fileName);
    let categoryAllowed: boolean | undefined;
    let sizeChecked: boolean | undefined;
    let sizeLimit = 0;

    const extensionSettings = filesSettings.tryGetExtensionSettings()?.tryGet(fileExtension);
    if (extensionSettings) {
      categoryAllowed = this.isCategoryAllowed(extensionSettings, context.category);
      [sizeChecked, sizeLimit] = PermissionsControlsVisitor.checkFileSize(
        extensionSettings,
        context.category,
        context.fileInfo.fileSize
      );
    }

    const globalSettings = filesSettings.globalSettings;
    if (globalSettings) {
      categoryAllowed ??= this.isCategoryAllowed(globalSettings, context.category);
      if (sizeChecked === undefined) {
        [sizeChecked, sizeLimit] = PermissionsControlsVisitor.checkFileSize(
          globalSettings,
          context.category,
          context.fileInfo.fileSize
        );
      }
    }

    if (!(categoryAllowed ?? this.canAddFiles)) {
      addFileValidationError(
        context.validationResult,
        context.fileInfo.file
          ? KrPermissionsErrorAction.ChangeCategory
          : KrPermissionsErrorAction.AddFile,
        KrPermissionsErrorType.NotAllowed,
        context.fileInfo.fileName,
        fileExtension,
        null,
        context.category?.caption
      );
    }
    if (sizeChecked === false) {
      addFileValidationError(
        context.validationResult,
        context.fileInfo.file
          ? KrPermissionsErrorAction.ChangeCategory
          : KrPermissionsErrorAction.AddFile,
        KrPermissionsErrorType.FileTooBig,
        context.fileInfo.fileName,
        fileExtension,
        null,
        context.category?.caption,
        sizeLimit
      );
    }
  }

  private validateFileContent(context: IFileValidateContentContext): void {
    if (context.fileInfo.file) {
      this.validateFileContentReplace(context);
    } else if (this.ownFilesSettings) {
      this.validateFileContentCore(context, false);
    }
  }

  private validateFileContentReplace(context: IFileValidateContentContext): void {
    const file = context.fileInfo.file!;
    const fileExtension = getExtension(context.fileInfo.fileName);
    const oldFileExtension = getExtension(file?.initialState.name);
    const fileCategoryID = PermissionsControlsVisitor.getCategoryID(file.category);
    const oldFileCategoryID = PermissionsControlsVisitor.getCategoryID(file.initialState.category);

    // Если при замене файла расширение или категория файла не поменялось, то проверяем только размер файла по настройкам конкретного файла, если они заданы.
    // Иначе делаем проверку по настройкам добавления файла.
    if (fileExtension === oldFileExtension && fileCategoryID === oldFileCategoryID) {
      const replaceFileSettings = this.fileSettings?.find(x => x.id == context.fileInfo.file?.id);

      if (
        replaceFileSettings?.fileSize &&
        context.fileInfo.fileSize >= replaceFileSettings.fileSize
      ) {
        addFileValidationError(
          context.validationResult,
          KrPermissionsErrorAction.ReplaceFile,
          KrPermissionsErrorType.FileTooBig,
          context.fileInfo.fileName,
          fileExtension,
          null,
          file.category?.caption,
          replaceFileSettings.fileSize
        );
      }
    } else {
      return this.validateFileContentCore(context, true);
    }
  }

  private validateFileContentCore(
    context: IFileValidateContentContext,
    checkOnFileReplace = false
  ): void {
    const filesSettings = this.selectFilesSettings(context.fileInfo.file);
    if (!filesSettings) {
      return;
    }

    const fileExtension = getExtension(context.fileInfo.fileName);
    let hasAccess = false;
    let sizeLimit = 0;
    const replaceFile = context.fileInfo.file;
    let extensionSettings: KrPermissionsFileExtensionSettings | null | undefined = filesSettings
      .tryGetExtensionSettings()
      ?.tryGet(fileExtension);

    // Если файл новый и категория не известна, значит проверяем, что есть хотя бы одна возможная настройка добалвения файла.
    if (!replaceFile) {
      if (
        !extensionSettings ||
        (!extensionSettings.addAllowed &&
          !extensionSettings.addDisallowed &&
          extensionSettings.allowedCategories.length === 0 &&
          extensionSettings.disallowedCategories.length === 0)
      ) {
        extensionSettings = filesSettings.globalSettings;
      }

      hasAccess = !extensionSettings
        ? this.canAddFiles
        : extensionSettings.addAllowed ||
          extensionSettings.allowedCategories.length > 0 ||
          (this.canAddFiles && !extensionSettings.addDisallowed);
    } else {
      let categoryAllowed: boolean | undefined;
      let sizeChecked: boolean | undefined;

      const extensionSettings = filesSettings.tryGetExtensionSettings()?.tryGet(fileExtension);
      if (extensionSettings) {
        categoryAllowed = this.isCategoryAllowed(extensionSettings, replaceFile.category);
        [sizeChecked, sizeLimit] = PermissionsControlsVisitor.checkFileSize(
          extensionSettings,
          replaceFile.category,
          context.fileInfo.fileSize
        );
      }

      const globalSettings = filesSettings.globalSettings;
      if (globalSettings) {
        categoryAllowed ??= this.isCategoryAllowed(globalSettings, replaceFile.category);
        if (sizeChecked === undefined) {
          [sizeChecked, sizeLimit] = PermissionsControlsVisitor.checkFileSize(
            globalSettings,
            replaceFile.category,
            context.fileInfo.fileSize
          );
        }
      }

      // Доступ есть, если:
      // 1) Категория доступна для добавления по настройкам или, если настроек для категории нет, добавление доступно глобельно.
      // 2) При проверке ограничений размера не было превышения размера.
      hasAccess = categoryAllowed ?? (this.canAddFiles && sizeChecked != false);
    }

    if (!hasAccess) {
      let errorAction: KrPermissionsErrorAction;
      let errorType: KrPermissionsErrorType;
      if (sizeLimit) {
        errorAction = KrPermissionsErrorAction.ReplaceFile;
        errorType = KrPermissionsErrorType.FileTooBig;
      } else if (checkOnFileReplace) {
        errorAction = KrPermissionsErrorAction.ReplaceFile;
        errorType = KrPermissionsErrorType.NotAllowed;
      } else {
        errorAction = KrPermissionsErrorAction.AddFile;
        errorType = KrPermissionsErrorType.NotAllowed;
      }

      addFileValidationError(
        context.validationResult,
        errorAction,
        errorType,
        context.fileInfo.fileName,
        fileExtension,
        replaceFile?.name,
        replaceFile?.category?.caption,
        sizeLimit
      );
    }
  }

  private static checkFileSize(
    extensionSettings: KrPermissionsFileExtensionSettings,
    category: FileCategory | null,
    fileSize: number
  ): [sizeChecked: boolean | undefined, sizeLimit: number] {
    const sizeLimitSettings = extensionSettings.tryGetSizeLimitSettings();
    if (!sizeLimitSettings) {
      return [undefined, 0];
    }

    const categoryID = PermissionsControlsVisitor.getCategoryID(category);
    const fileSizeSetting = categoryID
      ? sizeLimitSettings.find(x => x.categories.includes(categoryID)) ??
        sizeLimitSettings.find(x => x.categories.length == 0)
      : sizeLimitSettings.find(x => x.categories.length == 0);

    if (fileSizeSetting) {
      return [fileSize <= fileSizeSetting.limit, fileSizeSetting.limit];
    }

    return [undefined, 0];
  }

  private isCategoryAllowed(
    extensionSettings: KrPermissionsFileExtensionSettings,
    category: FileCategory | null
  ): boolean | undefined {
    const categoryID = PermissionsControlsVisitor.getCategoryID(category);

    // Вручную введённая категория доступна только, если все категории доступны.
    if (!categoryID) {
      if (extensionSettings.addDisallowed && extensionSettings.disallowedCategories.length > 0) {
        return false;
      } else if (extensionSettings.addAllowed) {
        return true;
      }
    } else if (extensionSettings.addAllowed) {
      return !extensionSettings.disallowedCategories.includes(categoryID);
    } else if (extensionSettings.addDisallowed) {
      return extensionSettings.allowedCategories.includes(categoryID);
    } else if (extensionSettings.allowedCategories.includes(categoryID)) {
      return true;
    } else if (extensionSettings.disallowedCategories.includes(categoryID)) {
      return false;
    }

    return undefined;
  }

  private isSignAllowed(file: IFile): boolean {
    const filesSettings = this.selectFilesSettings(file);
    if (!filesSettings) {
      return this.canSignFiles;
    }

    const categoryID = PermissionsControlsVisitor.getCategoryID(file.category);
    const fileExtension = getExtension(file.name);

    const extensionSettings = filesSettings.tryGetExtensionSettings()?.tryGet(fileExtension);
    if (extensionSettings) {
      const signAllowed = PermissionsControlsVisitor.isSignAllowed(extensionSettings, categoryID);
      if (signAllowed !== null) {
        return signAllowed;
      }
    }

    const globalSettings = filesSettings.globalSettings;
    if (globalSettings) {
      return (
        PermissionsControlsVisitor.isSignAllowed(globalSettings, categoryID) ?? this.canSignFiles
      );
    }

    return this.canSignFiles;
  }

  private static isSignAllowed(
    extensionSettings: KrPermissionsFileExtensionSettings,
    categoryID: guid | null
  ): boolean | null {
    // Вручную введённая категория доступна только, если все категории доступны.
    if (!categoryID) {
      return extensionSettings.signAllowed;
    } else if (extensionSettings.signAllowed) {
      return !extensionSettings.signDisallowedCategories.includes(categoryID);
    } else if (extensionSettings.signDisallowed) {
      return extensionSettings.signAllowedCategories.includes(categoryID);
    } else if (extensionSettings.signAllowedCategories.includes(categoryID)) {
      return true;
    } else if (extensionSettings.signDisallowedCategories.includes(categoryID)) {
      return false;
    }

    return null;
  }

  private hideControl(control: IControlViewModel) {
    control.controlVisibility = Visibility.Collapsed;
  }

  private showControl(control: IControlViewModel) {
    control.controlVisibility = Visibility.Visible;
  }

  private hideBlock(block: IBlockViewModel) {
    block.blockVisibility = Visibility.Collapsed;
  }

  private showBlock(block: IBlockViewModel) {
    block.blockVisibility = Visibility.Visible;
  }

  private hideForm(_model: ICardModel, formViewModel: IFormWithBlocksViewModel) {
    if (isTopLevelForm(unseal(formViewModel.cardTypeForm))) {
      formViewModel.isCollapsed = true;
    } else {
      formViewModel.close();
    }
  }

  private makeControlMandatory(controlViewModel: IControlViewModel) {
    controlViewModel.isRequired = true;
    controlViewModel.requiredText = LocalizationManager.instance.format(
      '$KrPermissions_MandatoryControlTemplate',
      controlViewModel.caption
    );
  }

  private checkIsHidden(settings: Array<VisibilitySetting>, checkName: string): boolean | null {
    if (settings && settings.length > 0) {
      for (const setting of settings) {
        if (setting.isPattern) {
          if (checkName.match(setting.text)) {
            return setting.isHidden;
          }
        } else {
          if (setting.text.toLowerCase() === checkName.toLowerCase()) {
            return setting.isHidden;
          }
        }
      }
    }
    return null;
  }

  private static getCategoryID(category: FileCategory | null): guid | null {
    // Для файлов без категории используем постоянную константу в качестве идентификатора категории.
    return category ? category.id : noCategoryFilesCategoryId;
  }

  private selectFilesSettings(file: IFile | null): KrPermissionsFilesSettings | null | undefined {
    if (this.isOwnFile(file)) {
      return this.ownFilesSettings;
    } else {
      return this.otherFilesSettings;
    }
  }

  private isOwnFile(file: IFile | null): boolean {
    if (!file) {
      return true;
    }

    const fileSettings = this.fileSettings?.find(x => x.id === file.id);
    return !fileSettings || fileSettings.ownFile;
  }
}

export class KrExtendedPermissionsUIExtension extends CardUIExtension {
  private _disposes: Array<(() => void) | null> = [];

  public initialized(context: ICardUIExtensionContext): void {
    const token = KrToken.tryGet(context.card.info);
    // Если не используется типовое решение
    if (!token) {
      return;
    }

    const cardControlsVisitor = new PermissionsControlsVisitor(this._disposes);

    // Набор визиторов по гридам
    const visitors = new Map<GridViewModel, PermissionsControlsVisitor>();
    // Стек текущих открытых таблиц
    const parentGridStack: Array<GridViewModel> = new Array<GridViewModel>();

    // Инициализация видимости контролов по визитору
    const initModel = (initModel: ICardModel, visitor: PermissionsControlsVisitor) => {
      visitor.visit(initModel, parentGridStack);
      for (const control of initModel.controlsBag) {
        if (control instanceof GridViewModel) {
          visitors.set(control, visitor);
          control.rowInitializing.add(rowInitializaing);
          control.rowEditorClosed.add(rowClosed);
        }
      }
    };

    // Инициализация вдимости контролов по визитору при открытии строки таблицы
    const rowInitializaing = (e: GridRowEventArgs) => {
      try {
        if (e.rowModel) {
          parentGridStack.push(e.control);
          initModel(e.rowModel, visitors.get(e.control)!);
        }
      } catch (e) {
        showNotEmpty(ValidationResult.fromError(e));
      }
    };

    // Отписка от созданных подписок при закрытии строки грида
    const rowClosed = (e: GridRowEventArgs) => {
      if (e.rowModel) {
        parentGridStack.pop();
        for (const control of e.rowModel.controlsBag) {
          if (control instanceof GridViewModel) {
            visitors.delete(control);
            control.rowInitializing.remove(rowInitializaing);
            control.rowEditorClosed.remove(rowClosed);
          }
        }
      }
    };

    const extendedSettings = token.extendedCardSettings;
    const sectionSettings = extendedSettings?.getCardSettings();
    const tasksSettings = extendedSettings?.getTaskSettings();
    const visibilitySettings = extendedSettings?.getVisibilitySettings();
    const fileSettings = extendedSettings?.getFileSettings();
    const ownFilesSettings = extendedSettings?.getOwnFilesSettings();
    const otherFilesSettings = extendedSettings?.getOtherFilesSettings();

    const model = context.model;

    const uiVisibilitySettings = new VisibilitySettings();
    if (visibilitySettings) {
      uiVisibilitySettings.fill(visibilitySettings);
    }
    cardControlsVisitor.fill(
      token,
      sectionSettings,
      uiVisibilitySettings,
      fileSettings,
      ownFilesSettings,
      otherFilesSettings
    );

    initModel(model, cardControlsVisitor);
    if (tasksSettings && tasksSettings.size > 0) {
      this.modifyTask(model, tvm => {
        const taskSettings = tasksSettings.get(tvm.taskModel.cardTask!.typeId);
        if (!taskSettings) {
          return;
        }

        const taskVisitor = new PermissionsControlsVisitor(this._disposes);
        taskVisitor.fill(
          token,
          taskSettings,
          uiVisibilitySettings,
          fileSettings,
          ownFilesSettings,
          otherFilesSettings
        );

        tvm.modifyWorkspace(e => {
          initModel(e.task.taskModel, taskVisitor);
        });
      });
    }
  }

  public finalized(): void {
    for (const func of this._disposes) {
      if (func) {
        func();
      }
    }
    this._disposes.length = 0;
  }

  private modifyTask(
    model: ICardModel,
    modifyAction: (tvm: TaskViewModel, m: ICardModel) => void
  ): boolean {
    if (!(model instanceof DefaultFormTabWithTasksViewModel)) {
      return false;
    }

    for (const task of model.tasks.filter(x => x instanceof TaskViewModel)) {
      modifyAction(task, model);
    }

    return true;
  }
}
