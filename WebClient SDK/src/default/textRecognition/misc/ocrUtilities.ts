import { ArrayStorage } from 'tessa/platform/storage';
import { Card, CardRow, CardRowState } from 'tessa/cards';
import { ICardEditorModel, ICardModel } from 'tessa/ui/cards/interfaces';
import { IUIContext, UIContext } from 'tessa/ui';
import { OcrRequestStates } from '../misc/ocrTypes';

/**
 * Выполняет проверку наличия запросов в состояниях {@link states}.
 * @param card Карточка, в которой выполняется проверка.
 * @param states Список состояний, которые необходимо проверить.
 * @returns Возвращает `true`, если найден хотя бы один запрос для хотя бы одного состояния, иначе - `false`.
 * */
export function checkRequestsStates(card: Card, ...states: OcrRequestStates[]): boolean {
  return (
    card.sections.tryGet('OcrRequests')?.rows?.some(request => {
      const stateId = request.tryGet('StateID', -1);
      return states.includes(stateId);
    }) ?? false
  );
}

/**
 * Вспомогательный метод для получения контекста, редактора и модели карточки.
 * @returns Возвращает текущий UI-контекст, редактор карточки и модель карточки.
 */
export function getContextEditorModel(): {
  uiContext: IUIContext;
  editor: ICardEditorModel | null;
  model: ICardModel | null | undefined;
} {
  const uiContext = UIContext.current;
  const editor = uiContext.cardEditor;
  const model = editor?.cardModel;
  return { uiContext, editor, model };
}

/**
 * Выполняет очистку хранилища строк по условию с учетом их состояний.
 * @param rows Хранилище строк для очистки.
 * @param predicate Условное выражение для проверки необходимости очистки строки.
 */
export function clearStorageRows(
  rows: ArrayStorage<CardRow>,
  predicate?: (row: CardRow) => boolean
): void {
  for (let i = rows.length - 1; i >= 0; --i) {
    const row = rows[i];
    if (predicate ? predicate(row) : true) {
      if (row.state === CardRowState.Inserted) {
        rows.remove(row);
      } else {
        row.state = CardRowState.Deleted;
      }
    }
  }
}
