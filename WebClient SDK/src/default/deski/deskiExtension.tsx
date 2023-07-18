import {
  CipherInfo,
  DeskiManager,
  DeskiSuccessResponse,
  FileProcessingInfo,
  versionCheck
} from 'tessa/deski';
import { APP_URL } from './deskiCommon';
import {
  ApplicationExtension,
  IApplicationExtensionMetadataContext,
  MetadataStorage,
  WorkspaceStorage
} from 'tessa';
import {
  MenuAction,
  showLoadingOverlay,
  showNotEmpty,
  showWarning,
  tryGetFromInfo,
  tryGetFromSettings,
  UIButton
} from 'tessa/ui';
import {
  FileControlExtension,
  FileExtension,
  FileExtensionContext,
  FileVersionExtension,
  FileVersionExtensionContext,
  IFileControl,
  IFileControlExtensionContext
} from 'tessa/ui/files';
import { FileContainer, FileType, IFile, IFileVersion } from 'tessa/files';

import {
  ValidationResult,
  ValidationResultBuilder,
  ValidationResultType
} from 'tessa/platform/validation';
import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { LocalizationManager } from 'tessa/localization';
import {
  FileListViewModel,
  FileViewModel,
  ViewControlButtonPanelViewModel,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';
import Platform from 'common/platform';
import { getNameAndExtForFile } from 'common/utility';
import {
  CardFileType,
  CardTypeExtensionTypes,
  executeExtensions,
  TypeExtensionContext
} from 'tessa/cards';
import { PageLifecycleSingleton, PageLifecycleState, userSession } from 'common';
import { Visibility } from 'tessa/platform';
import { runInAction } from 'mobx';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';

const MASTER_KEYS_UPDATE_INTERVAL = 1000 * 60 * 60 * 4;
let ExtensionWasInitialized = false;

export class DeskiExtension extends ApplicationExtension {
  private _lastMasterKeysUpdate: number;

  public async afterMetadataReceived(
    _context: IApplicationExtensionMetadataContext
  ): Promise<void> {
    if (!DeskiManager.instance.deskiEnabled || Platform.isMobile()) {
      return;
    }

    ExtensionWasInitialized = true;

    (async () => {
      await DeskiManager.instance.checkDeski(true);
      const cipher: CipherInfo = MetadataStorage.instance.info.get('DeskiCipher');
      const result = await DeskiManager.instance.updateMasterKeys(cipher);
      MetadataStorage.instance.info.delete('DeskiCipher');
      if (!result.isSuccessful) {
        console.error(result.format());
        await showNotEmpty(result);
        return;
      }
      this._lastMasterKeysUpdate = Date.now();

      // каждые 5 минут проверяем время, которое прошло с последнего обновления мастер ключа
      // если сессия не истекла и прошло больше константы (4 часа), то обновляемся
      setInterval(async () => {
        const now = Date.now();
        if (
          !userSession.isExpired() &&
          now - this._lastMasterKeysUpdate >= MASTER_KEYS_UPDATE_INTERVAL
        ) {
          const result = await DeskiManager.instance.updateMasterKeys();
          if (!result.isSuccessful) {
            console.error(result.format());
            return;
          }
          this._lastMasterKeysUpdate = now;
        }
      }, 300000);
    })();
  }
}

export class DeskiFileExtension extends FileExtension {
  public openingMenu(context: FileExtensionContext): void {
    if (!DeskiManager.instance.deskiAvailable || Platform.isMobile()) {
      return;
    }

    const control = context.control;
    const singleMode = context.files.length === 1;
    const editCollapsed = !context.file.model.permissions.canEdit;
    const previewIndex = context.actions.findIndex(x => x.name === 'Preview');

    const deskiInfo = DeskiManager.instance.deskiInfo;
    const minRequiredVer = '2.1.0';
    const isWordSupported = deskiInfo?.OS
      ? deskiInfo?.OS === 'windows'
      : window.navigator?.platform?.toLocaleLowerCase().includes('win');
    const isSomeNotDocFile = (files: readonly FileViewModel[]) =>
      files.some(
        x => getNameAndExtForFile(x.model.lastVersion.name).ext?.toLocaleLowerCase() !== 'docx'
      );

    const canEdit = context.file.model.permissions.canEdit;
    // скрываем "Открыть для чтения", если это новый файл или файл с заменённой версией, который доступен для редактирования;
    // т.е. показываем, если либо недоступен для редактирования, либо несколько файлов, либо есть добавленная версия
    const canRead = !canEdit || context.files.length > 1 || !context.file.model.versionAdded;

    context.actions.splice(
      previewIndex + 1,
      0,
      new MenuAction(
        'OpenForEdit',
        '$UI_Controls_FilesControl_OpenForEdit',
        'ta icon-thin-002',
        async () => {
          for (const fileVM of context.files) {
            const file = fileVM.model;
            await openFile(file, file.lastVersion, 'file', true);
          }
          control.multiSelectionMode = false;
        },
        null,
        !canEdit
      ),
      new MenuAction(
        'OpenInFolderForEdit',
        '$UI_Controls_FilesControl_OpenInFolderForEdit',
        'ta icon-thin-101',
        async () => {
          const file = context.file.model;
          await openFile(file, file.lastVersion, 'folder', true);
        },
        null,
        !singleMode || editCollapsed
      ),
      new MenuAction(
        'OpenForRead',
        '$UI_Controls_FilesControl_OpenForRead',
        'ta icon-thin-008',
        async () => {
          for (const fileVM of context.files) {
            const file = fileVM.model;
            await openFile(file, file.lastVersion, 'file', false);
          }
          control.multiSelectionMode = false;
        },
        null,
        !canRead
      ),
      new MenuAction(
        'OpenInFolderForRead',
        '$UI_Controls_FilesControl_OpenInFolderForRead',
        'ta icon-thin-101',
        async () => {
          const file = context.file.model;
          await openFile(file, file.lastVersion, 'folder', false);
        },
        null,
        !singleMode || !editCollapsed
      ),

      new MenuAction(
        'MergeWithFollowingInWord',
        '$UI_Controls_FilesControl_MergeWithFollowingInWord',
        'ta icon-thin-359',
        async () => {
          if (!(await versionCheck(minRequiredVer))) {
            return;
          }
          const file = context.file.model;

          await processMassFiles(
            file,
            file.lastVersion,
            context.files.map(x => x.model.lastVersion),
            DeskiManager.instance.mergeFiles.bind(DeskiManager.instance),
            true
          );
        },
        null,
        singleMode || editCollapsed || !isWordSupported || isSomeNotDocFile(context.files)
      ),
      new MenuAction(
        'CompareInWord',
        '$UI_Controls_FilesControl_CompareInWord',
        'ta icon-thin-420',
        async () => {
          if (!(await versionCheck(minRequiredVer))) {
            return;
          }
          const file = context.file.model;
          await processMassFiles(
            file,
            file.lastVersion,
            context.files.map(x => x.model.lastVersion),
            DeskiManager.instance.compareFiles.bind(DeskiManager.instance),
            false
          );
        },
        null,
        singleMode ||
          !isWordSupported ||
          context.files.length > 2 ||
          isSomeNotDocFile(context.files)
      ),
      new MenuAction(
        'CopyToClipboard',
        '$UI_Controls_FilesControl_CopyToClipboard',
        'ta icon-thin-063',
        async () => {
          if (!(await versionCheck(minRequiredVer))) {
            return;
          }
          const file = context.file.model;
          await processMassFiles(
            file,
            file.lastVersion,
            context.files.map(x => x.model.lastVersion),
            (appUrl, source, otherFiles) =>
              DeskiManager.instance.copyToClipboard(appUrl, [source, ...otherFiles]),
            false,
            true
          );
        },
        null
      )
    );
  }
}

export class DeskiFileControlExtension extends FileControlExtension {
  //#region FileControlExtension

  public initializing(context: IFileControlExtensionContext): void {
    const control = context.control;
    const isHidden =
      !control.fileContainer.permissions.canAdd || !DeskiManager.instance.deskiAvailable;

    const pasteFromClipboardButton = UIButton.create({
      icon: 'ta icon-thin-050',
      name: 'PasteFromClipboard',
      className: `button-plain files-control-button`,
      buttonAction: () => DeskiFileControlExtension.pasteFromClipboardAction(control),
      tooltip: '$UI_Controls_FilesControl_PasteFromClipboard',
      visibility: isHidden ? Visibility.Collapsed : Visibility.Visible
    });

    runInAction(() => {
      control.fileControlButtons.push(pasteFromClipboardButton);
    });
  }

  public openingMenu(context: IFileControlExtensionContext): void {
    if (!DeskiManager.instance.deskiAvailable || Platform.isMobile()) {
      return;
    }

    const control = context.control;

    let index = 0;
    const uploadIndex = context.actions.findIndex(x => x.name === 'Upload');
    if (uploadIndex > -1) {
      index = uploadIndex + 1;
    }

    context.actions.splice(
      index,
      0,
      new MenuAction(
        'PasteFromClipboard',
        '$UI_Controls_FilesControl_PasteFromClipboard',
        'ta icon-thin-050',
        () => DeskiFileControlExtension.pasteFromClipboardAction(control),
        null,
        !context.control.fileContainer.permissions.canAdd
      )
    );
  }

  public static async pasteFromClipboardAction(control: IFileControl): Promise<void> {
    const minRequiredVer = '2.1.0';

    if (!(await versionCheck(minRequiredVer))) {
      return;
    }
    const contents = await pasteFromClipboard();
    if (contents.length === 0) {
      return;
    }

    await control.handleDropFiles(contents);
  }
}

// В данном расширении добавляются кнопки дески в контролы представления с расширением на файлы
export class DeskiViewFileControlExtension extends CardUIExtension {
  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      CardTypeExtensionTypes.initializeFilesView,
      context.card,
      context.model.generalMetadata,
      this.executeInitializedAction,
      context
    );

    context.validationResult.add(result);
  }

  private executeInitializedAction = async (context: TypeExtensionContext) => {
    const uiContext = context.externalContext as ICardUIExtensionContext;
    const settings = context.settings;
    const filesViewAlias = tryGetFromSettings<string>(settings, 'FilesViewAlias', '');
    if (!filesViewAlias) {
      return;
    }
    if (!context.cardTask) {
      this.initializeViewControl(uiContext.model, filesViewAlias);
    } else {
      const model = uiContext.model;
      const tasks = (model.mainForm as DefaultFormTabWithTasksViewModel).tasks;
      if (!tasks) {
        return;
      }
      const task = tasks.find(x => x.taskModel.cardTask === context.cardTask);
      if (task) {
        task.modifyWorkspace(async () => {
          this.initializeViewControl(task.taskModel, filesViewAlias);
        });
      }
    }
  };

  private initializeViewControl(cardModel: ICardModel, viewControlAlias: string) {
    const viewControlViewModel = cardModel.controls.get(viewControlAlias) as ViewControlViewModel;
    if (!viewControlViewModel) {
      return;
    }

    const fileControl = tryGetFromInfo<FileListViewModel | null>(
      cardModel.info,
      viewControlAlias,
      null
    );
    if (!fileControl) {
      return;
    }

    const permissions = fileControl.fileContainer.permissions;

    const pasteFromClipboardButton = UIButton.create({
      icon: 'ta icon-thin-050',
      name: 'PasteFromClipboard',
      className: `button-plain files-control-button`,
      buttonAction: () => DeskiFileControlExtension.pasteFromClipboardAction(fileControl),
      tooltip: '$UI_Controls_FilesControl_PasteFromClipboard',
      visibility:
        DeskiManager.instance.deskiAvailable && permissions.canAdd
          ? Visibility.Visible
          : Visibility.Collapsed
    });

    const bottomPanelButtons = viewControlViewModel.bottomItems.find(
      x => x instanceof ViewControlButtonPanelViewModel
    ) as ViewControlButtonPanelViewModel;

    if (!bottomPanelButtons) {
      return;
    }

    runInAction(() => {
      bottomPanelButtons.buttons.push(pasteFromClipboardButton);
    });
  }
}

export class DeskiFileVersionExtension extends FileVersionExtension {
  public static showOpenForReadWarningMessage = true;

  public openingMenu(context: FileVersionExtensionContext): void {
    if (!DeskiManager.instance.deskiAvailable || Platform.isMobile()) {
      return;
    }

    const control = context.control;
    const singleMode = context.versions.length === 1;
    const editCollapsed = !context.file.model.permissions.canEdit;
    const downloadIndex = context.actions.findIndex(x => x.name === 'VersionDownload');

    const deskiInfo = DeskiManager.instance.deskiInfo;
    const minRequiredVer = '2.1.0';
    const isWordSupported = deskiInfo?.OS
      ? deskiInfo?.OS === 'windows'
      : window.navigator?.platform?.toLocaleLowerCase().includes('win');
    const IsSomeNotDocVersion = (versions: readonly IFileVersion[]) =>
      versions.some(x => getNameAndExtForFile(x.name).ext?.toLocaleLowerCase() !== 'docx');

    context.actions.splice(
      downloadIndex + 1,
      0,
      new MenuAction(
        'OpenForRead',
        '$UI_Controls_FilesControl_OpenForRead',
        'ta icon-thin-008',

        async () => {
          for (const version of context.versions) {
            // При открытии файла на редактирование и при открытии его последней версии на чтение,
            // их контент будет отличаться после объединения файлов в Word.
            // После сохранения карточки, открывается корректно.
            // https://gitlab.syntellect.ru/tessa_team/tessa/-/issues/2057
            if (
              DeskiFileVersionExtension.showOpenForReadWarningMessage &&
              version.file.versionAdded &&
              version.file.versionAdded.id === version.id &&
              version.file.isDirty
            ) {
              const contentInfo = await DeskiManager.instance.getContentInfo(APP_URL, version.id);
              if (contentInfo.isCached) {
                await showWarning('$Deski_Warning_OpenForRead');
              }
            }

            await openFile(context.file.model, version, 'file', false);
          }
          control.multiSelectionMode = false;
        }
      ),
      new MenuAction(
        'OpenInFolderForRead',
        '$UI_Controls_FilesControl_OpenInFolderForRead',
        'ta icon-thin-101',
        async () => {
          const file = context.file.model;
          for (const version of context.versions) {
            await openFile(file, version, 'folder', false);
          }
        },
        null,
        !singleMode || !editCollapsed
      ),

      new MenuAction(
        'MergeNewVersionWithFollowingInWord',
        '$UI_Controls_FilesControl_MergeNewVersionWithFollowingInWord',
        'ta icon-thin-359',
        async () => {
          if (
            context.versions.length === 1 &&
            context.version.id === context.file.model.lastVersion.id
          ) {
            return;
          }

          if (!(await versionCheck(minRequiredVer))) {
            return;
          }

          const file = context.file.model;
          // сохраняет ссылку на последнюю версию до file.revert()
          const lastVersion = file.lastVersion;
          const result = await context.version.ensureContentDownloaded();
          if (!result.isSuccessful) {
            await showNotEmpty(result);
            return;
          }

          const content = context.version.content;
          if (!content) {
            await showNotEmpty(
              ValidationResult.fromText('$Deski_Cant_Load_Content', ValidationResultType.Error)
            );
            return;
          }

          //выбираем контекстную версию не последнюю добавленную
          if (file.versionAdded && file.versionAdded.id !== context.version.id) {
            const resultRevert = await file.revert();
            if (!resultRevert.isSuccessful) {
              await showNotEmpty(resultRevert);
              return;
            }

            const contentInfo = await DeskiManager.instance.getContentInfo(APP_URL, lastVersion.id);
            if (contentInfo.result) {
              await showNotEmpty(contentInfo.result);
              return;
            }
            if (contentInfo.isCached) {
              const resultEditableRemove = await DeskiManager.instance.removeFile(
                APP_URL,
                lastVersion.id,
                true
              );
              if (!resultEditableRemove.success) {
                await showNotEmpty(resultEditableRemove.result);
                return;
              }
            }
            //контент в lastversion должен совпадать с контекстным
            const resultCacheContent = await DeskiManager.instance.cacheContent(
              APP_URL,
              lastVersion.id,
              context.version.name,
              content
            );
            if (!resultCacheContent.success) {
              await showNotEmpty(resultCacheContent.result);
              return;
            }
          } else if (file.versionAdded && file.versionAdded.id === context.version.id) {
            const contentInfo = await DeskiManager.instance.getContentInfo(APP_URL, lastVersion.id);
            if (contentInfo.result) {
              await showNotEmpty(contentInfo.result);
              return;
            }
            if (!contentInfo.isCached) {
              const result = await DeskiManager.instance.cacheContent(
                APP_URL,
                lastVersion.id,
                lastVersion.name,
                content
              );
              if (!result.success) {
                await showNotEmpty(result.result);
                return;
              }
            }
          }

          const fileNameIsChanged = file.name !== content.name;
          file.replace(content, fileNameIsChanged);

          // версии должны мерджиться от первой к последней
          const versions = context.versions
            .filter(version => version.id !== context.version.id)
            .sort((a, b) => a.number - b.number);

          if (!versions.some(version => version.id === file.lastVersion.id)) {
            versions.push(file.lastVersion);
          }

          context.dialog.close();
          await showLoadingOverlay(
            async () =>
              await processMassFiles(
                file,
                file.lastVersion,
                versions,
                DeskiManager.instance.mergeFiles.bind(DeskiManager.instance),
                true
              ),
            LocalizationManager.instance.localize('$UI_Controls_FilesControl_MergeFiles')
          );
        },
        null,
        editCollapsed ||
          !isWordSupported ||
          IsSomeNotDocVersion([context.file?.model.lastVersion, ...context.versions])
      ),
      new MenuAction(
        'CompareInWord',
        '$UI_Controls_FilesControl_CompareInWord',
        'ta icon-thin-420',
        async () => {
          if (!(await versionCheck(minRequiredVer))) {
            return;
          }
          const file = context.file.model;
          await processMassFiles(
            file,
            context.version,
            context.versions,
            DeskiManager.instance.compareFiles.bind(DeskiManager.instance),
            false
          );
        },
        null,
        singleMode ||
          !isWordSupported ||
          context.versions.length > 2 ||
          IsSomeNotDocVersion(context.versions)
      ),
      new MenuAction(
        'CopyToClipboard',
        '$UI_Controls_FilesControl_CopyToClipboard',
        'ta icon-thin-063',
        async () => {
          if (!(await versionCheck(minRequiredVer))) {
            return;
          }
          const file = context.file.model;
          await processMassFiles(
            file,
            context.version,
            context.versions,
            (appUrl, source, otherFiles) =>
              DeskiManager.instance.copyToClipboard(appUrl, [source, ...otherFiles]),
            false
          );
        },
        null
      )
    );
  }
}

export class DeskiUIExtension extends CardUIExtension {
  shouldExecute(): boolean {
    return DeskiManager.instance.deskiAvailable;
  }
  public initialized(context: ICardUIExtensionContext): void {
    const fileControls = context.model.controlsBag.filter(
      x => x instanceof FileListViewModel
    ) as FileListViewModel[];
    for (const control of fileControls) {
      control.fileDoubleClickAction = async fileVM => {
        const file = fileVM.model;
        const canEdit = file.permissions.canEdit;
        await openFile(file, file.lastVersion, 'file', canEdit);
      };
    }
  }
  public async saving(context: ICardUIExtensionContext): Promise<void> {
    const files = context.fileContainer.files;
    if (!files || files.length === 0) {
      return;
    }

    const savingResult = new ValidationResultBuilder();

    const editedFiles = files.filter(x => x.isDirty);
    if (editedFiles.length) {
      for (const file of editedFiles) {
        const content = file.lastVersion.content;
        try {
          const { info: fileInfo, result: fileInfoResult } =
            await DeskiManager.instance.getFileInfo(APP_URL, file.lastVersion.id, true);

          if (fileInfoResult) {
            savingResult.add(fileInfoResult);
            continue;
          }

          if (!fileInfo) {
            savingResult.add(
              ValidationResultType.Error,
              `Can not find deski's FileInfo. File version: ${file.lastVersion.id}`
            );
            continue;
          }

          if (fileInfo!.IsLocked) {
            savingResult.add(
              ValidationResultType.Error,
              LocalizationManager.instance.format('$UI_Cards_FileSaving_Locked', fileInfo.Name)
            );
            continue;
          }

          if (!fileInfo.IsModified) {
            await removeFile(file);
            file.isDirty = false;
            continue;
          }

          const cardFile = context.card.files.find(x => x.rowId === file.id);
          if (!cardFile) {
            savingResult.add(
              ValidationResultType.Error,
              `Can not find file in card storage. File: ${file.id}`
            );
            continue;
          }

          const cacheResult = await DeskiManager.instance.cacheModFileWithNewId(
            APP_URL,
            fileInfo.ID,
            cardFile.versionRowId
          );
          if (!cacheResult.success) {
            savingResult.add(cacheResult.result);
          }

          const result = await file.lastVersion.ensureContentDownloaded();
          if (!result.isSuccessful) {
            savingResult.add(result);
            continue;
          }

          if (!content) {
            savingResult.add(
              ValidationResult.fromText('$Deski_Cant_Load_Content', ValidationResultType.Error)
            );
            continue;
          }

          // TODO: Возможно стоит сделать доработку со стороны deski для cacheModFileWithNewId, чтобы он мог менять имя файла.
          // Т.к. при изменении имени файла, имя берется из текущей lastVersion.
          // После доработки код ниже нужно убрать.
          if (file.versionAdded) {
            file.versionAdded = null;
          }
          file.replace(content, false);
        } catch (error) {
          savingResult.add(ValidationResult.fromError(error));
        }
      }
    }

    const result = savingResult.build();
    if (result.items.length > 0) {
      console.error(result.format());
    }
  }
  public async finalized(context: ICardUIExtensionContext): Promise<void> {
    await clearEditedFiles(context.fileContainer);
  }
}

async function openFile(
  file: IFile,
  version: IFileVersion,
  mode: 'file' | 'folder',
  editable: boolean
): Promise<void> {
  if (!(await DeskiManager.instance.checkDeski())) {
    return;
  }
  let result = await DeskiManager.instance.setAppInfo(APP_URL, DeskiManager.instance.masterKeys);
  if (!result.success) {
    await showNotEmpty(result.result);
    return;
  }

  const contentInfo = await DeskiManager.instance.getContentInfo(APP_URL, version.id);
  if (contentInfo.result) {
    await showNotEmpty(contentInfo.result);
    return;
  }
  if (!contentInfo.isCached || isVirtualFile(file.type)) {
    if (isVirtualFile(file.type)) {
      await DeskiManager.instance.removeFile(APP_URL, version.id, editable);
    }

    const validationResult = await version.ensureContentDownloaded();
    if (!validationResult.isSuccessful) {
      await showNotEmpty(validationResult);
      return;
    }
    const content = version.content;

    if (!content) {
      await showNotEmpty(
        ValidationResult.fromText('$Deski_Cant_Load_Content', ValidationResultType.Error)
      );
      return;
    }
    result = await DeskiManager.instance.cacheContent(APP_URL, version.id, version.name, content);
    if (!result.success) {
      await showNotEmpty(result.result);
      return;
    }
  }

  result = await DeskiManager.instance.openFile(APP_URL, version.id, mode, editable);
  if (!result.success) {
    await showNotEmpty(result.result);
    return;
  }

  if (editable) {
    file.isDirty = true;
  }
}

async function processMassFiles(
  sourceFile: IFile,
  sourceVersion: IFileVersion,
  fileVersions: readonly IFileVersion[],
  func: (
    appUrl: string,
    source: FileProcessingInfo,
    otherFiles: FileProcessingInfo[]
  ) => DeskiSuccessResponse,
  editable = false,
  useFileName = false
): Promise<void> {
  if (!(await DeskiManager.instance.checkDeski())) {
    return;
  }

  const forProcessingFiles: FileProcessingInfo[] = [];
  for (const version of fileVersions) {
    let result = await DeskiManager.instance.setAppInfo(APP_URL, DeskiManager.instance.masterKeys);
    if (!result.success) {
      await showNotEmpty(result.result);
      return;
    }
    const contentInfo = await DeskiManager.instance.getContentInfo(APP_URL, version.id);
    if (contentInfo.result) {
      await showNotEmpty(contentInfo.result);
      return;
    }
    if (!contentInfo.isCached) {
      const validationResult = await version.ensureContentDownloaded();
      if (!validationResult.isSuccessful) {
        await showNotEmpty(validationResult);
        return;
      }
      const content = version.content;

      if (!content) {
        await showNotEmpty(
          ValidationResult.fromText('$Deski_Cant_Load_Content', ValidationResultType.Error)
        );
        return;
      }
      result = await DeskiManager.instance.cacheContent(
        APP_URL,
        version.id,
        useFileName ? version.file.name : version.name,
        content
      );
      if (!result.success) {
        await showNotEmpty(result.result);
        return;
      }
    }
    if (version.id !== sourceVersion.id) {
      forProcessingFiles.push({ id: version.id, author: version.createdByName });
    }
  }

  const result = await func(
    APP_URL,
    { id: sourceVersion.id, author: sourceVersion.createdByName },
    forProcessingFiles
  );
  if (!result.success) {
    await showNotEmpty(result.result);
    return;
  }

  if (editable) {
    sourceFile.isDirty = true;
  }
}

async function pasteFromClipboard(): Promise<File[]> {
  if (!(await DeskiManager.instance.checkDeski())) {
    return [];
  }

  const result = await DeskiManager.instance.pasteFromClipboard(APP_URL);
  if (result.result) {
    await showNotEmpty(result.result);
    return [];
  }
  return result.files;
}

// при закрытии вкладки или обновлении страницы вручную надо удалить открытые файлы
PageLifecycleSingleton.instance.addCallback(async function () {
  if (!ExtensionWasInitialized || !DeskiManager.instance.deskiAvailable) {
    return;
  }

  const cards = WorkspaceStorage.instance.cards;
  if (cards.size === 0) {
    return;
  }

  for (const card of cards.values()) {
    if (card.editor.cardModel) {
      await clearEditedFiles(card.editor.cardModel.fileContainer, true);
    }
  }
}, PageLifecycleState.terminated);

const clearEditedFiles = async (fileContainer: FileContainer, sendSynchronously = false) => {
  const files = fileContainer.files;
  if (!files) {
    return;
  }

  const editedFiles = files.filter(x => x.isDirty);
  if (editedFiles.length === 0) {
    return;
  }

  for (const file of editedFiles) {
    try {
      await removeFile(file, sendSynchronously);
    } catch (error) {
      console.error(error);
    }
  }
};

const removeFile = async (file: IFile, sendSynchronously = false) => {
  await DeskiManager.instance.removeFile(APP_URL, file.lastVersion.id, true, sendSynchronously);
};

const isVirtualFile = (fileType: FileType | null): boolean => {
  if (fileType && fileType instanceof CardFileType) {
    return fileType.isVirtual;
  }

  return false;
};
