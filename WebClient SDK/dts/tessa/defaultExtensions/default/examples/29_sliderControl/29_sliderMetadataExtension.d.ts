import { CardMetadataExtension, ICardMetadataExtensionContext } from 'tessa/cards/extensions';
/**
 * Пример данного расширения позволяет создавать собственные контролы и добавлять
 * их в выбранную карточку.
 *
 * Результат работы расширения:
 * Создаем кастомный слайдер-контрол и добавляем его в блок "Общая информация" тестовой
 * карточки "Автомобиль".
 */
export declare class SliderMetadataExtension extends CardMetadataExtension {
    initializing(context: ICardMetadataExtensionContext): void;
}
