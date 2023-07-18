import { ControlViewModelBase } from './controlViewModelBase';
import { ICardModel, ControlKeyDownEventArgs } from '../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { EventHandler } from 'tessa/platform';
import { ControlButtonsContainer } from './controlButtonsContainer';
/**
 * Модель представления для элемента управления, выполняющего ввод целых чисел.
 */
export declare class IntegerBoxViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private readonly _buttonsContainer;
    private _fields;
    private _fieldName;
    private _fieldType;
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
    getInternalValidation(value: string): string | null;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
}
