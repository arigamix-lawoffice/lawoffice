import { CardTypeValidatorSealed } from 'tessa/cards/types';
import { Card, CardStoreMode } from 'tessa/cards';
import { IStorage } from 'tessa/platform/storage';
import { CardValidationContext, CardValidationMode, CardValidationResult } from 'tessa/cards/validations';
export declare class KrCardValidationManager {
    private constructor();
    private static _instance;
    static get instance(): KrCardValidationManager;
    validateCard(validators: ReadonlyArray<CardTypeValidatorSealed>, mainCardTypeId: guid, mainCard: Card, storeMode: CardStoreMode, externalContextInfo?: IStorage | null, modifyContextAction?: ((context: CardValidationContext) => void) | null, validationMode?: CardValidationMode): CardValidationResult;
    validateTask(validators: ReadonlyArray<CardTypeValidatorSealed>, mainCardTypeId: guid, mainCard: Card, storeMode: CardStoreMode, taskCardTypeId: guid, taskCard: Card, externalContextInfo?: IStorage | null, modifyContextAction?: ((context: CardValidationContext) => void) | null, validationMode?: CardValidationMode): CardValidationResult;
    private getModifyContextAction;
    private modifyContext;
}
