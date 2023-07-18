import { Card } from './card';
import { CardFile } from './cardFile';
import { CardTask } from './cardTask';
import { CardTypeSealed, TypeSettingsSealed, CardTypeExtensionSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IStorage } from 'tessa/platform/storage';
import { IValidationResultBuilder } from 'tessa/platform/validation';
export declare class TypeExtensionContext {
    constructor(mainCard: Card, mainCardType: CardTypeSealed, cardMetadata: CardMetadataSealed, externalContext?: any | null, info?: IStorage | null);
    readonly mainCard: Card;
    readonly mainCardType: CardTypeSealed;
    cardType: CardTypeSealed;
    card: Card;
    cardFile: CardFile | null;
    cardTask: CardTask | null;
    extension: CardTypeExtensionSealed;
    get settings(): TypeSettingsSealed | null;
    readonly cardMetadata: CardMetadataSealed;
    readonly validationResult: IValidationResultBuilder;
    readonly externalContext: any | null;
    readonly info: IStorage | null;
}
