import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
/**
 * В этом расширении демонстрируется:
 * - Тайл-группа на левой панели.
 * - Запрос карточки с сервера.
 * - Запрос данных представления с сервера.
 * - Заполнение параметров поиска в запросе к представлению.
 * - Доступ к результату запроса представления.
 *
 * Результат работы расширения:
 * На левую панель добавляет групповой тайл - “Запросы”. По клику показываются дочерние тайлы - “Запрос карточки” и “Запрос представления”.
 * При клике на “Запрос карточки” происходит вызов сервера, получается карточка прав, и необработанное содержимое показывается в модальном окне.
 * Аналогично, с помощью CardService можно делать и другие реквесты (request, get, store и т.д.).
 * При клике на “Запрос представления” происходит вызов сервера, получаются данные списка контрагентов, и необработанное содержимое показывается в модальном окне.
 */
export declare class RequestTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static cardRequestCommand;
    private static viewRequestCommand;
}
