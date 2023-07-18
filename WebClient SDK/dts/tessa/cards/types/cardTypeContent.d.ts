import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardTypeContentSealed {
    readonly caption: string | null;
    seal<T = CardTypeContentSealed>(): T;
}
/**
 * Базовый объект для CardTypeControl и CardTypeColumn.
 */
export declare abstract class CardTypeContent extends CardSerializableObject {
    constructor();
    /**
     * Отображаемое имя объекта.
     */
    caption: string | null;
    seal<T = CardTypeContentSealed>(): T;
}
