import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение UI для карточки "Контекстная роль".
 * Скрывает флаг "Отключить замещения" в ситуации, если используется новая система замещения.
 */
export declare class ContextRoleUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
}
