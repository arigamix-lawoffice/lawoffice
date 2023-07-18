import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Скрытие дефолтного тайла для выбранного типа карточки.
 *
 * Результат работы расширения:
 * Пример данного расширения скрывает тайл "Сохранить" для тестовой карточки.
 */
export declare class HideTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
}
