import { IStorage, MapStorage, StorageObject } from 'tessa/platform/storage';
import { KrPermissionsFileExtensionSettings } from './krPermissionsFileExtensionSettings';
/**
 * Расширенные настройки доступа файлов.
 */
export declare class KrPermissionsFilesSettings extends StorageObject {
    static readonly globalSettingsKey = "GlobalSettings";
    static readonly extensionSettingsKey = "ExtensionSettings";
    private static readonly _extensionSettingsFactory;
    constructor(storage?: IStorage);
    /**
     * Настройки доступа файлов для всех расширений или <c>null</c>, если нет настроек для всех расширений.
     */
    get globalSettings(): KrPermissionsFileExtensionSettings | null | undefined;
    set globalSettings(value: KrPermissionsFileExtensionSettings | null | undefined);
    /**
     * Настройки доступа файлов для конкретных расширений.
     */
    get extensionSettings(): MapStorage<KrPermissionsFileExtensionSettings>;
    set extensionSettings(value: MapStorage<KrPermissionsFileExtensionSettings>);
    tryGetExtensionSettings(): MapStorage<KrPermissionsFileExtensionSettings> | null | undefined;
}
