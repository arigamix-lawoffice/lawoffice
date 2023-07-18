import Moment from 'moment';
import Platform from 'common/platform';
import React from 'react';
import { CalendarModificationCallback, DatePicker, DatePickerCalendar, Dialog, Popover } from 'ui';
import { DatePickerFormatter } from 'ui/datePicker/datePickerFormatter';
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
export const OcrInputCalendar: React.FC<OcrInputCalendarProps> = ({
  rootRef,
  isCalendarOpened,
  viewModel,
  calendarModification,
  onCalendarButtonClick,
  onDaySelect
}) => {
  const [isOpened, setIsOpened] = React.useState(isCalendarOpened);
  React.useEffect(() => setIsOpened(isCalendarOpened), [isCalendarOpened]);

  const { dateTimeFormat, selectedDayString } = viewModel;

  const dateTimeFormatter = React.useMemo<DatePickerFormatter>(() => {
    return new DatePickerFormatter({ dateTimeFormat });
  }, [dateTimeFormat]);

  const handleCloseRequest = React.useCallback(() => {
    onCalendarButtonClick?.();
    setIsOpened(false);
  }, [onCalendarButtonClick]);

  const handleDaySelect = React.useCallback(
    (value: Moment.Moment): void => {
      let dateIn: Moment.Moment | null = null;

      const dateOut = DatePicker.validateDate(value);
      if (dateOut && (dateIn = DatePicker.tryGetDate(selectedDayString)) != null) {
        dateOut.second(dateIn.second());
        dateOut.minute(dateIn.minute());
        dateOut.hour(dateIn.hour());
      }

      onDaySelect?.(dateOut, dateTimeFormatter.getDateFormat(dateOut) ?? null);
      onCalendarButtonClick?.();
      setIsOpened(false);
    },
    [dateTimeFormatter, selectedDayString, onDaySelect, onCalendarButtonClick]
  );

  const calendar = React.useMemo(
    () => (
      <DatePickerCalendar
        minDate={viewModel.minDate}
        maxDate={viewModel.maxDate}
        beginDate={viewModel.beginDate}
        selectedDate={DatePicker.tryGetDate(selectedDayString)}
        isHighlightBeginDate={viewModel.highlightBeginDate}
        onDaySelect={handleDaySelect}
        calendarModification={calendarModification}
      />
    ),
    [
      viewModel.minDate,
      viewModel.maxDate,
      viewModel.beginDate,
      viewModel.highlightBeginDate,
      selectedDayString,
      handleDaySelect,
      calendarModification
    ]
  );

  return Platform.isMobile() ? (
    <Dialog isAutoSize={false} isOpened={isOpened} onCloseRequest={handleCloseRequest}>
      {calendar}
    </Dialog>
  ) : (
    <Popover rootElement={rootRef()} isOpened={isOpened} onOutsideClick={handleCloseRequest}>
      <div className="date-picker-popover">{calendar}</div>
    </Popover>
  );
};
