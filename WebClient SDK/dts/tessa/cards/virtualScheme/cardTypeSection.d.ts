import { CardTypeSectionColumn, CardTypeSectionColumnSealed } from './cardTypeSectionColumn';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
import { SchemeTableContentType } from 'tessa/scheme';
import { IStorage } from 'tessa/platform/storage';
export interface CardTypeSectionSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly description: string | null;
    readonly tableType: SchemeTableContentType;
    readonly columns: ReadonlyArray<CardTypeSectionColumnSealed>;
    readonly info: IStorage | null;
    seal<T = CardTypeSectionSealed>(): T;
}
export declare class CardTypeSection extends CardSerializableObject {
    constructor();
    private _columns;
    id: guid | null;
    name: string | null;
    description: string | null;
    tableType: SchemeTableContentType;
    get columns(): CardTypeSectionColumn[];
    set columns(value: CardTypeSectionColumn[]);
    info: IStorage | null;
    seal<T = CardTypeSectionSealed>(): T;
}
