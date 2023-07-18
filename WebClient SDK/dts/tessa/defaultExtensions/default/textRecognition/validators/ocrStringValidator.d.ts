import { IControlViewModel } from 'tessa/ui/cards';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
/** Валидатор строкового значения. */
export declare class OcrStringValidator extends OcrValidator {
    /** Максимальная длина строки. */
    private readonly _maxLength;
    /**
     * Создает экземпляр класса {@link OcrStringValidator}.
     * @param {IControlViewModel} control Модель представления проверяемого контрола.
     */
    constructor(control: IControlViewModel);
    protected get patterns(): ReadonlyArray<RegExp>;
    protected get error(): string;
    protected compileValue(match: RegExpExecArray): string;
    validate(value: string): OcrValidationResult;
}
