import * as React from 'react';
import Moment from 'moment';
import { DatePickerDayData, DatePickerMonthData } from './datePickerTypes';
declare class DatePickerMonth extends React.Component<IDatePickerMonthProps> {
    isWeekInMonth(startOfWeek: Moment.Moment): boolean;
    render(): JSX.Element;
}
export interface IDatePickerMonthProps {
    date: Moment.Moment;
    month: DatePickerMonthData;
    isHighlightBeginDate?: boolean;
    selectedDate?: Moment.Moment | null;
    beginDate?: Moment.Moment | null;
    maxDate?: Moment.Moment | null;
    minDate?: Moment.Moment | null;
    onDaySelect: (value: Moment.Moment) => void;
    modifiedDays: DatePickerDayData[];
}
export default DatePickerMonth;
