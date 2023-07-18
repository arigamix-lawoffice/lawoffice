import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение UI для карточек генераторов умных ролей.
 * Производит UI логику таблицы Условия в триггерах.
 * Производит привязку кнопок "Проверить" и "Проверить все" к действиям.
 * Производит привязку даблклика к представлению "Ошибки".
 */
export declare class SmartRoleGeneratorUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    private initializeTriggersGrid;
    private initializeButtons;
}
