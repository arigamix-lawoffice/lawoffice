import { OcrPatternTypes } from '../misc/ocrTypes';
import { OcrSettings } from '../misc/ocrSettings';
import { OcrValidationResult } from './ocrValidationResult';

/** Абстрактный класс, описывающий базовый функционал валидатора. */
export abstract class OcrValidator {
  //#region protected properties

  /**
   * Набор регулярных выражений, используемых для
   * проверки значения на принадлежность к шаблону.
   */
  protected abstract get patterns(): ReadonlyArray<RegExp>;

  /**
   * Общее сообщение об ошибке, которое будет использоваться,
   * если попытка получения значения будет неуспешной.
   */
  protected abstract get error(): string;

  //#endregion

  //#region protected methods

  /**
   * Возвращает набор шаблонов для проверки входного значения.
   * @param type Тип шаблона для проверки значения.
   * @returns Набор шаблонов для проверки входного значения.
   */
  protected static getPatterns(type: OcrPatternTypes): ReadonlyArray<RegExp> {
    const patterns = OcrSettings.instance.patterns.get(type);
    if (!patterns) {
      throw new Error(`Can not find patterns at OCR settings card for type ${type}.`);
    } else {
      return patterns;
    }
  }

  /**
   * Преобразование совпадающего значения, в формат, поддерживаемый системой.
   * @param match Найденное совпадение значения одному из шаблонов.
   * @returns Преобразованное значение в формат, поддерживаемый системой.
   */
  protected abstract compileValue(match: RegExpExecArray): string;

  /**
   * Выполняет удаление пробельных символов из строки.
   * @param value Строка со значением.
   * @returns Строка со значением, в которой удалены пробельные символы.
   */
  protected removeSpaces(value: string): string {
    return value.replaceAll(/\s/gm, '');
  }

  //#endregion

  //#region public methods

  /**
   * Проверка значения по заданным алгоритмам.
   * @param value Проверяемое значение в строковом представлении.
   * @returns Информация о результате проверки значения.
   */
  public abstract validate(value: string): OcrValidationResult;

  //#endregion
}
