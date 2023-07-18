/// <reference types="react" />
import Moment from 'moment';
import { CalendarModificationCallback } from './datePickerTypes';
declare const DatePickerModifiableCalendar: (props: IDatePickerModifiableCalendar) => JSX.Element;
interface IDatePickerModifiableCalendar {
    date: Moment.Moment;
    beginDate?: Moment.Moment | null;
    selectedDate?: Moment.Moment | null;
    minDate?: Moment.Moment | null;
    maxDate?: Moment.Moment | null;
    isHighlightBeginDate?: boolean;
    onDaySelect: (value: Moment.Moment) => void;
    calendarModification?: CalendarModificationCallback | null;
}
export default DatePickerModifiableCalendar;
