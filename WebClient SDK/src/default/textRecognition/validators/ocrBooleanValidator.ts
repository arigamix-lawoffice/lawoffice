import { formatBoolean } from 'tessa/platform/formatting';
import { IControlViewModel } from 'tessa/ui/cards';
import { localize } from 'tessa/localization';
import { OcrPatternTypes } from '../misc/ocrTypes';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Валидатор булевого значения. */
export class OcrBooleanValidator extends OcrValidator {
  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrBooleanValidator}.
   * @param {IControlViewModel} _control Модель представления проверяемого контрола.
   */
  constructor(_control: IControlViewModel) {
    super();
  }

  //#endregion

  //#region base overrides

  protected get patterns(): ReadonlyArray<RegExp> {
    return OcrValidator.getPatterns(OcrPatternTypes.Boolean);
  }

  protected get error(): string {
    return '$UI_Controls_Boolean_InvalidValue';
  }

  protected compileValue(match: RegExpExecArray): string {
    if (match.groups) {
      const positive = match.groups['positive'] ?? '';
      const negative = match.groups['negative'] ?? '';
      return positive ? 'true' : negative ? 'false' : '';
    } else {
      return match[0];
    }
  }

  //#endregion

  //#region public methods

  public validate(value: string): OcrValidationResult {
    for (const pattern of this.patterns) {
      const match = pattern.exec(value);
      if (match && match.length > 0) {
        const compiledValue = this.compileValue(match);
        const parsedValue = Boolean(compiledValue);
        const formattedValue = localize(formatBoolean(parsedValue));
        let validationResult: ValidationResult | null = null;
        if (formattedValue != value) {
          validationResult = ValidationResult.fromText(formattedValue);
        }
        return new OcrValidationResult(validationResult, compiledValue, formattedValue);
      }
    }

    const result = ValidationResult.fromText(localize(this.error), ValidationResultType.Error);
    return new OcrValidationResult(result);
  }

  //#endregion
}
