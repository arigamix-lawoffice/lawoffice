import { ArrayStorage } from 'tessa/platform/storage';
import { Card, CardRow } from 'tessa/cards';
import { ICardEditorModel, ICardModel } from 'tessa/ui/cards/interfaces';
import { IUIContext } from 'tessa/ui';
import { OcrRequestStates } from '../misc/ocrTypes';
/**
 * Выполняет проверку наличия запросов в состояниях {@link states}.
 * @param card Карточка, в которой выполняется проверка.
 * @param states Список состояний, которые необходимо проверить.
 * @returns Возвращает `true`, если найден хотя бы один запрос для хотя бы одного состояния, иначе - `false`.
 * */
export declare function checkRequestsStates(card: Card, ...states: OcrRequestStates[]): boolean;
/**
 * Вспомогательный метод для получения контекста, редактора и модели карточки.
 * @returns Возвращает текущий UI-контекст, редактор карточки и модель карточки.
 */
export declare function getContextEditorModel(): {
    uiContext: IUIContext;
    editor: ICardEditorModel | null;
    model: ICardModel | null | undefined;
};
/**
 * Выполняет очистку хранилища строк по условию с учетом их состояний.
 * @param rows Хранилище строк для очистки.
 * @param predicate Условное выражение для проверки необходимости очистки строки.
 */
export declare function clearStorageRows(rows: ArrayStorage<CardRow>, predicate?: (row: CardRow) => boolean): void;
