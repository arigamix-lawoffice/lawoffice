import Moment from 'moment';
import { DatePickerFormatter } from 'ui/datePicker/datePickerFormatter';
import { DateTimeTypeFormat } from 'ui/datePicker/dateTimeTypeFormat';
import { IControlViewModel } from 'tessa/ui/cards';
import { localize } from 'tessa/localization';
import { OcrPatternTypes } from '../misc/ocrTypes';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Валидатор значения даты и времени. */
export class OcrDateTimeValidator extends OcrValidator {
  //#region fields

  /** Признак того, что часовой пояс сотрудника игнорируется и дата всегда указывается как UTC. */
  private readonly _ignoreTimezone: boolean;

  /** Настройки, определяющие формат отображаемой даты в контроле. */
  private readonly _dateTimeFormat: DateTimeTypeFormat;

  /** Инструмент для преобразования даты и времени к необходимому формату. */
  private readonly _dateTimeFormatter: DatePickerFormatter;

  //#endregion

  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrDateTimeValidator}.
   * @param {IControlViewModel} _control Модель представления проверяемого контрола.
   * @param {DateTimeTypeFormat} dateTimeFormat Настройки, определяющие формат отображаемой даты в контроле.
   * @param {IControlViewModel} ignoreTimezone Признак того, что часовой пояс сотрудника игнорируется и дата всегда указывается как UTC.
   */
  constructor(
    _control: IControlViewModel,
    dateTimeFormat: DateTimeTypeFormat,
    ignoreTimezone: boolean
  ) {
    super();
    this._dateTimeFormat = dateTimeFormat;
    this._ignoreTimezone = ignoreTimezone;
    this._dateTimeFormatter = new DatePickerFormatter({ dateTimeFormat: dateTimeFormat });
  }

  //#endregion

  //#region base overrides

  protected get patterns(): ReadonlyArray<RegExp> {
    switch (this._dateTimeFormat) {
      case DateTimeTypeFormat.DateTime:
        return OcrValidator.getPatterns(OcrPatternTypes.DateTime);
      case DateTimeTypeFormat.Date:
        return OcrValidator.getPatterns(OcrPatternTypes.Date);
      case DateTimeTypeFormat.Time:
        return OcrValidator.getPatterns(OcrPatternTypes.Time);
      case DateTimeTypeFormat.Interval:
        return OcrValidator.getPatterns(OcrPatternTypes.Interval);
    }
  }

  protected get error(): string {
    switch (this._dateTimeFormat) {
      case DateTimeTypeFormat.DateTime:
        return '$UI_Controls_DateTime_InvalidValue';
      case DateTimeTypeFormat.Date:
        return '$UI_Controls_Date_InvalidValue';
      case DateTimeTypeFormat.Time:
        return '$UI_Controls_Time_InvalidValue';
      case DateTimeTypeFormat.Interval:
        return '$UI_Controls_Interval_InvalidValue';
    }
  }

  protected compileValue(match: RegExpExecArray): string {
    if (match.groups) {
      return Moment({
        year: +(this.removeSpaces(match.groups['year'] ?? '') || '1970'),
        month: +(this.removeSpaces(match.groups['month'] ?? '') || '01') - 1,
        day: +(this.removeSpaces(match.groups['day'] ?? '') || '01'),
        hour: +(this.removeSpaces(match.groups['hours'] ?? '') || '00'),
        minute: +(this.removeSpaces(match.groups['minutes'] ?? '') || '00'),
        second: +(this.removeSpaces(match.groups['seconds'] ?? '') || '00')
      }).format();
    } else {
      return match[0];
    }
  }

  //#endregion

  //#region private methods

  private formatDate(value: Moment.Moment): string {
    value =
      this._dateTimeFormat === DateTimeTypeFormat.Date || this._ignoreTimezone
        ? value.local(true) // чтобы utc значение считалось локальным значением
        : value.local();

    return this._dateTimeFormatter.getDateFormat(value) ?? value.format();
  }

  //#endregion

  //#region public methods

  public validate(value: string): OcrValidationResult {
    for (const pattern of this.patterns) {
      const match = pattern.exec(value);
      if (match && match.length > 0) {
        let compiledValue = this.compileValue(match);
        const format = ['YYYY-MM-DDTHH:mm:ssZ', Moment.ISO_8601];
        const parsedValue = Moment(compiledValue, format, true);
        if (parsedValue.isValid()) {
          const formattedValue = this.formatDate(parsedValue);
          let validationResult: ValidationResult | null = null;
          if (formattedValue != value) {
            validationResult = ValidationResult.fromText(formattedValue);
          }
          compiledValue =
            this._dateTimeFormat === DateTimeTypeFormat.Date || this._ignoreTimezone
              ? parsedValue.utc(true).format() // чтобы локальное значение считалось utc значением
              : parsedValue.utc().format();
          compiledValue =
            this._dateTimeFormat === DateTimeTypeFormat.Time ||
            this._dateTimeFormat === DateTimeTypeFormat.Interval
              ? compiledValue.replace(/^\d\d\d\d/, '1753')
              : compiledValue;

          return new OcrValidationResult(validationResult, compiledValue, formattedValue);
        }
      }
    }

    const result = ValidationResult.fromText(localize(this.error), ValidationResultType.Error);
    return new OcrValidationResult(result);
  }

  //#endregion
}
