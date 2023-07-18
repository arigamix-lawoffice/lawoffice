import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение на модификацию UI карточки правила расчёта ACL.
 * Производит инициализацию таблицы с триггерами.
 * Производит обновление UI при установке флага "Использовать умные роли".
 * Производит привязку кнопок "Проверить" и "Проверить все" к действиям.
 * Производит привязку даблклика к представлению "Ошибки".
 * Производит обработку списка "Расширения" для отображение контролов расширений.
 */
export declare class AclGenerationRuleUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    private initializeExtensions;
    private initializeTriggersGrid;
    private initializeSmartRoles;
    private initializeButtons;
    private initializeLicenseWarning;
}
