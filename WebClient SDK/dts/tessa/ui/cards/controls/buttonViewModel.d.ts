/// <reference types="react" />
import { ControlViewModelBase } from './controlViewModelBase';
import { ICardModel } from '../interfaces';
import { CardTypeControl } from 'tessa/cards/types';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { MediaStyle } from 'ui';
/**
 * Модель представления для вывода кнопки.
 */
export declare class ButtonViewModel extends ControlViewModelBase {
    constructor(control: CardTypeControl, model: ICardModel);
    private _cardModel;
    private _text;
    private _command;
    private _await;
    private _onClick;
    private _useAllSpace;
    private _minButtonWidth;
    /**
     * Текст кнопки.
     */
    get text(): string;
    set text(value: string);
    /**
     * Если флаг установлен в true, то кнопка будет задизейблена пока выполняется команда.
     * По умолчанию - true.
     */
    get await(): boolean;
    set await(value: boolean);
    /**
     * Можем ли выполнить команду при нажатии.
     */
    get canExecute(): boolean;
    /**
     * Команда, выполняемая при нажатии на кнопку.
     */
    get onClick(): ((e: React.SyntheticEvent<HTMLButtonElement>) => void) | null;
    set onClick(value: ((e: React.SyntheticEvent<HTMLButtonElement>) => void) | null);
    /**
     * Определяет, должна ли кнопка использовать все свободное пространство.
     */
    get useAllSpace(): boolean;
    set useAllSpace(value: boolean);
    /**
     * Минимальная ширина кнопки в пикселях. По умолчанию 100.
     */
    get minButtonWidth(): number;
    set minButtonWidth(value: number);
    executeOnClick(e: React.SyntheticEvent<HTMLButtonElement>): Promise<void>;
    getControlStyle(): MediaStyle | null;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
