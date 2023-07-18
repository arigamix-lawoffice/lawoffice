import { IControlViewModel } from 'tessa/ui/cards';
import { OcrValidationResult } from './ocrValidationResult';
import { OcrValidator } from './ocrValidator';
/** Валидатор булевого значения. */
export declare class OcrBooleanValidator extends OcrValidator {
    /**
     * Создает экземпляр класса {@link OcrBooleanValidator}.
     * @param {IControlViewModel} _control Модель представления проверяемого контрола.
     */
    constructor(_control: IControlViewModel);
    protected get patterns(): ReadonlyArray<RegExp>;
    protected get error(): string;
    protected compileValue(match: RegExpExecArray): string;
    validate(value: string): OcrValidationResult;
}
