import { IControlViewModel } from 'tessa/ui/cards';
import { localize } from 'tessa/localization';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
import { tryGetFromSettings } from 'tessa/ui';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Валидатор строкового значения. */
export class OcrStringValidator extends OcrValidator {
  //#region fields

  /** Максимальная длина строки. */
  private readonly _maxLength: number;

  //#endregion

  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrStringValidator}.
   * @param {IControlViewModel} control Модель представления проверяемого контрола.
   */
  constructor(control: IControlViewModel) {
    super();
    const settings = control.cardTypeControl.controlSettings;
    this._maxLength = tryGetFromSettings(settings, 'MaxLength', Number.MAX_SAFE_INTEGER);
  }

  //#endregion

  //#region base overrides

  protected get patterns(): ReadonlyArray<RegExp> {
    return [];
  }

  protected get error(): string {
    return '$UI_Cards_TypesEditor_MaximumLength';
  }

  protected compileValue(match: RegExpExecArray): string {
    return match[0];
  }

  public validate(value: string): OcrValidationResult {
    return value.length > this._maxLength
      ? new OcrValidationResult(
          ValidationResult.fromText(localize(this.error), ValidationResultType.Error),
          value
        )
      : new OcrValidationResult(null, value);
  }

  //#endregion
}
