import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Позволяет отслеживать добавление\удаление строк в коллекционной секции для определенного типа карточки.
 *
 * Результат работы расширения:
 * В тестовой карточке "Автомобиль" при добавлении или удалении строки из контрола "Список Акций"
 * секции "TEST_CarSales" в поле "Цвет" добавлется текст с соответсвующим идентификатором строки и
 * наименованием действия (добавления или удаления соответстенно).
 */
export declare class TableSectionChangedUIExtension extends CardUIExtension {
    private _listener;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
}
