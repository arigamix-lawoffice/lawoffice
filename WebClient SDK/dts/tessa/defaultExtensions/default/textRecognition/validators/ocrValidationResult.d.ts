import { ValidationResult } from 'tessa/platform/validation';
/** Информация о результате проверки значения. */
export declare class OcrValidationResult {
    /** Проверенное значение, преобразованное в формат, поддерживаемый системой. */
    readonly compiledValue: string | null;
    /** Проверенное значение, преобразованное в отображаемый формат. */
    readonly formattedValue: string | null;
    /** Результат проверки значения. */
    readonly validationResult: ValidationResult | null;
    /**
     * Создает экземпляр класса {@link OcrValidationResult}.
     * @param {ValidationResult | null} validationResult Результат проверки значения.
     * @param {string | null} compiledValue
     * Проверенное значение, преобразованное в формат, поддерживаемый системой.
     * Если параметр не задан, то значение по умолчанию - `null`.
     * @param {string | null} formattedValue
     * Проверенное значение, преобразованное в отображаемый формат.
     * Если параметр не задан, то значение по умолчанию - `null`.
     */
    constructor(validationResult: ValidationResult | null, compiledValue?: string | null, formattedValue?: string | null);
}
