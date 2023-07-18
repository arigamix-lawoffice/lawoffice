import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardTypeSchemeItemSealed {
    readonly sectionId: guid | null;
    readonly columnIdList: ReadonlyArray<guid>;
    seal<T = CardTypeSchemeItemSealed>(): T;
}
/**
 * Метаданные секции, которая входит в состав типа карточки.
 */
export declare class CardTypeSchemeItem extends CardSerializableObject {
    constructor();
    private _columnIdList;
    /**
     * Идентификатор секции, в которой содержится колонки с заданным идентификаторами columnIdList,
     * или null, если поле содержится в физической колонке или составлено из нескольких
     * физических колонок.
     */
    sectionId: guid | null;
    /**
     * Список идентификаторов физических и комплексных колонок секции sectionId,
     * которые определяют, какие колонки будут загружаться для типа карточки.
     */
    get columnIdList(): guid[];
    set columnIdList(value: guid[]);
    seal<T = CardTypeSchemeItemSealed>(): T;
}
