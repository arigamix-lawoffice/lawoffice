import { IStorage } from 'tessa/platform/storage';
import { CardNewRequest, CardNewResponse } from 'tessa/cards/service';
import { ICloneable } from 'tessa/platform';
import { CardResponseBase } from './cardResponseBase';
/**
 * Ответ на запрос CardCreateFromTemplateRequest.
 */
export declare class CardCreateFromTemplateResponse extends CardResponseBase implements ICloneable<CardCreateFromTemplateResponse> {
    /**
     * Инициализирует новый экземпляр класса {@link CardCreateFromTemplateResponse}.
     * @param {IStorage} storage Хранилище, декоратором для которого является создаваемый объект.
     */
    constructor(storage?: IStorage);
    static readonly requestKey: string;
    static readonly responseKey: string;
    /**
     * Внутренний запрос на создание карточки по шаблону.
     *
     * Может иметь значение null, если его не удалось создать.
     */
    get request(): CardNewRequest | null;
    set request(value: CardNewRequest | null);
    /**
     * Ответ на запрос на создание карточки по шаблону.
     */
    get response(): CardNewResponse;
    set response(value: CardNewResponse);
    tryGetResponse(): CardNewResponse | null;
    clean(): void;
    clone(): CardCreateFromTemplateResponse;
}
