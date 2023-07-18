import { CardMetadataType, CardMetadataTypeSealed } from './cardMetadataType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataEnumerationColumnSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly metadataType: CardMetadataTypeSealed | null;
    seal<T = CardMetadataEnumerationColumnSealed>(): T;
}
/**
 * Содержит метаинформацию о колонке перечисления.
 */
export declare class CardMetadataEnumerationColumn extends CardSerializableObject {
    constructor();
    id: guid | null;
    name: string | null;
    metadataType: CardMetadataType | null;
    seal<T = CardMetadataEnumerationColumnSealed>(): T;
}
