import { OcrPatternTypes } from '../misc/ocrTypes';
import { OcrValidationResult } from './ocrValidationResult';
/** Абстрактный класс, описывающий базовый функционал валидатора. */
export declare abstract class OcrValidator {
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
    /**
     * Возвращает набор шаблонов для проверки входного значения.
     * @param type Тип шаблона для проверки значения.
     * @returns Набор шаблонов для проверки входного значения.
     */
    protected static getPatterns(type: OcrPatternTypes): ReadonlyArray<RegExp>;
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
    protected removeSpaces(value: string): string;
    /**
     * Проверка значения по заданным алгоритмам.
     * @param value Проверяемое значение в строковом представлении.
     * @returns Информация о результате проверки значения.
     */
    abstract validate(value: string): OcrValidationResult;
}
