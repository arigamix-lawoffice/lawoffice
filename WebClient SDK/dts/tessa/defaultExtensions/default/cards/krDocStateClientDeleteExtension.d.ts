import { CardDeleteExtension, CardDeleteExtensionContext } from 'tessa/cards/extensions';
/**
 * Расширение на удаление виртуальной карточки состояния документа со стороны клиента.
 */
export declare class KrDocStateClientDeleteExtension extends CardDeleteExtension {
    shouldExecute(context: CardDeleteExtensionContext): boolean;
    beforeRequest(context: CardDeleteExtensionContext): void;
}
