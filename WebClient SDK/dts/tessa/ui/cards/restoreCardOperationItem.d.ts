import { CardOperationItem } from './cardOperationItem';
/**
 * Элемент операции, описывающий восстанавливаемую карточку для операции <see cref="RestoreCardOperation"/>.
 */
export declare class RestoreCardOperationItem extends CardOperationItem {
    /**
     * Создаёт экземпляр класса с указанием идентификатора карточки и отображаемого значения.
     * @param cardId Идентификатор карточки.
     * @param displayValue Отображаемое имя карточки.
     */
    constructor(cardId: guid, displayValue: string);
}
