import Moment from 'moment';
import React from 'react';
import { CalendarModificationCallback } from 'ui';
import { OcrDateTimeViewModel } from './ocrDateTimeViewModel';
/** Пропсы контрола для ввода выбора даты из календаря. */
interface OcrInputCalendarProps {
    /** Ссылка на родительский компонент. */
    rootRef: () => React.ReactInstance | null;
    /** Признак того, что календарь открыт. */
    isCalendarOpened: boolean;
    /** Модель представления с данными для контрола выбора выбора даты из календаря. */
    viewModel: OcrDateTimeViewModel;
    /** Функция обратного вызова  для изменения даты. */
    calendarModification?: CalendarModificationCallback | null;
    /** Обработчик события нажатия на кнопку календаря. */
    onCalendarButtonClick?: VoidFunction;
    /** Обработчик события выбора даты в календаре. */
    onDaySelect?: (date: Moment.Moment | null, formattedDate: string | null) => void;
}
/** Контрол для ввода выбора даты из календаря. */
export declare const OcrInputCalendar: React.FC<OcrInputCalendarProps>;
export {};
