import { ControlViewModelBase } from './controlViewModelBase';
import { CardTypeControl } from 'tessa/cards/types';
/**
 * Базовый объект для модели представления для элемента управления,
 * выполняющего вывод текстовых строк.
 */
export declare abstract class TextBlockViewModelBase extends ControlViewModelBase {
    protected constructor(control: CardTypeControl);
    protected _textFontSize: string;
    protected _textColor: string;
    protected _backgroundColor: string;
    protected _borderColor: string;
    protected _borderWidth: string;
    protected _borderRadius: string;
    protected _padding: string;
    get textFontSize(): string;
    set textFontSize(value: string);
    get textColor(): string;
    set textColor(value: string);
    get backgroundColor(): string;
    set backgroundColor(value: string);
    get borderColor(): string;
    set borderColor(value: string);
    get borderWidth(): string;
    set borderWidth(value: string);
    get borderRadius(): string;
    set borderRadius(value: string);
    get padding(): string;
    set padding(value: string);
}
