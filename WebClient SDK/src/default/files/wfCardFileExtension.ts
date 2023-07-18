import { FileExtension, IFileExtensionContext, IFileControl } from 'tessa/ui/files';
import { UIContext, SeparatorMenuAction, MenuAction, showNotEmpty } from 'tessa/ui';
import {
  IFile,
  checkCanDownloadFilesAndShowMessages,
  FileCategory,
  FilePermissions
} from 'tessa/files';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { unseal, createTypedField, DotNetType } from 'tessa/platform';
import { FileViewModel } from 'tessa/ui/cards/controls';

export class WfCardFileExtension extends FileExtension {
  public openingMenu(context: IFileExtensionContext) {
    const editor = UIContext.current.cardEditor;
    if (
      !editor ||
      !editor.cardModel ||
      editor.cardModel.cardType.id !== 'de75a343-8164-472d-a20e-4937819760ac' // WfTaskCardTypeID
    ) {
      return;
    }

    const control = context.control;
    const files = context.files;

    const canCopyToMainCard = files.some(
      x => !x.model.category || x.model.category.id !== 'ef065661-6613-4c87-bf93-0e1dd558a751'
    ); // MainCardCategoryID

    let index = 0;
    const downloadIndex = context.actions.findIndex(x => x.name === 'Download');
    if (downloadIndex > -1) {
      index = downloadIndex + 1;
    }

    context.actions.splice(
      index,
      0,
      new SeparatorMenuAction(!canCopyToMainCard),
      new MenuAction(
        'CopyFromTaskToMainCard',
        '$WfResolution_CopyFileFromTaskToMainCard',
        'ta icon-thin-119',
        () => WfCardFileExtension.createCopyInMainCard(control, files),
        null,
        !canCopyToMainCard
      )
    );
  }

  private static async createCopyInMainCard(
    control: IFileControl,
    files: ReadonlyArray<FileViewModel>
  ) {
    const operationFiles = files.map(x => x.model);
    if (!(await checkCanDownloadFilesAndShowMessages(operationFiles))) {
      return;
    }

    const copiedFileList: [IFile, IFile][] = [];
    const totalResult = new ValidationResultBuilder();
    for (let fileVM of files) {
      const file = fileVM.model;
      if (!file.category || file.category.id !== 'ef065661-6613-4c87-bf93-0e1dd558a751') {
        // MainCardCategoryID
        try {
          fileVM.isLoading = true;
          // TODO: не загружать контент, если не нужно.
          // сейчас не работает из-за расширений на сервере TaskSatelliteStoreExtension.ExtendedRepositoryWithoutTransaction

          // если контент уже загружен, то делаем обычное копирование
          // в противном случае используем externalSource
          // const hasContent = !!file.lastVersion.content;
          const contentResult = await file.ensureContentDownloaded();
          if (!contentResult.isSuccessful) {
            fileVM.isLoading = false;
            totalResult.add(contentResult);
            return;
          }

          // const result = file.copy(!hasContent);
          const result = file.copy();
          totalResult.add(result.validationResult);

          if (!result.validationResult.isSuccessful) {
            break;
          }

          const copiedFile = result.data!;
          // мы создаём копию, но не сохраняем ссылку на файл, из которого он был скопирован
          copiedFile.origin = null;
          copiedFile.category = new FileCategory(
            'ef065661-6613-4c87-bf93-0e1dd558a751',
            '$WfResolution_MainCardFileCategory'
          );
          unseal<FilePermissions>(copiedFile.permissions).canModifyCategory = false;
          copiedFile.info['CopiedToMainCard'] = createTypedField(true, DotNetType.Boolean);
          copiedFileList.push([copiedFile, file]);
        } finally {
          fileVM.isLoading = false;
        }
      }
    }

    const result = totalResult.build();
    await showNotEmpty(result);

    if (result.isSuccessful) {
      for (let files of copiedFileList) {
        const newFile = files[0];
        // const sourceFile = files[1];
        await control.fileContainer.addFile(newFile);

        // const hasContent = !!newFile.lastVersion.content;
        // if (!hasContent) {
        //   newFile.setExternalContent(sourceFile.lastVersion);
        // }
      }

      control.multiSelectionMode = false;
    }
  }
}
