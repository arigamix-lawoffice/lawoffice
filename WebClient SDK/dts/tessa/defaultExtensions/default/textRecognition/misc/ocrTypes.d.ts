/** Типы шаблонов для проверки значения. */
export declare enum OcrPatternTypes {
    /** Булево значение. */
    Boolean = 0,
    /** Целое число. */
    Integer = 1,
    /** Вещественное число. */
    Double = 2,
    /** Дата и время. */
    DateTime = 3,
    /** Только дата. */
    Date = 4,
    /** Только время. */
    Time = 5,
    /** Временной интервал. */
    Interval = 6
}
/** Состояния запросов на распознавание текста в файле. */
export declare enum OcrRequestStates {
    /** Создан. */
    Created = 0,
    /** Активен. */
    Active = 1,
    /** Выполнен. */
    Completed = 2,
    /** Прерван. */
    Interrupted = 3
}
/** Контракт, предоставляющий метод для создания экземпляра заданного типа. */
export interface ILazyInitializer<T> {
    (): T;
}
/** Обеспечивает поддержку отложенной инициализации. */
export declare class Lazy<T> {
    private instance;
    private readonly initializer;
    /**
     * Создает экземпляр класса {@link Lazy<T>}.
     * @param {ILazyInitializer<T>} initializer
     * Функция для создания значения с отложенной инициализацией при необходимости.
     */
    constructor(initializer: ILazyInitializer<T>);
    /** Получает значение, которое показывает, создано ли значение для этого экземпляра {@link Lazy<T>}. */
    get isValueCreated(): boolean;
    /** Получает значение с отложенной инициализацией текущего экземпляра {@link Lazy<T>}. */
    get value(): T;
}
