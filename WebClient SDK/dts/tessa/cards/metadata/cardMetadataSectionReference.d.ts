import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataSectionReferenceSealed {
    readonly id: guid | null;
    readonly name: string | null;
    seal<T = CardMetadataSectionReferenceSealed>(): T;
}
/**
 * Содержит ссылку на секцию в метаинформации.
 */
export declare class CardMetadataSectionReference extends CardSerializableObject {
    constructor();
    id: guid | null;
    name: string | null;
    seal<T = CardMetadataSectionReferenceSealed>(): T;
}
