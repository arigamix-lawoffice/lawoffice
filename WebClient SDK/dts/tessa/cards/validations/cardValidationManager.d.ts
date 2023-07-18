import { CardValidationContext } from './cardValidationContext';
import { CardValidationMode } from './cardValidationMode';
import { CardValidationResult } from './cardValidationResult';
import { CardTypeValidatorSealed } from '../types';
import { Card } from '../card';
import { CardStoreMode } from '../cardStoreMode';
import { IStorage } from 'tessa/platform/storage';
export declare class CardValidationManager {
    private constructor();
    private static _instance;
    static get instance(): CardValidationManager;
    private _cardMetadata;
    private _registry;
    private _result;
    get result(): CardValidationResult;
    validateCard(validators: ReadonlyArray<CardTypeValidatorSealed>, mainCardTypeId: guid, mainCard: Card, storeMode: CardStoreMode, externalContextInfo?: IStorage | null, modifyContextAction?: ((context: CardValidationContext) => void) | null, validationMode?: CardValidationMode): CardValidationResult;
    validateTask(validators: ReadonlyArray<CardTypeValidatorSealed>, mainCardTypeId: guid, mainCard: Card, storeMode: CardStoreMode, taskCardTypeId: guid, taskCard: Card, externalContextInfo?: IStorage | null, modifyContextAction?: ((context: CardValidationContext) => void) | null, validationMode?: CardValidationMode): CardValidationResult;
    private validateInternal;
}
