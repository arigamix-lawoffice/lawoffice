import { CardMetadataColumn, CardMetadataColumnSealed } from './cardMetadataColumn';
import { CardMetadataSectionFlags } from './cardMetadataSectionFlags';
import { CardSectionType } from 'tessa/cards/cardSectionType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
import { CardTableType } from 'tessa/cards/cardTableType';
import { SchemeTableContentType } from 'tessa/scheme/schemeTableContentType';
export interface CardMetadataSectionSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly sectionType: CardSectionType;
    readonly tableType: SchemeTableContentType;
    readonly sectionTableType: CardTableType;
    readonly order: number;
    readonly flags: CardMetadataSectionFlags;
    readonly columns: ReadonlyArray<CardMetadataColumnSealed>;
    readonly cardTypeIdList: ReadonlyArray<guid>;
    readonly isVirtual: boolean;
    seal<T = CardMetadataSectionSealed>(): T;
    getColumnById(columnId: guid): CardMetadataColumnSealed | undefined;
    getColumnByName(columnName: string): CardMetadataColumnSealed | undefined;
    getPhysicalReferenceNames(): ReadonlyArray<string>;
    getPhysicalColumns(complexColumn: CardMetadataColumn | CardMetadataColumnSealed): ReadonlyArray<CardMetadataColumnSealed>;
}
/**
 * Содержит метаинформацию о секции.
 */
export declare class CardMetadataSection extends CardSerializableObject {
    constructor();
    private _columns;
    private _cardTypeIdList;
    private _physicalReferenceNames;
    id: guid | null;
    name: string | null;
    sectionType: CardSectionType;
    tableType: SchemeTableContentType;
    get cardTypeIdList(): guid[];
    set cardTypeIdList(value: guid[]);
    get sectionTableType(): CardTableType;
    set sectionTableType(value: CardTableType);
    order: number;
    flags: CardMetadataSectionFlags;
    get columns(): CardMetadataColumn[];
    set columns(value: CardMetadataColumn[]);
    get isVirtual(): boolean;
    set isVirtual(value: boolean);
    seal<T = CardMetadataSectionSealed>(): T;
    getColumnById(columnId: guid): CardMetadataColumn | undefined;
    getColumnByName(columnName: string): CardMetadataColumn | undefined;
    getPhysicalReferenceNames(): ReadonlyArray<string>;
    private getPhysicalReferenceNamesWithoutCache;
    getPhysicalColumns(complexColumn: CardMetadataColumn): CardMetadataColumn[];
    static createFrom(section: CardMetadataSection, columns: CardMetadataColumn[]): CardMetadataSection;
}
