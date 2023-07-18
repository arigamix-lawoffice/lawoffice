import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { FileListViewModel } from 'tessa/ui/cards/controls';
import { showMessage } from 'tessa/ui/tessaDialog';
import { tryGetFromInfo } from 'tessa/ui';
import { TestCardTypeID } from './common';

/**
 * Пример данного расширения позволяет для определденного типа карточки:
 * - Фильтровать содержимое файлового контрола по определенной категории.
 * - Добавлять файлы только с определенным расширением.
 *
 * Результат работы расширения:
 * В карточке типа "Автомобиль" выполняет фильтрацию файлового контрола "Без изображений (всё, кроме категории "Image")"
 * по названию категории: отображает файлы только с категорией "Text".
 * Запрещает добавление файлов с разрешением, отличным от ".txt", для всех файлововых контролов выбранной карточки.
 */
export class FileControlUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если не карточка "автомобиль", то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся получить контрол "Без изображений (всё, кроме категории "Image")"
    const AllFilesControl = tryGetFromInfo<FileListViewModel | null>(
      context.model.info,
      'AllFilesControl',
      null
    );

    if (!AllFilesControl) {
      return;
    }

    // показываем файлы только с категорией Text
    AllFilesControl.removeFiles(
      file => !(file.model.category && file.model.category.caption === 'Text')
    );
    // если добавляется файл с расширением отличным от 'txt', то прерываем добавление
    context.fileContainer.containerFileChanging.add(async e => {
      const file = e.added;
      if (file && file.getExtension() !== 'txt') {
        await showMessage(`Вы должны приложить файл только с расширением 'txt'.`);
        e.cancel = true;
      }
    });
  }
}
