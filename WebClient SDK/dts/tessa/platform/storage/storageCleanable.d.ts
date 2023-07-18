/**
 * Поддерживает очистку хранилища от избыточных данных.
 */
export interface IStorageCleanable {
    /**
     * Выполняет очистку хранилища от избыточных данных.
     */
    clean(): any;
}
/**
 * Функция проверяет, что объект поддерживает интерфейс {@link IStorageCleanable}.
 * @param obj Объект, который требуется проверить.
 * @returns `true`, если объект поддерживает интерфейс; `false`, если интерфейс не поддерживается.
 */
export declare function isIStorageCleanable(obj: object): obj is IStorageCleanable;
