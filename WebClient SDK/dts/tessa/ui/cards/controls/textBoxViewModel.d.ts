import { ControlViewModelBase } from './controlViewModelBase';
import { ControlKeyDownEventArgs, ICardModel } from '../interfaces';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { TextBoxMode } from 'ui/textField/textBoxMode';
import { EventHandler } from 'tessa/platform';
import { MediaStyle } from 'ui/mediaStyle';
import { AvalonTextBoxFontType } from 'tessa/cards/avalonTextBoxFontType';
import { SyntaxHighlighting } from 'tessa/cards/syntaxHighlighting';
import type { ValidationResultBuilder } from 'tessa/platform/validation';
import { ControlButtonsContainer } from './controlButtonsContainer';
/**
 * Модель представления для элемента управления, выполняющего ввод текстовых строк.
 */
export declare class TextBoxViewModel extends ControlViewModelBase {
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private readonly _buttonsContainer;
    private _fields;
    private _isNullable;
    private _fieldNames;
    private readonly _defaultFieldName;
    private readonly _defaultFieldType;
    private _displayFormat;
    /**
     * Признак необходимости выполнения проверки орфографии при редактировании.
     */
    private _spellcheck;
    /**
     * Максимальная длина введённой текстовой строки.
     */
    readonly maxLength: number;
    /**
     * Минимальное количество строк.
     */
    readonly minRows: number;
    /**
     * Максимальное количество строк.
     */
    readonly maxRows: number;
    /**
     * Способ отображения элемента управления.
     */
    readonly textBoxMode: TextBoxMode;
    readonly avalonFontType: AvalonTextBoxFontType;
    readonly avalonShowLineNumbers: boolean;
    readonly avalonSyntaxType: SyntaxHighlighting;
    /**
     * Текстовое представление введённой строки.
     * Для элементов управления, доступных только для чтения,
     * строка возвращается в локализованном виде.
     */
    get text(): string;
    set text(value: string);
    /**
     * Формат поля. Применяется только в случае, когда контрол доступен только для чтения.
     * Значение null или пустая строка определяют отсутствие дополнительного форматирования.
     */
    get displayFormat(): string | null;
    set displayFormat(value: string | null);
    get error(): string | null;
    get hasEmptyValue(): boolean;
    get buttonsContainer(): ControlButtonsContainer;
    private getFormattedText;
    getControlStyle(): MediaStyle | null;
    selectAll(): void;
    private static defaultKeyDown;
    private setGuidValue;
    getInternalValidation(value: string): string | null;
    /**
     * Получить признак необходимости выполнения проверки орфографии при редактировании.
     */
    get spellcheck(): boolean;
    /**
     * Установить признак необходимости выполнения проверки орфографии при редактировании.
     */
    set spellcheck(value: boolean);
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
