import { CardTypeControl, TypeSettings, TypeSettingsSealed } from './cardTypeCommon';
import { CardValidatorType } from '../cardValidatorType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardTypeValidatorSealed {
    readonly type: CardValidatorType | null;
    readonly validatorSettings: TypeSettingsSealed;
    seal<T = CardTypeValidatorSealed>(): T;
    appliesRequiredToControl(control: CardTypeControl): boolean;
}
/**
 * Информация о валидаторе, используемом в типе карточки.
 */
export declare class CardTypeValidator extends CardSerializableObject {
    private _validatorSettings;
    type: CardValidatorType | null;
    get validatorSettings(): TypeSettings;
    set validatorSettings(value: TypeSettings);
    seal<T = CardTypeValidatorSealed>(): T;
    appliesRequiredToControl(control: CardTypeControl): boolean;
}
