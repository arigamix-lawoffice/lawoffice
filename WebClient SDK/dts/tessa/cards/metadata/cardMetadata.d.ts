import { CardMetadataSection, CardMetadataSectionSealed } from './cardMetadataSection';
import { CardMetadataEnumeration, CardMetadataEnumerationSealed } from './cardMetadataEnumeration';
import { CardMetadataCompletionOption, CardMetadataCompletionOptionSealed } from './cardMetadataCompletionOption';
import { CardType, CardTypeSealed } from 'tessa/cards/types';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
import { CardMetadataFunctionRoleSealed, CardMetadataFunctionRole } from './cardMetadataFunctionRole';
export interface ICardMetadata {
    sections: CardMetadataSection[];
    cardTypes: CardType[];
    enumerations: CardMetadataEnumeration[];
    seal<T = CardMetadataSealed>(): T;
    getSectionById(sectionId: guid): CardMetadataSection | undefined;
    getSectionByName(sectionName: string): CardMetadataSection | undefined;
    getChildRowSectionsById(sectionId: guid): CardMetadataSection[];
    getChildRowSectionsByName(sectionName: string): CardMetadataSection[];
    getCardTypeById(cardTypeId: guid): CardType | undefined;
    getCardTypeByName(cardTypeName: string): CardType | undefined;
    getMetadataForType(cardTypeId: guid): ICardMetadata;
    getCompletionOptions(): CardMetadataCompletionOption[];
    getFunctionRoles(): CardMetadataFunctionRole[];
}
export interface CardMetadataSealed {
    sections: ReadonlyArray<CardMetadataSectionSealed>;
    cardTypes: ReadonlyArray<CardTypeSealed>;
    enumerations: ReadonlyArray<CardMetadataEnumerationSealed>;
    seal<T = CardMetadataSealed>(): T;
    getSectionById(sectionId: guid): CardMetadataSectionSealed | undefined;
    getSectionByName(sectionName: string): CardMetadataSectionSealed | undefined;
    getChildRowSectionsById(sectionId: guid): CardMetadataSectionSealed[];
    getChildRowSectionsByName(sectionName: string): CardMetadataSectionSealed[];
    getCardTypeById(cardTypeId: guid): CardTypeSealed | undefined;
    getCardTypeByName(cardTypeName: string): CardTypeSealed | undefined;
    getMetadataForType(cardTypeId: guid): ICardMetadata;
    getCompletionOptions(): CardMetadataCompletionOption[];
    getFunctionRoles(): CardMetadataFunctionRoleSealed[];
}
/**
 * Содержит метаинформацию, необходимую для использования типов карточек
 * совместно с пакетом карточек.
 */
export declare class CardMetadata extends CardSerializableObject implements ICardMetadata {
    constructor();
    private _cardTypeCache;
    private _sections;
    private _cardTypes;
    private _enumerations;
    get sections(): CardMetadataSection[];
    set sections(value: CardMetadataSection[]);
    get cardTypes(): CardType[];
    set cardTypes(value: CardType[]);
    get enumerations(): CardMetadataEnumeration[];
    set enumerations(value: CardMetadataEnumeration[]);
    seal<T = CardMetadataSealed>(): T;
    getSectionById(sectionId: guid): CardMetadataSection | undefined;
    getSectionByName(sectionName: string): CardMetadataSection | undefined;
    getChildRowSectionsById(sectionId: guid): CardMetadataSection[];
    getChildRowSectionsByName(sectionName: string): CardMetadataSection[];
    getCardTypeById(cardTypeId: guid): CardType | undefined;
    getCardTypeByName(cardTypeName: string): CardType | undefined;
    getMetadataForType(cardTypeId: guid): ICardMetadata;
    getCompletionOptions(): CardMetadataCompletionOptionSealed[];
    /**
     * Функциональные роли заданий. Идентификаторы типовых ролей перечислены в классе <see cref="CardFunctionRoles"/>.
     */
    getFunctionRoles(): CardMetadataFunctionRoleSealed[];
    static createCopy(cardMetadata: CardMetadataSealed): CardMetadata;
}
