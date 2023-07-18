/**
 * Объект, получающий уведомления об изменениях в его хранилище.
 */
export interface IStorageNotificationReciever {
    /**
     * Метод, уведомляющий объект о наличии изменений в его хранилище.
     */
    notifyStorageUpdated(): any;
}
/**
 * Функция проверяет, что объект поддерживает интерфейс {@link IStorageNotificationReciever}.
 * @param obj Объект, который требуется проверить.
 * @returns `true`, если объект поддерживает интерфейс; `false`, если интерфейс не поддерживается.
 */
export declare function isIStorageNotificationReciever(obj: object): obj is IStorageNotificationReciever;
