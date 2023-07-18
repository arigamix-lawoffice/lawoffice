import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataRecordSealed {
    readonly values: ReadonlyArray<any>;
    seal<T = CardMetadataRecordSealed>(): T;
}
/**
 * Содержит метаинформацию о строке перечисления.
 */
export declare class CardMetadataRecord extends CardSerializableObject {
    constructor();
    private _values;
    get values(): any[];
    set values(value: any[]);
    seal<T = CardMetadataRecordSealed>(): T;
}
