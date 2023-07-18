import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardMetadataCompletionOptionSealed {
    readonly id: guid | null;
    readonly name: string | null;
    readonly caption: string | null;
    seal<T = CardMetadataCompletionOptionSealed>(): T;
}
/**
 * Содержит информацию о варианте завершения заданий.
 */
export declare class CardMetadataCompletionOption extends CardSerializableObject {
    constructor();
    id: guid | null;
    name: string | null;
    caption: string | null;
    seal<T = CardMetadataCompletionOptionSealed>(): T;
}
