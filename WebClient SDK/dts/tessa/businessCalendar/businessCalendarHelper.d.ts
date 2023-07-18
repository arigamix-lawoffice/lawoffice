import { CalendarDays } from './calendarDays';
/**
 * Длина кванта времени календаря в минутах.
 */
export declare const QuantLength = 15;
/**
 * Ключ, задающий начало временного интервала.
 */
export declare const DateTimeStartKey: string;
/**
 * Ключ, задающий окончание временного интервала.
 */
export declare const DateTimeEndKey: string;
/**
 * Ключ, задающий начало рабочего времени.
 */
export declare const WorkingTimeStartKey: string;
/**
 * Ключ, задающий окончание рабочего времени.
 */
export declare const WorkingTimeEndKey: string;
/**
 * Ключ, задающий начало обеденного перерыва.
 */
export declare const LunchHourStartKey: string;
/**
 * Ключ, задающий окончание обеденного перерыва.
 */
export declare const LunchHourEndKey: string;
/**
 * Маркер необходимости запуска пересчёта календаря при сохранении карточки.
 */
export declare const RebuildMarkKey = "rebuild_calendar";
/**
 * Ключ для Info в котором передаётся Guid, запущенной операции сохранения.
 * Является маркером для необходимости запуска пересчёта календаря при сохранении карточки.
 */
export declare const RebuildOperationGuidKey: string;
/**
 * Ключ для Info в котором передаётся Guid, запущенной операции сохранения.
 */
export declare const RebuildOperationIDKey = "RebuildOperationID";
export declare const CalendarCalcMethodsSection = "CalendarCalcMethods";
export declare const CalendarTypeWeekDaysSection = "CalendarTypeWeekDays";
export declare const CalendarSettingsSection = "CalendarSettings";
/**
 * Ключ для Info в котором передаётся Guid, карточки календаря, который будет пересчитываться.
 */
export declare const CalendarCardIDKey: string;
export declare const RequestZoneOffsetKey: string;
export declare const RequestDateTimeKey: string;
export declare const RequestRoleIDKey: string;
export declare const ResponseDateTimeKey: string;
export declare const ResponseUtcOffsetKey: string;
export declare const ResponseTimeZoneIDKey: string;
export declare const IsWorkingTimeSignKey: string;
export declare const QuantsKey: string;
export declare const IntervalKey: string;
export declare const DaysOffsetKey: string;
export declare const DateDiffKey: string;
export declare const getDayName: (day: CalendarDays) => string;
