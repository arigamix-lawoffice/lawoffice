import { Guid } from 'tessa/platform';
import { ICardUIExtensionContext, CardUIExtension } from 'tessa/ui/cards';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms/defaultFormTabWithTasksViewModel';
import { TestCardTypeID } from '../common';

/**
 * При сохранении выбранного типа карточки добавленые/измененные/подписанные файлы файлового
 * контрола задания переносятся в основную карточку.
 *
 * Дополнительная информация по данному расширению находится в документации kb_106.md.
 *
 * Результат работы расширения:
 * Для карточки "Автомобиль":
 * - добавьте файловый контрол для задачи "Тестовое согласование"
 * - создайте задачу через тайл на левой панели - “Тестовое согласование”.
 * - возьмите её в работу, добавьте файл в файловый контрол.
 * - при сохранении карточки файлы из файлового контрола задания переместятся в основную карточку.
 */
export class TaskEnableAttachFilesExampleUIExtension extends CardUIExtension {
  public async saving(context: ICardUIExtensionContext): Promise<void> {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // Если в форме с карточкой нет задач, то ничего не делаем.
    if (
      !(context.model.mainForm instanceof DefaultFormTabWithTasksViewModel) ||
      !context.model.mainForm.tasks.length
    ) {
      return;
    }

    const form = context.model.mainForm;
    // Перебираем файлы в карточках задач главной формы.
    for await (const task of form.tasks) {
      await task.taskModel.fileContainer.ensureAllContentModified();
      // Отправляем в контейнер только измененные файлы, либо те, у которых была добавлена или снята электронная подпись.
      const filesCount = task.taskModel.card.files.length;
      for (let i = 0; i < filesCount; i++) {
        const cardFile = task.taskModel.card.files[i];
        if (cardFile.hasChanges()) {
          cardFile.taskId = task.id;
          context.storeRequest.card.files.add(cardFile);
          const file = task.taskModel.fileContainer.files.find(x => x.id === cardFile.rowId)!;
          if (file) {
            await file.lastVersion.ensureContentDownloaded();
            await context.fileContainer.addFile(file);
          }
        }
      }
    }
  }
}
