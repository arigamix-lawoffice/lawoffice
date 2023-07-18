/**
 * Создает полную копию объекта. Потдерживаеются только @serializable типы!
 * @param source Объект, копию которого нужно создать.
 */
export declare function deepClone<T>(source: T): T;
