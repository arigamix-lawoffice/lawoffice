import Moment from 'moment';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { computed, observable, runInAction } from 'mobx';
import { DatePickerFormatter } from 'ui/datePicker/datePickerFormatter';
import { DateTimeTypeFormat } from 'ui/datePicker/dateTimeTypeFormat';
import { ICardModel } from 'tessa/ui/cards';
import { LocalizationManager } from 'tessa/localization';
import { tryGetFromSettings } from 'tessa/ui/uiHelper';

/** Модель представления с данными для контрола выбора выбора даты из календаря. */
export class OcrDateTimeViewModel {
  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrDateTimeViewModel}.
   * @param {CardTypeEntryControl} control
   * Объект, описывающий расположение и свойства элемента
   * управления для привязки к полям строковой секции карточки.
   * @param {ICardModel} _model Модель карточки, доступная в UI.
   */
  constructor(control: CardTypeEntryControl, _model: ICardModel) {
    const settings = control.controlSettings;
    this.minDate = tryGetFromSettings<Moment.Moment | null>(settings, 'MinDate', null);
    this.maxDate = tryGetFromSettings<Moment.Moment | null>(settings, 'MaxDate', null);
    this.beginDate = tryGetFromSettings<Moment.Moment | null>(settings, 'BeginDate', null);
    this.highlightBeginDate = tryGetFromSettings<boolean>(settings, 'HighlightBeginDate', false);
    this.dateNullable = tryGetFromSettings<boolean>(settings, 'DateNullable', false);
    this.ignoreTimezone = tryGetFromSettings<boolean>(settings, 'IgnoreTimezone', false);
    const dateEnabled = tryGetFromSettings<boolean>(settings, 'DateFormat', false);
    const timeEnabled = tryGetFromSettings<boolean>(settings, 'TimeEnabled', false);
    this.dateTimeFormat =
      DateTimeTypeFormat[tryGetFromSettings<string>(settings, 'DateTimeFormat')] ??
      ((dateEnabled && timeEnabled) || (!dateEnabled && !timeEnabled)
        ? DateTimeTypeFormat.DateTime
        : dateEnabled
        ? DateTimeTypeFormat.Date
        : DateTimeTypeFormat.Time);
    this._dateTimeFormatter = new DatePickerFormatter({ dateTimeFormat: this.dateTimeFormat });
  }

  //#endregion

  //#region properties

  /** Устанавливает минимальную границу даты, дни ДО указанного значения в свойстве будут недоступны. */
  public readonly minDate: Moment.Moment | null;

  /** Устанавливает максимальную границу даты, дни ПОСЛЕ указанного значения в свойстве будут недоступны. */
  public readonly maxDate: Moment.Moment | null;

  /** Дата, на которой открывается календарь. */
  public readonly beginDate: Moment.Moment | null;

  /** Добавляет подсветку для дня в календаре, указанного в {@link beginDate}. */
  public readonly highlightBeginDate: boolean;

  /** Признак, показывающий, что дата может быть не указана. */
  public readonly dateNullable: boolean;

  /** Признак того, что часовой пояс сотрудника игнорируется и дата всегда указывается как UTC. */
  public readonly ignoreTimezone: boolean;

  /** Настройки, определяющие формат отображаемой даты в контроле. */
  public readonly dateTimeFormat: DateTimeTypeFormat;

  /** Инструмент для преобразования даты и времени к необходимому формату. */
  private readonly _dateTimeFormatter: DatePickerFormatter;

  /** Текущие дата/время, хранящиеся в системе, или `null`, если дата/время не выбраны. */
  @observable
  public storageDate: string | null;

  /** Текущие дата/время, отображаемые элементом управления, или `null`, если дата/время не выбраны. */
  @computed
  public get selectedDate(): Moment.Moment | null {
    let value = this.storageDate;

    if (!value) {
      return null;
    }

    // Date плохо работает с датой < 1970 года
    value = !this.isDateFormat ? value.replace(/^1753/, '1970') : value;

    const format = ['YYYY-MM-DDTHH:mm:ssZ', Moment.ISO_8601];
    let date = Moment.utc(value, format, true);
    if (!date || !date.isValid()) {
      return null;
    }

    date =
      this.dateTimeFormat === DateTimeTypeFormat.Date || this.ignoreTimezone
        ? date.local(true) // чтобы utc значение считалось локальным значением
        : date.local();

    return date.locale(LocalizationManager.instance.currentLocaleCode);
  }
  public set selectedDate(value: Moment.Moment | null) {
    if (!value) {
      runInAction(() => (this.storageDate = null));
      return;
    }

    if (this.maxDate && value.isAfter(this.maxDate)) {
      runInAction(() => (this.storageDate = null));
      return;
    }

    if (this.minDate && value.isBefore(this.minDate)) {
      runInAction(() => (this.storageDate = null));
      return;
    }

    // Если дата не нужна, то нам нужно поставить дату равной 1753-01-01.
    // Поставить год меньше 1970 мы не можем, поэтому делаем это поэтапно:
    // 1) сначала ставим месяц и день,
    // 2) а после форматирования - меняем год руками
    value = !this.isDateFormat ? value.month(0).date(2) : value;

    let formatDate =
      this.dateTimeFormat === DateTimeTypeFormat.Date || this.ignoreTimezone
        ? value.utc(true).format() // чтобы локальное значение считалось utc значением
        : value.utc().format();

    // меняем год руками
    formatDate = !this.isDateFormat ? formatDate.replace(/^\d\d\d\d/, '1753') : formatDate;

    runInAction(() => (this.storageDate = formatDate));
  }

  /**
   * Текущие дата/время, отображаемые элементом управления представленные
   * в виде строкового значения, или `null`, если дата/время не выбраны.
   */
  @computed
  public get selectedDayString(): string | null {
    return this.selectedDate?.format() ?? null;
  }

  /**
   * Текущие дата/время, отображаемые элементом управления представленные
   * в виде отформатированного строкового значения, или `null`, если дата/время не выбраны.
   */
  @computed
  public get selectedDayFormatted(): string | null {
    return this._dateTimeFormatter.getDateFormat(this.selectedDate) ?? null;
  }

  /** Признак того, что формат контрола поддерживает отображение даты. */
  @computed
  public get isDateFormat(): boolean {
    return (
      this.dateTimeFormat === DateTimeTypeFormat.Date ||
      this.dateTimeFormat === DateTimeTypeFormat.DateTime
    );
  }

  //#endregion
}
