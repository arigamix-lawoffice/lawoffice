import { IControlViewModel } from 'tessa/ui/cards';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
import { ValidationResult } from 'tessa/platform/validation';
/** Валидатор числового значения. */
export declare abstract class OcrNumberValidator extends OcrValidator {
    /** Минимальное значение числа. */
    private readonly _minValue;
    /** Максимальное значение числа. */
    private readonly _maxValue;
    /**
     * Создает экземпляр класса {@link OcrNumberValidator}.
     * @param {IControlViewModel} control Модель представления проверяемого контрола.
     */
    protected constructor(control: IControlViewModel);
    /**
     * Выполняет проверку, что входное значение является числом.
     * @param value Проверяемое значение.
     * @param message Сообщение об ошибке.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    private validateValue;
    /**
     * Выполняет проверку, что входное значения больше либо равно минимальному.
     * @param value Проверяемое значение.
     * @param message Сообщение об ошибке.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    private validateMinValue;
    /**
     * Выполняет проверку, что входное значения меньше либо равно максимальному.
     * @param value Проверяемое значение.
     * @param message Сообщение об ошибке.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    private validateMaxValue;
    /**
     * Выполняет проверку значения, преобразованного в формат, поддерживаемый системой.
     * @param _value Проверяемое значение.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    protected validateString(_value: string): ValidationResult | null;
    /**
     * Выполняет проверку входного значения.
     * @param value Проверяемое значение.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    protected validateNumber(value: number): ValidationResult | null;
    /**
     * Выполняет преобразование значения в форматированную строку.
     * @param value Значение для форматирования.
     * @returns Отформатированное значение в виде строки.
     */
    protected abstract formatNumber(value: number): string;
    validate(value: string): OcrValidationResult;
}
