import { CardValidatorType } from './cardValidatorType';
import { Registry, IRegistry } from 'tessa/platform/registry';
export declare class CardValidatorTypeRegistry extends Registry<CardValidatorType> {
    private constructor();
    private static _instance;
    static get instance(): IRegistry<CardValidatorType>;
}
