import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Скрывать\показывать какой-либо элемент управления задания в зависимости от данных задания
 * для определенного типа карточки.
 *
 * Результат работы расширения:
 * При использовании типового процесса отправки задач для тестовой карточки "Автомобиль"
 * на этапе отправки проверяем поле "Комментарий" на наличие данных:
 * - Если поле "Комментарий" содержит текст, то скрываем контрол "Вернуть на роль".
 * - Если комментария нет, то показываем контрол "Вернуть на роль".
 */
export declare class HideTaskBlockUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static modifyWorkspace;
}
