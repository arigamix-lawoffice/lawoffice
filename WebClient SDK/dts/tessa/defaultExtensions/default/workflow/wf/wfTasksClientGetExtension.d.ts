import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
/**
 * Загрузка карточки с управлением секциями заданий на клиенте.
 */
export declare class WfTasksClientGetExtension extends CardGetExtension {
    afterRequest(context: ICardGetExtensionContext): void;
    private static setResolutionFieldChanged;
}
