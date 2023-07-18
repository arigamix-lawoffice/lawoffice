import { AvalonTextBoxFontType } from 'tessa/cards/avalonTextBoxFontType';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { ICardModel } from 'tessa/ui/cards';
import { SyntaxHighlighting } from 'tessa/cards/syntaxHighlighting';
import { TextBoxMode } from 'ui';
/** Модель представления для элемента управления, выполняющего ввод текстовых строк. */
export declare class OcrTextBoxViewModel {
    /**
     * Создает экземпляр класса {@link OcrTextBoxViewModel}.
     * @param {CardTypeEntryControl} control
     * Объект, описывающий расположение и свойства элемента
     * управления для привязки к полям строковой секции карточки.
     * @param {ICardModel} _model Модель карточки, доступная в UI.
     */
    constructor(control: CardTypeEntryControl, _model: ICardModel);
    private _displayFormat;
    private _spellcheck;
    /** Максимальная длина введённой текстовой строки. */
    readonly maxLength: number;
    /** Минимальное количество строк. */
    readonly minRows: number;
    /** Максимальное количество строк. */
    readonly maxRows: number;
    /** Способ отображения элемента управления.*/
    readonly textBoxMode: TextBoxMode;
    /** Тип используемого шрифта в строковом поле Avalon. */
    readonly avalonFontType: AvalonTextBoxFontType;
    /** Отображать номера строк в строковом поле Avalon. */
    readonly avalonShowLineNumbers: boolean;
    /** Синтаксис подсветки строк в строковом поле Avalon. */
    readonly avalonSyntaxType: SyntaxHighlighting;
    /**
     * Формат поля. Применяется только в случае, когда контрол доступен только для чтения.
     * Значение `null` или пустая строка определяют отсутствие дополнительного форматирования.
     */
    get displayFormat(): string | null;
    set displayFormat(value: string | null);
    /** Признак необходимости выполнения проверки орфографии при редактировании. */
    get spellcheck(): boolean;
    set spellcheck(value: boolean);
}
