import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * При клике на контрол кнопки показываем диалог с выбранной формой.
 *
 * Результат работы расширения:
 * При клике на кнопку "Показать диалог" показываем диалоговое окно "Создать несколько карточек".
 */
export declare class ShowFormDialogUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static showFormDialog;
}
