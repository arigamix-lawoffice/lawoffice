import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import './25_styles.css';
/**
 * Изменяем стили контролов таблиц для определенного типа карточек
 * посредством CSS-классов и свойства style.
 *
 * Результат работы расширения:
 * Пример данного расширения изменяет стили контрола "Список акций" посредством добавления
 * СSS-классов для тестовой карточки "Автомобиль".
 */
export declare class GridBasicStylingExtension extends CardUIExtension {
    private _disposer?;
    initialized(context: ICardUIExtensionContext): void;
    finalized(_context: ICardUIExtensionContext): void;
}
