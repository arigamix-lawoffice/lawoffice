import { CardMetadataColumnType } from './cardMetadataColumnType';
import { CardMetadataSectionReference, CardMetadataSectionReferenceSealed } from './cardMetadataSectionReference';
import { CardMetadataType, CardMetadataTypeSealed } from './cardMetadataType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataColumnSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly columnType: CardMetadataColumnType;
    readonly metadataType: CardMetadataTypeSealed | null;
    readonly defaultValue: any | null;
    readonly defaultValidValue: any | null;
    readonly cardTypeIdList: ReadonlyArray<guid>;
    readonly referencedSection: CardMetadataSectionReferenceSealed | null;
    readonly parentRowSection: CardMetadataSectionReferenceSealed | null;
    readonly complexColumnIndex: number;
    readonly isReference: boolean;
    seal<T = CardMetadataColumnSealed>(): T;
}
/**
 * Содержит метаинформацию о колонке секции.
 */
export declare class CardMetadataColumn extends CardSerializableObject {
    constructor();
    private _cardTypeIdList;
    id: guid | null;
    name: string | null;
    columnType: CardMetadataColumnType;
    metadataType: CardMetadataType | null;
    defaultValue: any | null;
    defaultValidValue: any | null;
    get cardTypeIdList(): guid[];
    set cardTypeIdList(value: guid[]);
    referencedSection: CardMetadataSectionReference | null;
    parentRowSection: CardMetadataSectionReference | null;
    complexColumnIndex: number;
    isReference: boolean;
    seal<T = CardMetadataColumnSealed>(): T;
    static createFrom(column: CardMetadataColumn, cardTypeId: string): CardMetadataColumn;
}
