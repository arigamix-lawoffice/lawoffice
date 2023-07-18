import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * При клике на контрол кнопки показываем кастомный диалог.
 *
 * Результат работы расширения:
 * При клике на кнопку "Показать диалог" показываем кастомный диалог,
 * содержащий текстовое поле, а также кнопки "ОК" и "Закрыть". При нажатии на кнопку "OK"
 * в консоли отображается содержимое текстового поля.
 */
export declare class ShowCustomDialogUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static showCustomDialog;
}
