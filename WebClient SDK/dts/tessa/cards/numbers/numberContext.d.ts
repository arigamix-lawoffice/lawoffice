import { NumberLocation } from './numberLocation';
import { NumberEventTypes } from './numberEventTypes';
import { NumberObject } from './numberObject';
import { Card } from '../card';
import { CardTypeSealed } from '../types';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class NumberContext {
    constructor(card: Card, cardType: CardTypeSealed, numberObject?: NumberObject | null, numberLocation?: NumberLocation | null, contextInfo?: IStorage | null);
    eventType: NumberEventTypes;
    readonly card: Card;
    readonly cardType: CardTypeSealed;
    numberObject: NumberObject | null;
    numberLocation: NumberLocation | null;
    readonly validationResult: IValidationResultBuilder;
    readonly info: IStorage;
    readonly contextInfo: IStorage;
    setNumberAction(actionKey: string, numberAction: ((ctx: NumberContext, num: NumberObject) => Promise<void>) | null): void;
    executeNumberAction(actionKey: string, num: NumberObject): Promise<boolean>;
    storeNumber(numberObject: NumberObject): void;
}
