import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
/**
 * Добавлять\cкрывать\показывать тайл в левой панели для выбранного представления в зависимости от:
 * - идентификатора узла рабочего места.
 * - алиаса представления.
 * - данных выделенной строки.
 *
 * Результат работы расширения:
 * Добавляет на левую панель тайл, который по нажатию открывает модальное окно
 * с данными выделенной строки представления. Добавленный тайл отображается только для
 * представления "Мои документы".
 */
export declare class SimpleViewTileExtension extends TileExtension {
    private static myDocumentsAlias;
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static showViewData;
    private static enableIfMyDocumentsViewAndHasSelectedRow;
}
