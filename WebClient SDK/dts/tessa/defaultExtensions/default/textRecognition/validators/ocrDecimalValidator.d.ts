import { IControlViewModel } from 'tessa/ui/cards';
import { OcrDoubleValidator } from './ocrDoubleValidator';
import { ValidationResult } from 'tessa/platform/validation';
/** Валидатор значения вещественного числа с фиксированной запятой. */
export declare class OcrDecimalValidator extends OcrDoubleValidator {
    /** Точность числа (кол-во цифр после точки). */
    private readonly _digitsAfterSeparator;
    /**
     * Создает экземпляр класса {@link OcrDecimalValidator}.
     * @param {IControlViewModel} control Модель представления проверяемого контрола.
     */
    constructor(control: IControlViewModel);
    /**
     * Выполняет проверку, что входное значение имеет заданную точность.
     * @param value Проверяемое значение.
     * @param precision Точность - кол-во цифр после точки.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    private validatePrecision;
    protected get error(): string;
    protected validateString(value: string): ValidationResult | null;
    protected formatNumber(value: number): string;
}
