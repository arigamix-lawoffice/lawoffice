import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { OnlyOfficeApiSingleton } from './onlyOfficeApiSingleton';
import { OnlyOfficeOpenFileInfo } from './onlyOfficeOpenFileInfo';
import { LocalizationManager } from 'tessa/localization';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';
import { IFile } from 'tessa/files';
import { showNotEmpty } from 'tessa/ui';
import { OnlyOfficeApi } from './onlyOfficeApi';
import { OnlyOfficeEditorPreviewViewModel } from './onlyOfficeEditorPreviewViewModel';
import { IFileControlManager } from 'tessa/ui/files';
import { FilePreviewViewModel } from 'tessa/ui/cards/controls';

/**
 * Представляет собой расширение, которое устанавливает в качестве средства предпросмотра OnlyOffice
 * и предупреждает пользователя об открытых файлах на редактирование при операциях с карточкой.
 */
export class OnlyOfficeCardUIExtension extends CardUIExtension {
  public initializing(context: ICardUIExtensionContext): void {
    OnlyOfficeCardUIExtension.setUpOnlyOfficePreviewFactory(context.model.previewManager);

    for (const control of context.model.controls.values()) {
      if (control instanceof FilePreviewViewModel) {
        if (control.fileControlManager !== context.model.previewManager) {
          OnlyOfficeCardUIExtension.setUpOnlyOfficePreviewFactory(control.fileControlManager);
        }
      }
    }
  }

  public reopening(context: ICardUIExtensionContext): void {
    const res = OnlyOfficeCardUIExtension.checkOpenFiles(
      context.fileContainer.files,
      context.card.id
    );

    if (res.hasErrors) {
      context.validationResult.add(res);
    }
  }

  public saving(context: ICardUIExtensionContext): void {
    const res = OnlyOfficeCardUIExtension.checkOpenFiles(
      context.fileContainer.files,
      context.card.id
    );

    if (res.hasErrors) {
      context.validationResult.add(res);
    }
  }

  public async finalizing(context: ICardUIExtensionContext): Promise<void> {
    const res = OnlyOfficeCardUIExtension.checkOpenFiles(
      context.fileContainer.files,
      context.card.id
    );

    if (res.hasErrors) {
      context.cancel = true;
      await showNotEmpty(res);
    }
  }

  private static checkOpenFiles(files: ReadonlyArray<IFile>, cardId: guid): ValidationResult {
    const openFilesForEdit: OnlyOfficeOpenFileInfo[] = [];

    for (const file of files) {
      for (const version of file.versions) {
        const openedFileInfo = OnlyOfficeApiSingleton.instance.openFiles.find(
          of => of.forEdit && of.version.id === version.id && of.cardId === cardId
        );

        if (openedFileInfo) {
          openFilesForEdit.push(openedFileInfo);
        }
      }
    }

    if (openFilesForEdit.length === 0) return ValidationResult.empty;

    const openFileNames = openFilesForEdit.map(of => of.version.file.name).join('\n');
    return ValidationResult.fromText(
      LocalizationManager.instance.format(
        '$OnlyOffice_Error_CloseEditableFilesBeforeContinue',
        openFileNames
      ),
      ValidationResultType.Error
    );
  }

  private static setUpOnlyOfficePreviewFactory(manager: IFileControlManager): void {
    const existingFactory = manager.previewToolFactory;

    manager.previewToolFactory = version => {
      const fileExt = version.getExtension();

      const canUseOnlyOffice =
        OnlyOfficeApiSingleton.instance.settings.previewEnabled &&
        OnlyOfficeApi.isSupportedFormat(fileExt) &&
        !OnlyOfficeApiSingleton.instance.settings.excludedPreviewFormats.includes(fileExt);

      if (canUseOnlyOffice) {
        return {
          type: OnlyOfficeEditorPreviewViewModel.type,
          createViewModelFunc: () =>
            new OnlyOfficeEditorPreviewViewModel(OnlyOfficeApiSingleton.instance, e => {
              manager.message = {
                main: LocalizationManager.instance.localize('$UI_Controls_Preview_NotAvailable'),
                additional: e.message
              };
            })
        };
      }

      return existingFactory(version);
    };
  }

  public shouldExecute(): boolean {
    return OnlyOfficeApiSingleton.isAvailable;
  }
}
