import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение для UI карточки с настройками лицензии.
 */
export declare class LicenseUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private getForeground;
    private setControlForeground;
    private addEmployeesFromRoles;
    private addEmployeesFromRolesForSection;
    private uniqueWithGetter;
    private addUsersFromRole;
    private tryAskRolesFromUser;
    private checkResponseAndSetID;
    private checkResponse;
}
