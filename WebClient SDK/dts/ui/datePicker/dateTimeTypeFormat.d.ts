export declare class DateTimeTypeFormatWrapper {
    static getEnum(value: string): DateTimeTypeFormat | null;
}
/**
 * Настройки, определяющие формат отображаемой даты в контроле {@link DateTimeControl}.
 */
export declare enum DateTimeTypeFormat {
    /**
     * Дата и время. Значение по умолчанию.
     */
    DateTime = 0,
    /**
     * Только дата.
     */
    Date = 1,
    /**
     * Только время.
     */
    Time = 2,
    /**
     * Временной интервал.
     */
    Interval = 3
}
