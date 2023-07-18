import { CardControlType } from './cardControlType';
import { Registry, IRegistry } from 'tessa/platform/registry';
/**
 * Реестр типов элементов управления CardControlType.
 * Класс является синглтоном.
 */
export declare class CardControlTypeRegistry extends Registry<CardControlType> {
    private constructor();
    private static _instance;
    static get instance(): IRegistry<CardControlType>;
}
