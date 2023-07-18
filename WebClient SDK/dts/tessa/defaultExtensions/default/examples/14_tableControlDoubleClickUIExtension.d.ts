import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Подписываемся на двойной клик в контроле таблицы выбранной карточки.
 *
 * Результат работы расширения:
 * В карточке "Автомобиль" подписываемся на двойной клик в контроле таблицы "Список Акций".
 */
export declare class TableControlDoubleClickUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
}
