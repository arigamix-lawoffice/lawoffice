import { ValidationResult } from 'tessa/platform/validation/validationResult';
/**
 * Представляет собой ошибку, связанную с неуспешным результатом валидации ValidationResult.
 */
export declare class ValidationError extends Error {
    readonly Result: ValidationResult;
    constructor(result: ValidationResult);
}
