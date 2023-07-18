import { IStorage } from 'tessa/platform/storage';
import { SchemeType } from 'tessa/scheme';
import { CardSerializableObject } from '../cardSerializableObject';
import { CardMetadataColumnType } from '../metadata/cardMetadataColumnType';
export interface CardTypeSectionColumnSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly columnType: CardMetadataColumnType;
    readonly schemeType: SchemeType | null;
    readonly defaultValue: any;
    readonly referencedSectionName: string | null;
    readonly isReferencedToOwner: boolean | null;
    readonly isReference: boolean | null;
    readonly referencedColumns: ReadonlyArray<CardTypeSectionColumnSealed>;
    readonly description: string | null;
    readonly info: IStorage | null;
    seal<T = CardTypeSectionColumnSealed>(): T;
}
export declare class CardTypeSectionColumn extends CardSerializableObject {
    constructor();
    private _referencedColumns;
    id: guid | null;
    name: string | null;
    columnType: CardMetadataColumnType;
    schemeType: SchemeType | null;
    defaultValue: any;
    referencedSectionName: string | null;
    isReferencedToOwner: boolean | null;
    isReference: boolean | null;
    get referencedColumns(): CardTypeSectionColumn[];
    set referencedColumns(value: CardTypeSectionColumn[]);
    description: string | null;
    info: IStorage | null;
    seal<T = CardTypeSectionColumn>(): T;
}
