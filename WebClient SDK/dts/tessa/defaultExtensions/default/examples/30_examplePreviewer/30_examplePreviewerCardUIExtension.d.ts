import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Позволяет создавать кастомный превьюер и использовать его для определенного
 * типа данных в выбранной карточке.
 *
 * Результат работы расширения:
 * Для тестовой карточки "Автомобиль" создает кастомный превьюер и отображает его
 * для типа данных с расширением ".txt".
 */
export declare class ExamplePreviewerCardUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static setExamplePreviewerFactory;
}
