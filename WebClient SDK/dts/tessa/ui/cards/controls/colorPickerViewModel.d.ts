import { ControlViewModelBase } from './controlViewModelBase';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { ICardModel, ControlKeyDownEventArgs } from '../interfaces';
import { EventHandler } from 'tessa/platform';
/**
 * Создаёт экземпляр класса с указанием метаинформации по элементу управления и модели карточки.
 */
export declare class ColorPickerViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
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
    getInternalValidation(value: string): string | null;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
}
