import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Расширение на обновление виртуальной карточки состояния документа со стороны клиента.
 */
export declare class KrDocStateUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    reopening(context: ICardUIExtensionContext): void;
}
