import { ValidationLevel, ValidationResultItem, ValidationResultType } from 'tessa/platform/validation';
export declare class ValidationResult {
    constructor(items: ValidationResultItem[]);
    readonly items: ReadonlyArray<ValidationResultItem>;
    get hasErrors(): boolean;
    get hasWarnings(): boolean;
    get hasInfo(): boolean;
    get isSuccessful(): boolean;
    static get empty(): ValidationResult;
    format(): string;
    toString(level?: ValidationLevel): string;
    /**
     * Returns the validation result for the exception.
     * @param error The exception for which to get the validation result, or null if you want to return an {@link ValidationResult.empty} object.
     * @param warning An indication that a warning message should be returned {@link ValidationResultType.Warning} instead of an error message ({@link ValidationResultType.Error}).
     * @param message The message, or null, undefined, empty string if a short description of the exception is displayed as the message. The full text of the exception is always displayed in detail.
     * @param additionalDetails Additional information that appears after the exception text in the message details. If equal to null, undefined or an empty string, then only an exception is displayed in the details.
     * @returns Validation result for the exception.
     */
    static fromError(error: Error | string | null | undefined | any, warning?: boolean, message?: string | null, additionalDetails?: string | null): ValidationResult;
    static fromText(text: string, type?: ValidationResultType): ValidationResult;
}
