import { Duration, Moment } from 'moment';
import { TimeZoneInfo } from 'tessa/businessCalendar/timeZoneInfo';
import { ValidationResult } from 'tessa/platform/validation';
/**
 * Представляет собой сервис для работы с бизнес календарём.
 */
export declare class BusinessCalendarService {
    /**
     * Проверяет, является ли рабочим указанное дата\время в абстрактном времени календаря.
     * @param dateTime Дата и время.
     * @param zoneOffset Смещение временной зоны.
     * @returns True, если указанное дата\время является рабочим; иначе - False.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static isWorkTime(dateTime: Moment, zoneOffset?: Duration): Promise<boolean>;
    /**
     * Рассчитывает рабочее время между двумя указанными датами.
     * @param dateTimeStart Дата начала.
     * @param dateTimeEnd Дата конца (должна быть больше даты начала).
     * @param zoneOffset Смещение временной зоны.
     * @returns Рабочее время в квантах между двумя указанными датами.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static getDateDiff(dateTimeStart: Moment, dateTimeEnd: Moment, zoneOffset?: Duration): Promise<number>;
    /**
     * Добавляет к указанной дате/времени указанное рабочее время в квантах.
     * @param dateTime Дата/время.
     * @param quants Рабочее время в квантах.
     * @param zoneOffset Смещение временной зоны.
     * @returns Новую дату/время.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static addWorkingQuantsToDate(dateTime: Moment, quants: number, zoneOffset?: Duration): Promise<Moment>;
    /**
     * Получает начало первого рабочего кванта рабочего дня, полученного смещением относительно заданной даты.
     * @param dateTime Дата в абстрактном времени календаря.
     * @param daysOffset Смещение в рабочих днях.
     * @param zoneOffset Смещение временной зоны.
     * @returns Дату\время в абстрактном времени календаря начала первого рабочего кванта рабочего дня.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static getFirstQuantStart(dateTime: Moment, daysOffset: number, zoneOffset?: Duration): Promise<Moment>;
    /**
     * Получает конец последнего рабочего кванта рабочего дня, полученного смещением относительно заданной даты.
     * @param dateTime Дата в абстрактном времени календаря.
     * @param daysOffset Смещение в рабочих днях.
     * @param zoneOffset Смещение временной зоны.
     * @returns Дату\время в абстрактном времени календаря конца последнего рабочего кванта рабочего дня.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static getLastQuantEnd(dateTime: Moment, daysOffset: number, zoneOffset?: Duration): Promise<Moment>;
    /**
     * Выполняет смещение от указанной даты/времени на указанное количество расчётных рабочих дней.
     * @param dateTime Дата/время.
     * @param daysOffset Смещение в рабочих днях. Может быть нецелым числом.
     * @param zoneOffset Смещение временной зоны.
     * @returns Новую дату/время.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static addWorkingDaysToDate(dateTime: Moment, daysOffset: number, zoneOffset?: Duration): Promise<Moment>;
    /**
     * Добавляет к указанной дате/времени указанное количество фактических рабочих дней.
     * @param dateTime Дата/время.
     * @param interval Количество рабочих дней.
     * @param zoneOffset Смещение временной зоны.
     * @returns Новую дату/время.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static сalendarAddWorkingDaysToDateExact(dateTime: Moment, interval: number, zoneOffset?: Duration): Promise<Moment>;
    /**
     * Получает информацию временной зоны указанной роли.
     * @param roleId Идентификатор роли.
     * @returns Информацию временной зоны.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static getRoleTimeZoneInfo(roleId: string): Promise<TimeZoneInfo>;
    /**
     * Выполняет перестроение календаря на основании указанных настроек, в т.ч. списка исключений.
     * @param operationGuid Guid операции.
     * @param dateTimeStart Первая дата в абстрактном времени календаря.
     * @param dateTimeEnd Вторая дата в абстрактном времени календаря (должна быть больше первой).
     * @param workTimeStart Начало рабочего дня.
     * @param workTimeEnd Конец рабочего дня.
     * @param lunchTimeStart Начало обеда.
     * @param lunchTimeEnd Конец обеда.
     * @exception ValidationError Ошибка, выбрасываемая в случае, если не удалось получить корректный результат с сервера.
     */
    static rebuildCalendar(operationGuid: string, dateTimeStart: Moment, dateTimeEnd: Moment, workTimeStart: Moment, workTimeEnd: Moment, lunchTimeStart: Moment, lunchTimeEnd: Moment): Promise<void>;
    /**
     * Проверяет календарь на отсутствие пропусков между квантами. Непредвиденные ошибки при выполнении на клиенте возвращаются
     * в объекте ValidationResult, а при выполнении на сервере - выбрасываются в виде исключений.
     * @returns Результат валидации ValidationResult.
     */
    static validateCalendar(calendarCardId: guid): Promise<ValidationResult>;
    private static doRequestWithDateTimeAndOffset;
    private static doRequestWithOffset;
    private static doRequest;
}
