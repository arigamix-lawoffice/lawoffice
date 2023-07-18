import { CardTypeForm, CardTypeFormSealed, CardTypeNamedForm, CardTypeNamedFormSealed } from './cardTypeCommon';
import { CardTypeCompletionOption, CardTypeCompletionOptionSealed } from './cardTypeCompletionOption';
import { CardTypeExtension, CardTypeExtensionSealed } from './cardTypeExtension';
import { CardTypeFlags } from './cardTypeFlags';
import { CardTypeSchemeItem, CardTypeSchemeItemSealed } from './cardTypeSchemeItem';
import { CardTypeValidator, CardTypeValidatorSealed } from './cardTypeValidator';
import { CardTypeSection, CardTypeSectionSealed } from '../virtualScheme';
import { CardInstanceType } from 'tessa/cards/cardInstanceType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardTypeSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly instanceType: CardInstanceType;
    readonly flags: CardTypeFlags;
    readonly caption: string | null;
    readonly group: string | null;
    readonly digestFormat: string | null;
    readonly schemeItems: ReadonlyArray<CardTypeSchemeItemSealed>;
    readonly completionOptions: ReadonlyArray<CardTypeCompletionOptionSealed>;
    readonly cardTypeSections: ReadonlyArray<CardTypeSectionSealed>;
    readonly forms: ReadonlyArray<CardTypeNamedFormSealed>;
    readonly extensions: ReadonlyArray<CardTypeExtensionSealed>;
    readonly validators: ReadonlyArray<CardTypeValidatorSealed>;
    seal<T = CardTypeSealed>(): T;
    equals(other: CardType | CardTypeSealed): boolean;
    hasDigestFormat(): boolean;
    tryGetCompletionOptionValidators(form: CardTypeForm | CardTypeFormSealed): ReadonlyArray<CardTypeValidatorSealed> | null;
}
export declare class CardType extends CardSerializableObject {
    constructor();
    private _schemeItems;
    private _completionOptions;
    private _cardTypeSections;
    private _forms;
    private _extensions;
    private _validators;
    protected _name: string | null;
    formatVersion: number;
    /**
     * ID объекта.
     */
    id: guid | null;
    /**
     * Наименование объекта.
     */
    get name(): string | null;
    set name(value: string | null);
    /**
     * Тип экземпляра карточки.
     */
    instanceType: CardInstanceType;
    /**
     * Флаги типа карточки.
     */
    flags: CardTypeFlags;
    /**
     * Отображаемое имя типа карточки.
     */
    caption: string | null;
    /**
     * Название группы для типа карточки. Может быть равно null, если группа не задана.
     */
    group: string | null;
    /**
     * Формат функции Digest для карточки. Актуально только для типов CardInstanceType.Card.
     * Чтобы определить, действительно ли строка содержит строку формата,
     * можно использовать метод hasDigestFormat.
     */
    digestFormat: string | null;
    /**
     * Метаданные всех секций, входящих в состав типа карточки.
     */
    get schemeItems(): CardTypeSchemeItem[];
    set schemeItems(value: CardTypeSchemeItem[]);
    /**
     * Варианты завершения типа карточки задания.
     */
    get completionOptions(): CardTypeCompletionOption[];
    set completionOptions(value: CardTypeCompletionOption[]);
    get cardTypeSections(): CardTypeSection[];
    set cardTypeSections(value: CardTypeSection[]);
    /**
     * Альтернативные варианты пользовательского интерфейса для редактирования карточки.
     */
    get forms(): CardTypeNamedForm[];
    set forms(value: CardTypeNamedForm[]);
    /**
     * Список расширений для типов карточек.
     */
    get extensions(): CardTypeExtension[];
    set extensions(value: CardTypeExtension[]);
    /**
     * Список валидаторов, используемых при сохранении карточки.
     */
    get validators(): CardTypeValidator[];
    set validators(value: CardTypeValidator[]);
    seal<T = CardTypeSealed>(): T;
    equals(other: CardType): boolean;
    hasDigestFormat(): boolean;
    /**
     * Возвращает список валидаторов, связанных с вариантом завершения, подходящего для заданной,
     * формы задания, или null, если текущий тип не является типом задания, или с формой
     * не связано ни одного или связано более одного варианта завершения.
     * @param form Форма задания, для которой требуется получить валидаторы.
     * @returns Список валидаторов, связанных с вариантом завершения, подходящего для
     * заданной формы задания, или null, если текущий тип не является типом задания, или
     * с формой не связано ни одного или связано более одного варианта завершения.
     */
    tryGetCompletionOptionValidators(form: CardTypeForm): CardTypeValidator[] | null;
}
