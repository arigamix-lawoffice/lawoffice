import { IControlViewModel } from 'tessa/ui/cards';
import { OcrNumberValidator } from './ocrNumberValidator';
/** Валидатор значения вещественного числа с плавающей запятой. */
export declare class OcrDoubleValidator extends OcrNumberValidator {
    /**
     * Создает экземпляр класса {@link OcrDoubleValidator}.
     * @param {IControlViewModel} control Модель представления проверяемого контрола.
     */
    constructor(control: IControlViewModel);
    protected get patterns(): ReadonlyArray<RegExp>;
    protected get error(): string;
    protected compileValue(match: RegExpExecArray): string;
    protected formatNumber(value: number): string;
}
