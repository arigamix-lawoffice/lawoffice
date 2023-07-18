import { CardMetadataEnumerationColumn, CardMetadataEnumerationColumnSealed } from './cardMetadataEnumerationColumn';
import { CardMetadataRecord, CardMetadataRecordSealed } from './cardMetadataRecord';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataEnumerationSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly columns: ReadonlyArray<CardMetadataEnumerationColumnSealed>;
    readonly records: ReadonlyArray<CardMetadataRecordSealed>;
    seal<T = CardMetadataEnumerationSealed>(): T;
}
/**
 * Содержит метаинформацию о перечислении.
 */
export declare class CardMetadataEnumeration extends CardSerializableObject {
    constructor();
    private _columns;
    private _records;
    id: guid | null;
    name: string | null;
    get columns(): CardMetadataEnumerationColumn[];
    set columns(value: CardMetadataEnumerationColumn[]);
    get records(): CardMetadataRecord[];
    set records(value: CardMetadataRecord[]);
    seal<T = CardMetadataEnumerationColumnSealed>(): T;
}
