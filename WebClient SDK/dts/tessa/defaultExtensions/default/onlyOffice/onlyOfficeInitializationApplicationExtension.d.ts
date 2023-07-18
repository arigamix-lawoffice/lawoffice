import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
/**
 * Представляет собой расширение, которое инициализирует работу с OnlyOffice.
 */
export declare class OnlyOfficeInitializationApplicationExtension extends ApplicationExtension {
    initialize(): void;
    afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void>;
    /**
     * Получает необходимые данные из карточки настроек OnlyOffice или null, если OnlyOffice не настроен.
     */
    private static getSettings;
}
