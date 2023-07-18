import { IControlViewModel } from 'tessa/ui/cards';
import { localize } from 'tessa/localization';
import { OcrNumberValidator } from './ocrNumberValidator';
import { OcrPatternTypes } from '../misc/ocrTypes';
import { OcrValidator } from './ocrValidator';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Валидатор целочисленного значения. */
export class OcrIntegerValidator extends OcrNumberValidator {
  //#region fields

  /** Признак, что число является без знаковым. */
  private readonly _unsigned: boolean;

  //#endregion

  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrIntegerValidator}.
   * @param {IControlViewModel} control Модель представления проверяемого контрола.
   * @param {boolean} unsigned Признак, что число является без знаковым.
   */
  constructor(control: IControlViewModel, unsigned: boolean) {
    super(control);
    this._unsigned = unsigned;
  }

  //#endregion

  //#region private methods

  /**
   * Выполняет проверку, что входное значение является положительным.
   * @param value Проверяемое значение.
   * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
   */
  private validateUnsigned(value: number): ValidationResult | null {
    const message = localize('$UI_Cards_TypesEditor_ValueCantBeLowerThanZero');
    return value < 0 ? ValidationResult.fromText(message, ValidationResultType.Error) : null;
  }

  //#endregion

  //#region base overrides

  protected get patterns(): ReadonlyArray<RegExp> {
    return OcrValidator.getPatterns(OcrPatternTypes.Integer);
  }

  protected get error(): string {
    return '$UI_Controls_Integer_InvalidValue';
  }

  protected compileValue(match: RegExpExecArray): string {
    if (match.groups) {
      const sign = match.groups['sign'] ?? '';
      const integer = match.groups['integer'] ?? '';
      return `${sign}${integer}`;
    } else {
      return match[0];
    }
  }

  protected validateNumber(value: number): ValidationResult | null {
    const validationResult = super.validateNumber(value);
    return !validationResult && this._unsigned ? this.validateUnsigned(value) : validationResult;
  }

  protected formatNumber(value: number): string {
    return value.toString();
  }

  //#endregion
}
