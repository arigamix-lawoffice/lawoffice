import { TabPanelUIExtension, ITabPanelUIExtensionContext } from 'tessa/ui/tabPanel';
/**
 * Добавляем дополнительную кнопку на панель с табами приложения.
 * При нажатии на кнопку открываем виртуальную карточку.
 *
 * Результат работы расширения:
 * Пример данного расширения добавляет дополнительную кнопку на панель с табами, при нажатии на которую
 * открывается виртуальная карточка "Мои замещения".
 */
export declare class CustomTabPanelButtonUIExtension extends TabPanelUIExtension {
    initialize(context: ITabPanelUIExtensionContext): void;
}
