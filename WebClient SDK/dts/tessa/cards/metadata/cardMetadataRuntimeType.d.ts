export declare enum CardMetadataRuntimeType {
    /**
     * Объект неизвестного типа. Используется для определения ссылочного типа в комплексной колонке.
     * @type {Number}
     */
    Object = 0,
    /**
     * Строка
     * @type {Number}
     */
    String = 1,
    /**
     * Логическое значение
     * @type {Number}
     */
    Boolean = 2,
    /**
     * Целое число
     * @type {Number}
     */
    Int32 = 3,
    /**
     * Длинное целое число
     * @type {Number}
     */
    Int64 = 4,
    /**
     * Уникальный идентификатор
     * @type {Number}
     */
    Guid = 5,
    /**
     * Вещественное число
     * @type {Number}
     */
    Double = 6,
    /**
     * Десятичное число
     * @type {Number}
     */
    Decimal = 7,
    /**
     * Дата и/или время
     * @type {Number}
     */
    DateTime = 8,
    /**
     * Дата и время с учётом часового пояса
     * @type {Number}
     */
    DateTimeOffset = 9,
    /**
     * Массив байт
     * @type {Number}
     */
    Binary = 10
}
