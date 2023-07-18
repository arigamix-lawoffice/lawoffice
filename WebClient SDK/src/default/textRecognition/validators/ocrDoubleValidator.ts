import { formatDecimal } from 'tessa/platform/formatting';
import { IControlViewModel } from 'tessa/ui/cards';
import { OcrNumberValidator } from './ocrNumberValidator';
import { OcrPatternTypes } from '../misc/ocrTypes';
import { OcrValidator } from './ocrValidator';

/** Валидатор значения вещественного числа с плавающей запятой. */
export class OcrDoubleValidator extends OcrNumberValidator {
  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrDoubleValidator}.
   * @param {IControlViewModel} control Модель представления проверяемого контрола.
   */
  constructor(control: IControlViewModel) {
    super(control);
  }

  //#endregion

  //#region base overrides

  protected get patterns(): ReadonlyArray<RegExp> {
    return OcrValidator.getPatterns(OcrPatternTypes.Double);
  }

  protected get error(): string {
    return '$UI_Controls_Double_InvalidValue';
  }

  protected compileValue(match: RegExpExecArray): string {
    if (match.groups) {
      const sign = match.groups['sign'] ?? '';
      const integer = match.groups['integer'] ?? '';
      const fractional = match.groups['fractional'] ?? '';
      return fractional ? `${sign}${integer}.${fractional}` : `${sign}${integer}`;
    } else {
      return match[0];
    }
  }

  protected formatNumber(value: number): string {
    return formatDecimal(value, false);
  }

  //#endregion
}
