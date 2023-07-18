import { TextBlockViewModelBase } from './textBlockViewModelBase';
import { ICardModel } from '../interfaces';
import { CardTypeCustomControl } from 'tessa/cards/types';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { MediaStyle } from 'ui/mediaStyle';
import React from 'react';
/**
 * Модель представления для элемента управления, выполняющего вывод текстовых строк.
 */
export declare class LabelViewModel extends TextBlockViewModelBase {
    constructor(control: CardTypeCustomControl, model: ICardModel);
    private _cardModel;
    private _text;
    private _hyperlink;
    private _hyperlinkColor;
    private _underline;
    private _linkCommand;
    private _uriLinkDependencies;
    /**
     * Текстовое представление введённой строки.
     */
    get text(): string;
    set text(value: string);
    /**
     * Если флажок установлен, то контрол работает у гиперсслыки есть подчеркивание
     */
    get underline(): boolean;
    set underline(value: boolean);
    /**
     * Если флажок установлен, то контрол работает как гиперссылка
     */
    get hyperlink(): boolean;
    set hyperlink(value: boolean);
    get hyperlinkColor(): string;
    set hyperlinkColor(value: string);
    get linkCommand(): string;
    set linkCommand(value: string);
    /**
     * Команда, выполняемая при нажатии на гиперссылку.
     */
    onClick: ((e: React.SyntheticEvent) => void) | null;
    executeOnClick(e: React.SyntheticEvent): void;
    getControlStyle(): MediaStyle | null;
    onUnloading(validationResult: ValidationResultBuilder): void;
    private defaultCommandOnClick;
    private handleGenericLinkClickAsync;
}
