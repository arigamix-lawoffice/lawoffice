import { CardTypeColumn, CardTypeColumnSealed } from './cardTypeColumn';
import { CardTypeContent, CardTypeContentSealed } from './cardTypeContent';
import { CardTypeCustomControlFlags } from './cardTypeCustomControlFlags';
import { CardTypeEntryControlFlags } from './cardTypeEntryControlFlags';
import { CardTypeTabControlFlags } from './cardTypeTabControlFlags';
import { CardTypeTableControlFlags } from './cardTypeTableControlFlags';
import { CardControlSourceInfo } from '../cardControlSourceInfo';
import { CardControlType } from 'tessa/cards/cardControlType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
import { CardMetadataRuntimeType } from 'tessa/cards/metadata/cardMetadataRuntimeType';
import { CardMetadataSection, CardMetadataSectionSealed } from 'tessa/cards/metadata/cardMetadataSection';
import { Visibility } from 'tessa/platform/visibility';
import { Margin } from 'tessa/platform';
import { MediaStyle } from 'ui/mediaStyle';
export declare type TypeSettings = {
    [key: string]: null | boolean | number | string | string[] | Array<TypeSettings> | TypeSettings;
};
export declare type TypeSettingsSealed = {
    readonly [key: string]: null | boolean | number | string | string[] | ReadonlyArray<TypeSettings> | TypeSettings;
};
export interface CardTypeBlockSealed extends CardTypeContentSealed {
    readonly name: string | null;
    readonly blockClass: string | null;
    readonly blockSettings: TypeSettingsSealed;
    readonly formSettings: TypeSettingsSealed;
    readonly controls: ReadonlyArray<CardTypeControlSealed>;
    seal<T = CardTypeBlockSealed>(): T;
    getCaptionVisibility(): Visibility;
    getBlockVisibility(): Visibility;
    getColumnsCount(): number;
}
/**
 * Блок типа карточки, содержащий отображаемые единым образом контролы для полей и секций карточки.
 * Блок может определять способ расположения контролов в одноколоночном или
 * многоколоночном интерфейсах, а также определяемый пользователем способ через
 * имя класса blockClass и его настройки blockSettings.
 */
export declare class CardTypeBlock extends CardTypeContent {
    constructor();
    _blockClass: string | null;
    private _blockSettings;
    private _formSettings;
    private _controls;
    /**
     * Имя блока. Может использоваться в пользовательком классе формы для вставки содержимого блока
     * в определённое место.
     */
    name: string | null;
    /**
     * Полное имя типа для класса, выполняющего отображение блока карточки в UI.
     */
    get blockClass(): string | null;
    set blockClass(value: string | null);
    /**
     * Настройки класса, полное имя типа которого задано в свойстве blockClass.
     */
    get blockSettings(): TypeSettings;
    set blockSettings(value: TypeSettings);
    /**
     * Настройки формы CardTypeForm, которые задаются внутри каждого включённого в его состав объекта.
     */
    get formSettings(): TypeSettings;
    set formSettings(value: TypeSettings);
    /**
     * Объекты содержимого, определяющие внешний вид карточки.
     */
    get controls(): CardTypeControl[];
    set controls(value: CardTypeControl[]);
    seal<T = CardTypeBlockSealed>(): T;
    getCaptionVisibility(): Visibility;
    getBlockVisibility(): Visibility;
    getColumnsCount(): number;
}
export interface CardTypeFormSealed {
    readonly formClass: string | null;
    readonly formSettings: TypeSettingsSealed;
    readonly blocks: ReadonlyArray<CardTypeBlock>;
    seal<T = CardTypeFormSealed>(): T;
    formIsEmpty(): boolean;
}
/**
 * Объект, описывающий базовую информацию о форме карточки.
 * Тип карточки CardType - это частный случай формы.
 */
export declare abstract class CardTypeForm extends CardSerializableObject {
    constructor();
    _formClass: string | null;
    private _formSettings;
    private _blocks;
    /**
     * Полное имя типа для класса, выполняющего отображение формы карточки в UI.
     */
    get formClass(): string | null;
    set formClass(value: string | null);
    /**
     * Настройки класса, полное имя типа которого задано в свойстве formClass.
     */
    get formSettings(): TypeSettings;
    set formSettings(value: TypeSettings);
    /**
     * Блоки типа карточки, определяющие внешний вид карточки.
     */
    get blocks(): CardTypeBlock[];
    set blocks(value: CardTypeBlock[]);
    seal<T = CardTypeFormSealed>(): T;
    /**
     * Возвращает признак того, что форма не содержит отображаемых блоков.
     * @returns true, если форма не содержит отображаемых блоков; false в противном случае.
     */
    formIsEmpty(): boolean;
}
export interface CardTypeTabFormSealed extends CardTypeFormSealed {
    readonly name: string | null;
    readonly tabCaption: string | null;
    seal<T = CardTypeTabFormSealed>(): T;
}
/**
 * Объект, описывающий базовую информацию о форме карточки, которая выводится во вкладке.
 * Тип карточки CardType - это частный случай формы.
 */
export declare abstract class CardTypeTabForm extends CardTypeForm {
    constructor();
    protected _name: string | null;
    get name(): string | null;
    set name(value: string | null);
    /**
     * Заголовок вкладки с формой.
     */
    tabCaption: string | null;
    seal<T = CardTypeTabFormSealed>(): T;
}
export interface CardTypeNamedFormSealed extends CardTypeTabFormSealed {
    readonly name: string | null;
    seal<T = CardTypeNamedFormSealed>(): T;
}
/**
 * Именованный объект, описывающий пользовательский интерфейс для редактирования карточки.
 */
export declare class CardTypeNamedForm extends CardTypeTabForm {
    constructor();
    protected _name: string | null;
    static mainFormDefaultName: string;
    /**
     * Имя формы, уникальное в пределах типа карточки, по которому можно сослаться на форму.
     * Используется в вариантах завершения заданий.
     */
    get name(): string | null;
    set name(value: string | null);
    seal<T = CardTypeNamedFormSealed>(): T;
}
export interface CardTypeTabControlFormSealed extends CardTypeTabFormSealed {
    seal<T = CardTypeTabControlFormSealed>(): T;
}
/**
 * Объект, описывающий пользовательский интерфейс для вкладки карточки,
 * который используется в элементе управления CardTypeTabControl.
 */
export declare class CardTypeTabControlForm extends CardTypeTabForm {
    get name(): string;
    set name(value: string);
    seal<T = CardTypeTabControlFormSealed>(): T;
}
export interface CardTypeTableFormSealed extends CardTypeFormSealed {
    seal<T = CardTypeTableFormSealed>(): T;
}
/**
 * Объект, описывающий пользовательский интерфейс для редактирования строки коллекционной
 * или древовидной секции.
 */
export declare class CardTypeTableForm extends CardTypeForm {
    seal<T = CardTypeTableFormSealed>(): T;
}
export interface CardTypeControlSealed extends CardTypeContentSealed {
    readonly name: string | null;
    readonly toolTip: string | null;
    readonly type: CardControlType;
    readonly controlSettings: TypeSettingsSealed;
    readonly blockSettings: TypeSettingsSealed;
    seal<T = CardTypeControlSealed>(): T;
    isRequired(): boolean;
    isVisible(): boolean;
    getCaptionVisibility(): Visibility;
    getControlSpan(): boolean;
    getSourceInfo(): CardControlSourceInfo;
}
/**
 * Базовый объект, который может включаться в состав блока типа карточки CardTypeBlock.
 */
export declare abstract class CardTypeControl extends CardTypeContent {
    constructor();
    private _controlSettings;
    private _blockSettings;
    static readonly entryTypeCode = 0;
    static readonly tableTypeCode = 1;
    static readonly tabTypeCode = 2;
    static readonly customTypeCode = 3;
    /**
     * Имя элемента управления или null, если имя не задано.
     * При задании пустой строки устанавливается значение null.
     * Рекомендуется задавать имя, уникальное для формы.
     */
    name: string | null;
    /**
     * Текст всплывающей подсказки для элемента управления или null, если имя не задано.
     * При задании пустой строки или строки, состоящей из пробелов, устанавливается значение null.
     */
    toolTip: string | null;
    /**
     * Тип используемого элемента управления.
     */
    type: CardControlType;
    /**
     * Настройки используемого элемента управления, тип которого задан в свойстве Type.
     */
    get controlSettings(): TypeSettings;
    set controlSettings(value: TypeSettings);
    /**
     * Настройки блока CardTypeBlock, которые задаются для каждого включённого в его состав объекта.
     */
    get blockSettings(): TypeSettings;
    set blockSettings(value: TypeSettings);
    seal<T = CardTypeControlSealed>(): T;
    abstract isRequired(): boolean;
    abstract isVisible(): boolean;
    abstract setRequired(required: boolean): CardTypeControl;
    abstract setVisible(visible: boolean): CardTypeControl;
    getCaptionVisibility(): Visibility;
    getCaptionStyle(): MediaStyle | null;
    getControlStyle(): MediaStyle | null;
    getMarginStyle(): Margin | null;
    getControlSpan(): boolean;
    private getTextStyle;
    getSourceInfo(): CardControlSourceInfo;
}
export interface CardTypeEntryControlSealed extends CardTypeControlSealed {
    readonly complexColumnId: guid | null;
    readonly physicalColumnIdList: ReadonlyArray<guid>;
    readonly displayFormat: string | null;
    readonly requiredText: string | null;
    readonly sectionId: guid | null;
    readonly flags: CardTypeEntryControlFlags;
    seal<T = CardTypeEntryControlSealed>(): T;
    getFieldNames(metadataSection: CardMetadataSection | CardMetadataSectionSealed): {
        fieldNames: string[];
        defaultFieldName: string;
    };
}
/**
 * Объект, описывающий расположение и свойства элемента управления для привязки к
 * полям строковой секции карточки.
 */
export declare class CardTypeEntryControl extends CardTypeControl {
    constructor();
    private _physicalColumnIdList;
    /**
     * Идентификатор комплексной колонки, в которой содержится значение поля,
     * или null, если поле содержится в физической колонке или составлено
     * из нескольких физических колонок.
     */
    complexColumnId: guid | null;
    /**
     * Список идентификаторов физических колонок, которые определяют значение поля.
     */
    get physicalColumnIdList(): guid[];
    set physicalColumnIdList(value: guid[]);
    /**
     * Формат отображаемого в текстовом виде поля.
     * Если задано null или пустая строка, то в текстовом виде поле будет отображаться как
     * значения всех колонок из списка physicalColumnIdList, объединённые пробелами.
     */
    displayFormat: string | null;
    /**
     * Текст, отображаемый при отсутствии значения для контрола, значение которого должно
     * быть обязательно задано.
     * Если задано null или пустая строка, то используется строка из валидатора или
     * строка по умолчанию.
     */
    requiredText: string | null;
    /**
     * Идентификатор секции, содержащей колонку с полем complexColumnId.
     */
    sectionId: guid | null;
    /**
     * Флаги, определяющие дополнительные атрибуты.
     */
    flags: CardTypeEntryControlFlags;
    seal<T = CardTypeEntryControlSealed>(): T;
    isRequired(): boolean;
    isVisible(): boolean;
    setRequired(required: boolean): CardTypeControl;
    setVisible(visible: boolean): CardTypeControl;
    getFieldNames(metadataSection: CardMetadataSection): {
        fieldNames: string[];
        defaultFieldName: string;
        defaultFieldType: CardMetadataRuntimeType;
    };
}
export interface CardTypeTableControlSealed extends CardTypeControlSealed {
    readonly sectionId: guid | null;
    readonly flags: CardTypeTableControlFlags;
    readonly requiredText: string | null;
    readonly form: CardTypeTableForm | null;
    readonly columns: ReadonlyArray<CardTypeColumnSealed>;
    seal<T = CardTypeTableControlSealed>(): T;
}
/**
 * Объект, описывающий расположение и свойства элемента управления для привязки к колонкам
 * коллекционной или древовидной секции карточки.
 */
export declare class CardTypeTableControl extends CardTypeControl {
    constructor();
    private _columns;
    /**
     * Идентификатор секции.
     */
    sectionId: guid | null;
    /**
     * Флаги, определяющие дополнительные атрибуты.
     */
    flags: CardTypeTableControlFlags;
    /**
     * Текст, отображаемый при отсутствии значения для контрола, значение которого должно быть
     * обязательно задано.
     * Если задано null или пустая строка, то используется строка из валидатора или строка
     * по умолчанию.
     */
    requiredText: string | null;
    /**
     * Объект, описывающий пользовательский интерфейс для редактирования строки коллекционной
     * или древовидной секции.
     */
    form: CardTypeTableForm | null;
    /**
     * Объекты, описывающие информацию о колонках коллекционных или древовидных секций карточки.
     */
    get columns(): CardTypeColumn[];
    set columns(value: CardTypeColumn[]);
    seal<T = CardTypeTableControlSealed>(): T;
    isRequired(): boolean;
    isVisible(): boolean;
    setRequired(required: boolean): CardTypeControl;
    setVisible(visible: boolean): CardTypeControl;
}
export interface CardTypeTabControlSealed extends CardTypeControlSealed {
    readonly flags: CardTypeTabControlFlags;
    readonly forms: ReadonlyArray<CardTypeTabControlFormSealed>;
    seal<T = CardTypeTabControlSealed>(): T;
}
/**
 * Объект, описывающий расположение и свойства элемента управления для вывода
 * списка форм CardTypeTabControlForm во вкладках.
 */
export declare class CardTypeTabControl extends CardTypeControl {
    constructor();
    private _forms;
    /**
     * Флаги, определяющие дополнительные атрибуты.
     */
    flags: CardTypeTabControlFlags;
    /**
     * Список форм, выводимый во вкладках.
     */
    get forms(): CardTypeTabControlForm[];
    set forms(value: CardTypeTabControlForm[]);
    seal<T = CardTypeTabControlSealed>(): T;
    isRequired(): boolean;
    isVisible(): boolean;
    setRequired(_required: boolean): CardTypeControl;
    setVisible(visible: boolean): CardTypeControl;
}
export interface CardTypeCustomControlSealed extends CardTypeControlSealed {
    readonly flags: CardTypeCustomControlFlags;
    readonly requiredText: string | null;
    seal<T = CardTypeCustomControlSealed>(): T;
}
/**
 * Объект, описывающий элемент управления на карточке, особым образом привязанный к данным карточки
 * или не привязан вообще.
 */
export declare class CardTypeCustomControl extends CardTypeControl {
    constructor();
    /**
     * Флаги, определяющие дополнительные атрибуты.
     */
    flags: CardTypeCustomControlFlags;
    /**
     * Текст, отображаемый при отсутствии значения для контрола, значение которого должно
     * быть обязательно задано.
     * Если задано null или пустая строка, то используется строка из валидатора или
     * строка по умолчанию.
     */
    requiredText: string | null;
    seal<T = CardTypeCustomControlSealed>(): T;
    isRequired(): boolean;
    isVisible(): boolean;
    setRequired(required: boolean): CardTypeControl;
    setVisible(visible: boolean): CardTypeControl;
}
