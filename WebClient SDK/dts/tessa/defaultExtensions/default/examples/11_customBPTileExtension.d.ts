import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
/**
 * Добавление тайла запуска бизнес процесса в левую панель для определенного типа карточек.
 *
 * Результат работы расширения:
 * На левую панель для карточки "Автомобиль" добавляет тайл - “Запустить кастомный БП”. При нажатии на тайл
 * запускается БП (добавляется ключ .startProcess с значением TestProcess).
 */
export declare class CustomBPTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static startCustomBP;
    private static enableIfCard;
}
