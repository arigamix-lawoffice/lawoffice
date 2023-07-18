import { IControlViewModel } from 'tessa/ui/cards';
import { localize } from 'tessa/localization';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
import { tryGetFromSettings } from 'tessa/ui';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Валидатор числового значения. */
export abstract class OcrNumberValidator extends OcrValidator {
  //#region fields

  /** Минимальное значение числа. */
  private readonly _minValue: number;
  /** Максимальное значение числа. */
  private readonly _maxValue: number;

  //#endregion

  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrNumberValidator}.
   * @param {IControlViewModel} control Модель представления проверяемого контрола.
   */
  protected constructor(control: IControlViewModel) {
    super();
    const settings = control.cardTypeControl.controlSettings;
    this._minValue = tryGetFromSettings(settings, 'MinValue', Number.MIN_SAFE_INTEGER);
    this._maxValue = tryGetFromSettings(settings, 'MaxValue', Number.MAX_SAFE_INTEGER);
  }

  //#endregion

  //#region private methods

  /**
   * Выполняет проверку, что входное значение является числом.
   * @param value Проверяемое значение.
   * @param message Сообщение об ошибке.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  private validateValue(value: number, message: string): ValidationResult | null {
    return isNaN(value)
      ? ValidationResult.fromText(localize(message), ValidationResultType.Error)
      : null;
  }

  /**
   * Выполняет проверку, что входное значения больше либо равно минимальному.
   * @param value Проверяемое значение.
   * @param message Сообщение об ошибке.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  private validateMinValue(value: number, message: string): ValidationResult | null {
    return value < this._minValue
      ? ValidationResult.fromText(localize(message, this._minValue), ValidationResultType.Error)
      : null;
  }

  /**
   * Выполняет проверку, что входное значения меньше либо равно максимальному.
   * @param value Проверяемое значение.
   * @param message Сообщение об ошибке.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  private validateMaxValue(value: number, message: string): ValidationResult | null {
    return value > this._maxValue
      ? ValidationResult.fromText(localize(message, this._maxValue), ValidationResultType.Error)
      : null;
  }

  //#endregion

  //#region protected methods

  /**
   * Выполняет проверку значения, преобразованного в формат, поддерживаемый системой.
   * @param _value Проверяемое значение.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  protected validateString(_value: string): ValidationResult | null {
    return null;
  }

  /**
   * Выполняет проверку входного значения.
   * @param value Проверяемое значение.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  protected validateNumber(value: number): ValidationResult | null {
    let validationResult = this.validateValue(value, this.error);
    if (!validationResult) {
      validationResult = this.validateMinValue(value, '$UI_Controls_MinValueViolation');
      if (!validationResult) {
        validationResult = this.validateMaxValue(value, '$UI_Controls_MaxValueViolation');
      }
    }
    return validationResult;
  }

  /**
   * Выполняет преобразование значения в форматированную строку.
   * @param value Значение для форматирования.
   * @returns Отформатированное значение в виде строки.
   */
  protected abstract formatNumber(value: number): string;

  //#endregion

  //#region public methods

  public validate(value: string): OcrValidationResult {
    for (const pattern of this.patterns) {
      const match = pattern.exec(value);
      if (match && match.length > 0) {
        const compiledValue = this.removeSpaces(this.compileValue(match));
        let validationResult = this.validateString(compiledValue);
        if (!validationResult) {
          const parsedValue = Number(compiledValue);
          validationResult = this.validateNumber(parsedValue);
          if (!validationResult) {
            const formattedValue = this.formatNumber(parsedValue);
            // Выполняем проверку равенства значений с учетом наличия nbsp (неделимого пробела)
            if (formattedValue.replaceAll('\xa0', ' ') !== value.replaceAll('\xa0', ' ')) {
              validationResult = ValidationResult.fromText(formattedValue);
            }
            return new OcrValidationResult(validationResult, compiledValue, formattedValue);
          }
        }
        return new OcrValidationResult(validationResult);
      }
    }

    const result = ValidationResult.fromText(localize(this.error), ValidationResultType.Error);
    return new OcrValidationResult(result);
  }

  //#endregion
}
