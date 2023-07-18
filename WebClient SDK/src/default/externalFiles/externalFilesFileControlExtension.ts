import { ExternalFile } from './externalFile';
import { FileSourceGrouping } from './fileSourceGrouping';
import { ExternalFileSource } from './externalFileSource';
import { ExternalFileCreationToken } from './externalFileCreationToken';
import { FileControlExtension, IFileControlExtensionContext } from 'tessa/ui/files';
import { ICardModel } from 'tessa/ui/cards';
import { UIContext, MenuAction, IUIContext } from 'tessa/ui';
import { Guid } from 'tessa/platform';
import { userSession } from 'common/utility';
import { CardFileType } from 'tessa/cards';
import { IFile, FileVersionState } from 'tessa/files';

export class ExternalFilesFileControlExtension extends FileControlExtension {
  public openingMenu(context: IFileControlExtensionContext) {
    const editor = UIContext.current.cardEditor;
    let model: ICardModel;
    if (
      editor == null ||
      !(model = editor.cardModel!) ||
      model.cardType.name !== 'Car' ||
      model.inSpecialMode
    ) {
      return;
    }

    const isContainsExternalFiles = context.control.files.some(
      p => p.model instanceof ExternalFile
    );

    // Добавляем группировку/фильтр
    if (context.groupings.find(p => p.name === 'Source') == null && isContainsExternalFiles) {
      context.groupings.push(new FileSourceGrouping('Source', '$KrTest_GroupingBySource'));
    }

    let index = 0;
    const downloadIndex = context.actions.findIndex(x => x.name === 'Upload');
    if (downloadIndex > -1) {
      index = downloadIndex;
    }

    // Добавляем пункт меню для получения файлов
    context.actions.splice(
      index,
      0,
      new MenuAction(
        'GetExternalFiles',
        '$KrTest_ExternalSourceFiles',
        'icon-thin-427',
        async () => {
          if (!isContainsExternalFiles) {
            // Устанавливаем группировку
            context.control.selectedGrouping = new FileSourceGrouping(
              'Source',
              '$KrTest_GroupingBySource'
            );

            // Считываем имена и содержимое
            const filesNamesContents = [
              ['File1.txt', 'File1_text.txt'],
              ['File2.txt', 'File2_text.txt'],
              ['File3.txt', 'File3_text.txt']
            ];

            for (let file of filesNamesContents) {
              this.addFile(context, file[0], file[1], model.executeInContext);
            }
          } else {
            // Сбрасываем группировку
            context.control.selectedGrouping = null;

            // Убираем внешние файлы
            const removeFiles = context.control.files.filter(p => p.model instanceof ExternalFile);
            for (let f of removeFiles) {
              await context.control.fileContainer.removeFile(f.model, false);
            }
          }
        },
        null,
        false,
        isContainsExternalFiles
      )
    );
  }

  private async addFile(
    context: IFileControlExtensionContext,
    name: string,
    content: string,
    executor: (action: (context: IUIContext) => void) => void
  ): Promise<void> {
    const fileSource = new ExternalFileSource(userSession, executor);
    const fileToken = fileSource.getFileCreationToken();

    fileToken.id = Guid.newGuid();
    fileToken.name = name;
    fileToken.type = new CardFileType('ExternalFile', '$KrTest_ExternalFileType', null);
    (fileToken as ExternalFileCreationToken).description = content;

    const file = fileSource.createFile(fileToken);
    this.addVersion(name, Guid.newGuid(), 1, file);

    file.versionsAreComprehensive = true;

    await context.control.fileContainer.addFile(file, false, false);
  }

  private addVersion(name: string, id: guid, num: number, file: IFile) {
    const versionToken = file.source.getVersionCreationToken();

    versionToken.id = id;
    versionToken.name = name;
    versionToken.number = num;
    versionToken.state = FileVersionState.Success;

    const version = file.source.createFileVersion(versionToken, file, null);
    file.versions.push(version);
  }
}
