declare type RetryOptions = {
    /**
     * Максимальное количество попыток повторения операции.
     * @default 4
     */
    retries?: number;
    /**
     * Используемый экспоненциальный коэффициент.
     * @default 3
     */
    factor?: number;
    /**
     * Параметр, который позволяет использовать retry при наличии подключения к сети.
     * По-умолчанию, при обнаружении активного сетевого подключения, retry прекращает попытки повторного
     * вызова переданной в него функции.
     */
    online?: boolean;
    /**
     * Количество миллисекунд до начала первой повторной попытки.
     * @default 1000
     */
    minTimeout?: number;
    /**
     * Максимальное количество миллисекунд, по истечению которого, будет выполнен повторный запрос.
     * Берется минимальное значение между вычисленным timeout и maxTimeout,
     * то есть, если вычисленный timeout больше maxTimeout, то будет взят maxTimeout,
     * если меньше, то timeout.
     * @default Infinity
     */
    maxTimeout?: number;
    /**
     * AbortController - представляет объект контроллера,
     * который позволяет при необходимости обрывать один и более DOM запросов.
     */
    controller?: AbortController;
    /**
     * Выполняется в блоке catch, используется для обработки ошибки сети.
     * По умолчанию используется showLoadingNetworkError в качестве дефолтного обработчика ошибки, связанной с проблемой сети.
     * @param err - ошибка при выполнении полезной нагрузки;
     * @param networkListener - служит для прослушивания сети с переодичностью в одну секунду, если сеть появилась, то ресолвится;
     * @param controller - представляет объект контроллера, который позволяет при необходимости обрывать один и более DOM запросов;
     */
    errorHandler?: (err: Error, networkListener: (signal?: AbortSignal) => Promise<void>, controller?: AbortController) => Promise<void>;
    /**
     * Данная функция позволяет проанализировать содержимое переданного в нее результата и в зависимости
     * от этого инициировать новую retry-попытку.
     * @param result - результат выполнения функции, обернутой в retry;
     */
    resultDataCatcher?: (result: unknown) => void;
};
/**
 * Абстракция для повтора неудачных операций.
 * При дефолтных параметрах: retries = 4; factor = 3; minTimeout = 1000; maxTimeout = Infinity;
 * 1) timeout =  Math.round(Math.max(minTimeout, 1) * factor ^ retrie);
 * 2) timeout = Math.min(timeout, maxTimeout);
 * retrie - номер попытки повторного запроса;
 * factor - коэфициент, который возводится в степень retrie;
 * Получаем timeouts = [1000, 3000, 9000, 27000];
 * @param func - полезная нагрузка;
 * @param options - параметры, при помощи которых, определяются условия повтора операций;
 */
export declare function retry<T>(func: () => Promise<T>, options?: RetryOptions): Promise<T>;
export {};
