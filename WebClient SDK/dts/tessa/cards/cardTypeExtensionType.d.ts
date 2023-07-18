import { CardInstanceType } from './cardInstanceType';
/**
 * Тип расширения для типа карточки, используемый в объектах метаинформации по типу карточки CardType
 * для связи с пользовательским интерфейсом редактирования карточки и с выполнением расширений на типы карточек.
 */
export declare class CardTypeExtensionType {
    constructor(id: guid, name: string, allowedInstanceTypes?: CardInstanceType[]);
    private _allowedInstanceTypes;
    /**
     * ID объекта.
     */
    readonly id: guid;
    /**
     * Наименование объекта.
     */
    readonly name: string;
    isAllowed(type: CardInstanceType): boolean;
}
