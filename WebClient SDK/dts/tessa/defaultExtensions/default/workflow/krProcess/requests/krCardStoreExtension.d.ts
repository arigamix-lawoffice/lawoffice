import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
/**
 * Расширение на сохранение карточки, содержащей маршрут.
 */
export declare class KrCardStoreExtension extends CardStoreExtension {
    beforeRequest(context: ICardStoreExtensionContext): void;
    private static visitSections;
    private static getParentColumnSec;
    private static swapLayers;
}
