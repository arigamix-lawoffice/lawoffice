import { CardTypeExtensionType } from './cardTypeExtensionType';
import { Registry, IRegistry } from 'tessa/platform/registry';
export declare class CardTypeExtensionTypeRegistry extends Registry<CardTypeExtensionType> {
    private constructor();
    private static _instance;
    static get instance(): IRegistry<CardTypeExtensionType>;
}
