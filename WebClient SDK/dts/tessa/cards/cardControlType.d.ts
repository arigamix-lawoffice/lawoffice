import { CardControlTypeFlags } from './cardControlTypeFlags';
import { CardControlTypeUsageMode } from './cardControlTypeUsageMode';
/**
 * Тип элемента управления, используемый в объектах метаинформации по типу карточки CardType
 * для связи с пользовательским интерфейсом редактирования карточки.
 */
export declare class CardControlType {
    constructor(id: guid, name: string, usageMode: CardControlTypeUsageMode, flags: CardControlTypeFlags);
    /**
     * ID объекта.
     */
    readonly id: guid;
    /**
     * Наименование объекта.
     */
    readonly name: string;
    /**
     * Способ использования элемента управления.
     */
    readonly usageMode: CardControlTypeUsageMode;
    /**
     * Флаги элемента управления, описывающие его поведение.
     */
    readonly flags: CardControlTypeFlags;
}
