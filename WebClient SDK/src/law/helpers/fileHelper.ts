import { FileCategory, IFile } from 'tessa/files';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { showNotEmpty } from 'tessa/ui';
import { ViewControlViewModel, validateFileContent } from 'tessa/ui/cards/controls';
import { FileInfo, IFileControl } from 'tessa/ui/files';
import { InfoMarks } from '../infoMarks';
import { DotNetType, createTypedField } from 'tessa/platform';
import { TypeInfo } from '../info/typesInfo';
import { ViewInfo } from '../info/viewInfo';

/**
 * Класс для работы с файлами.
 */
export class FileHelper {
  /**
   * Загрузить файлы.
   * @param control Файловый контрол.
   * @returns Асинхронная задача.
   */
  public static async addFilesAsync(control: IFileControl, files: ReadonlyArray<File>): Promise<void> {
    const container = control.fileContainer;
    let fileInfos = files.map(content => new FileInfo(content));
    if (control.validateFileContent) {
      const totalValidationResult = new ValidationResultBuilder();
      for (let i = 0; i < fileInfos.length; i++) {
        const contentValidationResult = await validateFileContent(control, fileInfos[i]);
        totalValidationResult.add(contentValidationResult);
        if (!contentValidationResult.isSuccessful) {
          fileInfos.splice(i--, 1);
        }
      }

      showNotEmpty(totalValidationResult.build());
      if (fileInfos.length == 0) {
        return;
      }
    }

    const foldersViewControl = control.model.controls.get(TypeInfo.LawCase.FoldersView) as ViewControlViewModel;
    if (!foldersViewControl) {
      return;
    }

    const folderRowID = foldersViewControl.selectedRow?.get(ViewInfo.LawFolders.ColumnRowID.Alias);
    const folderName = foldersViewControl.selectedRow?.get(ViewInfo.LawFolders.ColumnName.Alias);
    const fileType = foldersViewControl.selectedRow?.get(ViewInfo.LawFolders.ColumnType.Alias);
    if (!folderRowID || !folderName || !fileType) {
      return;
    }

    const addingFiles: IFile[] = [];
    for (let i = 0; i < fileInfos.length; i++) {
      const fileInfo = fileInfos[i];
      const file = container.createFile(fileInfo.content!, null, new FileCategory(folderRowID, folderName));
      const date = new Date(fileInfo.content!.lastModified);
      file.options[InfoMarks.Extension] = createTypedField(file.getExtension().toUpperCase() + ' File', DotNetType.String);
      file.options[InfoMarks.Classification] = createTypedField(fileType, DotNetType.String);
      file.options[InfoMarks.ReservedBy] = createTypedField('', DotNetType.String);
      file.options[InfoMarks.Added] = createTypedField(new Date().toISOString(), DotNetType.DateTime);
      file.options[InfoMarks.Created] = createTypedField(date.toISOString(), DotNetType.DateTime);
      addingFiles.push(file);
    }

    for (const file of addingFiles) {
      await container.addFile(file);
    }
  }
}
