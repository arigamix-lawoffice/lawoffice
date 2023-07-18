import Moment from 'moment';
export declare type DatePickerMonthData = Array<Array<Moment.Moment>>;
export declare type CalendarModificationCallback = (ctx: {
    days: Array<Moment.Moment>;
    abortSignal: AbortSignal;
    modifiedDays: Array<DatePickerDayData>;
}) => Promise<Array<DatePickerDayData>>;
export interface DatePickerDayData {
    day: string;
    className?: string;
    classNameSelected?: string;
    icon?: string;
    disabled?: boolean;
}
