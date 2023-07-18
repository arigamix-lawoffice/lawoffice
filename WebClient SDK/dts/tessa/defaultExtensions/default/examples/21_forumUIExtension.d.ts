import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Для выбранной карточки добавляет элементы управления:
 * - В верхней панели текущего обсуждения.
 * - В контекстное меню текущего обсуждения.
 *
 * Результат работы расширения:
 * В верхней панели, а также в контекстном меню текущего обсуждения добавлены
 * соответсвующие тестовые кнопка и пункт контекстного меню для тестовой карточки "Автомобиль".
 */
export declare class ForumUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private tryGetForumControl;
}
