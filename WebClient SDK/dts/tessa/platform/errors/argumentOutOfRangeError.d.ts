/**
 * Исключение, которое выдается, если значение аргумента не соответствует допустимому диапазону значений, установленному вызванным методом.
 */
export declare class ArgumentOutOfRangeError extends RangeError {
    /**
     * Инициализирует новый экземпляр {@link ArgumentOutOfRangeError}.
     * @param {string} paramName Имя параметра, вызвавшего данное исключение.
     * @param {any} actualValue Значение параметра, вызвавшего данное исключение.
     */
    constructor(paramName: string, actualValue: unknown);
}
