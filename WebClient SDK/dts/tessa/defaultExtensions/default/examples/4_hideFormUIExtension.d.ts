import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Скрывать\показывать вкладку определенной карточки в зависимости:
 * - от наличия какого-то признака в info, пришедшем с сервера.
 * - от значения какого-то справочника, загруженного в init-стриме.
 * - от данных карточки.
 *
 * Результат работы расширения:
 * Для тестовой карточки "Автомобиль" скрываем вкладку "Сравнение файлов" в зависимости от:
 * - от наличия флажка "__HideForm" в info, пришедшем с сервера.
 * - от значения справочника "HideCommentForApprove".
 * - от наличия контрола "Базовый цвет" в тестовой карточке.
 */
export declare class HideFormUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
}
