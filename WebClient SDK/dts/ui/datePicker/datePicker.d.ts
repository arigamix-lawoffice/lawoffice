import * as React from 'react';
import Moment from 'moment';
import { MediaStyle } from '../mediaStyle';
import { DatePickerFormatter } from './datePickerFormatter';
import { DateTimeTypeFormat } from './dateTimeTypeFormat';
import { CalendarModificationCallback } from './datePickerTypes';
import { UIButton } from 'tessa/ui/uiButton';
export interface DatePickerProps {
    date: string | null;
    buttons?: UIButton[];
    onDateChange: (date: string | null, fromPopover?: boolean) => void;
    isHighlightBeginDate?: boolean;
    beginDate?: Moment.Moment | null;
    minDate?: Moment.Moment | null;
    maxDate?: Moment.Moment | null;
    disabled?: boolean;
    className?: string;
    dateTimeFormat: DateTimeTypeFormat;
    hideClearButton?: boolean;
    mediaStyle?: MediaStyle | null;
    style?: React.CSSProperties;
    title?: string;
    isInvalid?: boolean;
    onChange?: (event: React.SyntheticEvent) => void;
    onFocus?: (event: React.SyntheticEvent) => void;
    onBlur?: (event: React.SyntheticEvent) => void;
    onKeyDown?: (event: React.SyntheticEvent) => void;
    bindReactComponentRef?: (datePickerRef: React.RefObject<DatePicker>) => void;
    unbindReactComponentRef?: () => void;
    calendarModification?: CalendarModificationCallback | null;
}
export interface DatePickerState {
    isOpened: boolean;
}
declare class DatePicker extends React.Component<DatePickerProps, DatePickerState> {
    constructor(props: DatePickerProps);
    formatter: DatePickerFormatter;
    private _inputRef;
    private _datePickerRef;
    static tryGetDate(date?: string | null): Moment.Moment | null;
    static validateDate(date?: Moment.Moment | null): Moment.Moment | null;
    getDateFromText(value?: string): Moment.Moment | null;
    updateDate(date: string | null, fromPopover?: boolean): void;
    focus(opt?: FocusOptions): void;
    blur(): void;
    handleCalendarButtonClick: () => void;
    handleClearButtonClick: () => void;
    handleCalendarDateSelect: (value: Moment.Moment) => void;
    handleInputSelect: (event: React.FocusEvent<HTMLInputElement>) => void;
    handleInputFocus: (event: React.FocusEvent<HTMLInputElement>) => void;
    handleInputBlur: (event: React.FocusEvent<HTMLInputElement>) => void;
    handleInputValidate: (value: string) => string;
    handleOnInput: (event: React.FormEvent<HTMLInputElement>) => void;
    handleKeyDown: (event: React.KeyboardEvent) => void;
    componentDidMount(): void;
    componentWillUnmount(): void;
    renderCalendar(): JSX.Element | null;
    render(): JSX.Element;
}
export default DatePicker;
