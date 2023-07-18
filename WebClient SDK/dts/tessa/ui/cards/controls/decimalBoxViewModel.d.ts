import { ControlViewModelBase } from './controlViewModelBase';
import { ICardModel, ControlKeyDownEventArgs } from '../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { EventHandler } from 'tessa/platform';
import { ControlButtonsContainer } from './controlButtonsContainer';
/**
 * Модель представления для элемента управления, выполняющего ввод десятичных чисел.
 */
export declare class DecimalBoxViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private readonly _buttonsContainer;
    private _fields;
    private _fieldName;
    private _digitsAfterSeparator;
    /**
     * Количество цифр после десятичного разделителя.
     */
    get digitsAfterSeparator(): number;
    set digitsAfterSeparator(value: number);
    /**
     * Минимальное допустимое значение введённого числа.
     */
    readonly minValue: number;
    /**
     * Максимальное допустимое значение введённого числа.
     */
    readonly maxValue: number;
    /**
     * Текстовое представление введённого числа.
     */
    get text(): string;
    set text(value: string);
    get error(): string | null;
    get hasEmptyValue(): boolean;
    get buttonsContainer(): ControlButtonsContainer;
    private isEditable;
    private static trimTrailingZeroes;
    private decimalFormat;
    fixInputValue(value: string): string;
    getInternalValidation(value: string): string | null;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
}
