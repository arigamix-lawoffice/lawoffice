import { TextBlockViewModelBase } from './textBlockViewModelBase';
import { ICardModel } from '../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { MediaStyle } from 'ui/mediaStyle';
import React from 'react';
/**
 * Модель представления для элемента управления, выполняющего ввод текстовых строк.
 */
export declare class TextBlockViewModel extends TextBlockViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    /**
     * Команда, выполняемая при нажатии на гиперссылку.
     */
    onClick: ((e: React.SyntheticEvent) => void) | null;
    private _cardModel;
    private _linkCommand;
    private _hyperlink;
    protected _hyperlinkColor: string;
    private _fields;
    private _fieldNames;
    private readonly _defaultFieldName;
    private _displayFormat;
    /**
     * Текстовое представление введённой строки.
     */
    get text(): string;
    set text(value: string);
    /**
     * Формат поля. Значение null или пустая строка определяют
     * отсутствие дополнительного форматирования.
     */
    get displayFormat(): string | null;
    set displayFormat(value: string | null);
    /**
     * Признак, используется ли контрол в режиме гиперссылки.
     */
    get hyperlink(): boolean;
    set hyperlink(value: boolean);
    /**
     * Цвет текста гиперссылки.
     */
    get hyperlinkColor(): string;
    set hyperlinkColor(value: string);
    get linkCommand(): string;
    set linkCommand(value: string);
    private defaultCommandOnClick;
    private getFormattedText;
    executeOnClick(e: React.SyntheticEvent): void;
    getControlStyle(): MediaStyle | null;
}
