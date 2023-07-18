import { formatDecimalFixed } from 'tessa/platform/formatting';
import { IControlViewModel } from 'tessa/ui/cards';
import { localize } from 'tessa/localization';
import { OcrDoubleValidator } from './ocrDoubleValidator';
import { tryGetFromSettings } from 'tessa/ui/uiHelper';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Валидатор значения вещественного числа с фиксированной запятой. */
export class OcrDecimalValidator extends OcrDoubleValidator {
  //#region fields

  /** Точность числа (кол-во цифр после точки). */
  private readonly _digitsAfterSeparator: number;

  //#endregion

  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrDecimalValidator}.
   * @param {IControlViewModel} control Модель представления проверяемого контрола.
   */
  constructor(control: IControlViewModel) {
    super(control);

    const settings = control.cardTypeControl.controlSettings;
    this._digitsAfterSeparator = tryGetFromSettings(settings, 'DigitsAfterSeparator', 2);
    if (this._digitsAfterSeparator < 0) {
      throw new Error(localize('$UI_Controls_InvalidDecimalSeparator'));
    }
  }

  //#endregion

  //#region methods

  /**
   * Выполняет проверку, что входное значение имеет заданную точность.
   * @param value Проверяемое значение.
   * @param precision Точность - кол-во цифр после точки.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  private validatePrecision(value: string, precision: number): ValidationResult | null {
    const separatorIndex = value.indexOf('.');
    const valuePrecision = separatorIndex >= 0 ? value.length - separatorIndex - 1 : 0;
    return valuePrecision > precision
      ? ValidationResult.fromText(
          localize('$UI_Controls_DecimalSeparatorViolation', precision),
          ValidationResultType.Error
        )
      : null;
  }

  //#endregion

  //#region base overrides

  protected get error(): string {
    return '$UI_Controls_Decimal_InvalidValue';
  }

  protected validateString(value: string): ValidationResult | null {
    return this.validatePrecision(value, this._digitsAfterSeparator);
  }

  protected formatNumber(value: number): string {
    return formatDecimalFixed(value, this._digitsAfterSeparator, true);
  }

  //#endregion
}
