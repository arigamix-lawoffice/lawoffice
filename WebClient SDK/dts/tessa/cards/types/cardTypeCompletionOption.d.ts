import { CardTypeCompletionOptionFlags } from './cardTypeCompletionOptionFlags';
import { CardTypeValidator, CardTypeValidatorSealed } from './cardTypeValidator';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardTypeCompletionOptionSealed {
    readonly typeId: guid | null;
    readonly formName: string | null;
    readonly flags: CardTypeCompletionOptionFlags;
    readonly validators: ReadonlyArray<CardTypeValidatorSealed>;
    readonly functionRoleIDs: guid[] | null;
    seal<T = CardTypeCompletionOptionSealed>(): T;
}
/**
 * Вариант завершения типа карточки задания.
 */
export declare class CardTypeCompletionOption extends CardSerializableObject {
    constructor();
    private _validators;
    /**
     * Идентификатор варианта завершения.
     */
    typeId: guid | null;
    /**
     * Имя формы, которая выводится для варианта завершения, или null, если выводится форма,
     * определённая для типа карточки.
     */
    formName: string | null;
    /**
     * Флаги варианта завершения.
     */
    flags: CardTypeCompletionOptionFlags;
    /**
     * Функциональные роли варианта завершения.
     */
    functionRoleIDs: guid[] | null;
    /**
     * Список валидаторов, используемых для варианта завершения.
     */
    get validators(): CardTypeValidator[];
    set validators(value: CardTypeValidator[]);
    seal<T = CardTypeCompletionOptionSealed>(): T;
}
