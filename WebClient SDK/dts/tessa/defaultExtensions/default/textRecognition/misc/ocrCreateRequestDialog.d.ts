import { ArrayStorage } from 'tessa/platform/storage';
import { CardRow, FieldMapStorage } from 'tessa/cards';
/**
 * Выполняет создание и отображение диалога "Создание запроса на распознавание".
 * @param isMultipage Признак многостраничного файла.
 * @returns Информация по запросу, заполненная пользователем - если создание выполнено успешно, в противном случае - null.
 */
export declare function ocrCreateRequestDialog(isMultipage: boolean): Promise<{
    request: FieldMapStorage;
    languages: ArrayStorage<CardRow>;
} | null>;
/**
 * Создает строку {@link CardRow} и переносит в нее данные по запросу из {@link request}.
 * @param request Запрос на распознавание текса.
 * @returns Строку {@link CardRow} с информацией по распознаванию.
 */
export declare function ocrCreateRequestRow(request: FieldMapStorage): CardRow;
/**
 * Создает строку {@link CardRow} и переносит в нее данные по языкам запроса из {@link languages}.
 * @param languages Языки для запроса на распознавание текса.
 * @param requestId Идентификатор созданной строки с запросом на распознавание текста.
 * @returns Строку {@link CardRow} с информацией по распознаванию.
 */
export declare function ocrCreateLanguagesRows(languages: ArrayStorage<CardRow>, requestId: guid): CardRow[];
