import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
/**
 * Добавлять\cкрывать\показывать тайл в левой панели для карточек в зависимости от:
 * - типа карточки.
 * - данных карточки.
 *
 * Результат работы расширения:
 * Добавляет на левую панель тайл, который по нажатию открывает модальное окно.
 * Тайл виден только в карточке типа "Автомобиль" и при значении контрола пробег 100000 км.
 */
export declare class SimpleCardTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static showMessageBoxCommand;
    private static enableIfCardWithSubject;
}
