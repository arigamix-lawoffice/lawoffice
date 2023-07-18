import { IDeputiesManagementSettings } from './deputiesManagementSettings';
/**
 * Объект, с помозью которого можно получить настройки замещения <see cref="IDeputiesManagementSettings"/>.
 */
export interface IDeputiesManagementSettingsProvider {
    /**
     * Метод для получения настроек замещения.
     * @returns >Настройки замещения
     */
    getSettings(): IDeputiesManagementSettings;
}
/**
 * Клиентская реализация провайдера настроек замещения.
 */
export declare class DeputiesManagementSettingsProvider implements IDeputiesManagementSettingsProvider {
    private _settings;
    private constructor();
    private static _instance;
    static get instance(): DeputiesManagementSettingsProvider;
    /** @inheritdoc */
    getSettings(): IDeputiesManagementSettings;
    /**
     * Производит инициализацию настроек для провайдера, полученные с сервера.
     * Может быть вызван только один раз.
     * @param settings Настройки замещения
     */
    initializeSettings(settings: IDeputiesManagementSettings): void;
}
