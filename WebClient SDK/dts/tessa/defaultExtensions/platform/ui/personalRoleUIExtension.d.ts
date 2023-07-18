import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Прячем поле "Аккаунт" и/или "Пароль" в зависимости от типа входа пользователя.
 * При изменении настроек текущего пользователя через карточку сотрудника обновляем UserSettings
 */
export declare class PersonalRoleUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    reopened(context: ICardUIExtensionContext): void;
    private static updateControls;
    private static updateWarningLabel;
}
