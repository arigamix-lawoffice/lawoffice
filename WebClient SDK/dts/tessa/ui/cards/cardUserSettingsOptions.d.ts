/**
 * Значение по умолчанию для свойства {@link CardUserSettingsOptions#notificationDelayMilliseconds}
 */
export declare const defaultNotificationDelayMilliseconds = 10000;
/**
 * Настройки по отправке изменённых настроек по элементам карточки {@link CardUserSettingsCache}
 * посредством объекта {@link CardUserSettingsOptions}.
 */
export declare class CardUserSettingsOptions {
    private constructor();
    private static _instance;
    static get instance(): CardUserSettingsOptions;
    private _notificationDelayMilliseconds;
    /**
     * Задержка по отправке изменённых настроек в миллисекундах. По умолчанию задержка 10 секунд {@link defaultNotificationDelayMilliseconds}.
     */
    get notificationDelayMilliseconds(): number;
    set notificationDelayMilliseconds(value: number);
}
