import { IControlViewModel } from 'tessa/ui/cards';
import { OcrNumberValidator } from './ocrNumberValidator';
import { ValidationResult } from 'tessa/platform/validation';
/** Валидатор целочисленного значения. */
export declare class OcrIntegerValidator extends OcrNumberValidator {
    /** Признак, что число является без знаковым. */
    private readonly _unsigned;
    /**
     * Создает экземпляр класса {@link OcrIntegerValidator}.
     * @param {IControlViewModel} control Модель представления проверяемого контрола.
     * @param {boolean} unsigned Признак, что число является без знаковым.
     */
    constructor(control: IControlViewModel, unsigned: boolean);
    /**
     * Выполняет проверку, что входное значение является положительным.
     * @param value Проверяемое значение.
     * @returns Результат проверки с сообщением об ошибке или `null`, если проверка прошла успешно.
     */
    private validateUnsigned;
    protected get patterns(): ReadonlyArray<RegExp>;
    protected get error(): string;
    protected compileValue(match: RegExpExecArray): string;
    protected validateNumber(value: number): ValidationResult | null;
    protected formatNumber(value: number): string;
}
