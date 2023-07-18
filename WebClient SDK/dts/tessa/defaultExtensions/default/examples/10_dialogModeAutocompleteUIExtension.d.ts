import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Показываем выбранный автокомплит всегда в диалоговом окне для определенного типа карточки.
 *
 * Результат работы расширения:
 * Проверяем, что значения типа карточки и алиаса контрола те, что нам нужны и если так,
 * то добавляем флаг, который указывает автокомплиту работать в режиме диалога.
 */
export declare class DialogModeAutocompleteUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
}
