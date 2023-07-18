import { TypeSettings, TypeSettingsSealed } from './cardTypeCommon';
import { CardTypeExtensionType } from '../cardTypeExtensionType';
import { CardSerializableObject } from 'tessa/cards/cardSerializableObject';
export interface CardTypeExtensionSealed {
    readonly type: CardTypeExtensionType | null;
    readonly extensionSettings: TypeSettingsSealed;
    seal<T = CardTypeExtensionSealed>(): T;
}
/**
 * Информация о расширении, используемом в типе карточки.
 */
export declare class CardTypeExtension extends CardSerializableObject {
    private _extensionSettings;
    type: CardTypeExtensionType | null;
    get extensionSettings(): TypeSettings;
    set extensionSettings(value: TypeSettings);
    seal<T = CardTypeExtensionSealed>(): T;
}
