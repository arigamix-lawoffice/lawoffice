import { CardInstanceType } from './cardInstanceType';
import { CardValidationMode } from './validations/cardValidationMode';
/**
 * Тип валидатора, используемый в объектах метаинформации по типу карточки
 * для связи с пользовательским интерфейсом редактирования карточки и с валидацией карточки.
 */
export declare class CardValidatorType {
    constructor(id: guid, name: string, allowedValidationModes?: CardValidationMode[], allowedInstanceTypes?: CardInstanceType[]);
    private _allowedValidationModes;
    private _allowedInstanceTypes;
    /**
     * ID объекта.
     */
    readonly id: guid;
    /**
     * Наименование объекта.
     */
    readonly name: string;
    isAllowedInstanceType(type: CardInstanceType): boolean;
    isAllowedValidationMode(mode: CardValidationMode): boolean;
}
