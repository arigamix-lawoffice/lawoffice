import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
import { Guid, createTypedField, DotNetType } from 'tessa/platform';
import { CardTaskAction } from 'tessa/cards';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { tryGetFromInfo } from 'tessa/ui';
import { TestCardTypeID } from './common';

/**
 * Закрывает карточку выбранного типа после завершения определенного типа задания.
 *
 * Результат работы расширения:
 * В карточке "Автомобиль":
 * - Создайте задачу через тайл на левой панели - “Тестовое согласование”.
 * - Затем возьмите её в работу, выберите один из существующих вариантов. При выборе варианта
 * задача завершится и карточка закроется.
 */
export class CloseCardOnCompleteTaskStoreExtension extends CardStoreExtension {
  private _taskComplete = false;

  public beforeRequest(context: ICardStoreExtensionContext): void {
    const card = context.request.card;
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(card.typeId, TestCardTypeID)) {
      return;
    }

    // если задачи в карточке отсутствуют, то ничего не делаем
    const tasks = card.tryGetTasks();
    if (!tasks) {
      return;
    }

    // проверяем, есть ли среди существующих задач "Тестовое согласование"
    this._taskComplete = tasks.some(
      x =>
        Guid.equals(x.typeId, '929e345c-acdf-41ea-acb6-6bb308de73ae') && // TestTask1
        x.action === CardTaskAction.Complete
    );
  }

  public afterRequest(context: ICardStoreExtensionContext): void {
    if (!context.requestIsSuccessful || !context.request || !this._taskComplete) {
      return;
    }

    // добавляем флажок о закрытии в хранилище
    context.request.info['.closeCardAfterStore'] = createTypedField(true, DotNetType.Boolean);
  }
}

export class CloseCardOnCompleteTaskUIExtension extends CardUIExtension {
  public reopening(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся получить дынные хранилища
    const request = context.getRequest;
    if (!request) {
      return;
    }

    // проверяем наличие флажка о закрытии в хранилище
    const close = tryGetFromInfo(request.info, '.closeCardAfterStore', false);
    if (close) {
      const editor = context.uiContext.cardEditor;
      if (editor) {
        // закрываем карточку
        editor.closePending = true;
      }
    }
  }
}
