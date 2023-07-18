import * as React from 'react';
import Moment from 'moment';
import { DatePickerDayData } from './datePickerTypes';
declare class DatePickerDay extends React.Component<IDatePickerDayProps> {
    isSameDay(other?: Moment.Moment | null): boolean;
    isOutsideMonth(): boolean;
    render(): JSX.Element;
}
export interface IDatePickerDayProps {
    date: Moment.Moment;
    month: number;
    beginDate?: Moment.Moment | null;
    selectedDate?: Moment.Moment | null;
    maxDate?: Moment.Moment | null;
    minDate?: Moment.Moment | null;
    isHighlightBeginDate?: boolean;
    onDaySelect: () => void;
    modifiedDays: DatePickerDayData[];
}
export default DatePickerDay;
