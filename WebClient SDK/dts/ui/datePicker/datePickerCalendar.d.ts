import * as React from 'react';
import Moment from 'moment';
import { GesturesInstance } from 'common/gestures';
import { CalendarModificationCallback } from './datePickerTypes';
declare class DatePickerCalendar extends React.Component<IDatePickerCalendarProps, IDatePickerCalendarState> {
    swipeRightGesture: GesturesInstance;
    swipeLeftGesture: GesturesInstance;
    swipeLeft: any;
    swipeRight: any;
    private _calendarRef;
    static defaultProps: {
        onDaySelect: () => void;
    };
    constructor(props: IDatePickerCalendarProps);
    componentDidMount(): void;
    componentWillUnmount(): void;
    increaseMonth: () => void;
    decreaseMonth: () => void;
    handlePrevButton: (event: React.SyntheticEvent) => void;
    handleNextButton: (event: React.SyntheticEvent) => void;
    handleSwipeRight: () => void;
    handleSwipeLeft: () => void;
    render(): JSX.Element;
}
export interface IDatePickerCalendarProps {
    isHighlightBeginDate?: boolean;
    selectedDate?: Moment.Moment | null;
    beginDate?: Moment.Moment | null;
    minDate?: Moment.Moment | null;
    maxDate?: Moment.Moment | null;
    onDaySelect: (value: Moment.Moment) => void;
    calendarModification?: CalendarModificationCallback | null;
}
export interface IDatePickerCalendarState {
    date: Moment.Moment;
}
export default DatePickerCalendar;
