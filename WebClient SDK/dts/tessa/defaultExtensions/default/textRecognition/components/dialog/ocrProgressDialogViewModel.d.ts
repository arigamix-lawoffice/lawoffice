import { ValidationResult } from 'tessa/platform/validation';
/** Модель представления диалога прогресса OCR. */
export declare class OcrProgressDialogViewModel {
    /** Прогресс в процентном соотношении (от 0 до 100). */
    private _progress;
    /** Результат валидации. */
    private _validationResult;
    /** Таймер для отслеживания операции. */
    private _progressTimer;
    /** Идентификатор отслеживаемой операции. */
    private readonly _operationId;
    /** Промежуток времени, через который выполняется запрос состояния операции. */
    private readonly _timeout;
    /** Признак возможности отмены операции. */
    private readonly _canCancel;
    /** Ключ валидации с сообщением, что карточка с заданным идентификатором не найдена в таблице Instances. */
    private static readonly _instanceNotFoundKey;
    /** Прогресс в процентном соотношении (от 0 до 100). */
    get progress(): number;
    private set progress(value);
    /** Результат валидации. */
    get validationResult(): ValidationResult | null;
    /** Признак возможности отмены операции. */
    get canCancel(): boolean;
    /**
     * Создает экземпляр класса {@link OcrProgressDialogViewModel}.
     * @param {guid} operationId Идентификатор отслеживаемой операции.
     * @param {number} timeout Промежуток времени (мс), через который выполняется запрос состояния операции (от 500 мс).
     * @param {number} progress Начальное значение прогресса в процентном соотношении (от 0 до 100).
     * @param {boolean} canCancel Признак возможности отмены операции.
     */
    constructor(operationId: guid, timeout: number, progress?: number, canCancel?: boolean);
    /**
     * Выполняет запуск процесса отслеживания прогресса операции.
     * @returns Функция, выполняющая остановку процесса отслеживания прогресса операции.
     */
    monitorStart(): VoidFunction;
    /** Выполняет остановку процесса отслеживания прогресса операции. */
    monitorExit(): void;
    private isOperationCompleted;
}
