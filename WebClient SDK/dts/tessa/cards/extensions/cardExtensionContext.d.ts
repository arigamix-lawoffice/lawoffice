import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
import { IStorage } from 'tessa/platform/storage';
import { IValidationResultBuilder } from 'tessa/platform/validation';
export interface ICardExtensionContext {
    requestIsSuccessful: boolean;
    readonly cardType: CardTypeSealed | null;
    readonly cardTypeName: string | null;
    readonly cardMetadata: CardMetadataSealed;
    readonly session: IUserSession;
    validationResult: IValidationResultBuilder;
    readonly info: IStorage;
}
export declare abstract class CardExtensionContext implements ICardExtensionContext {
    constructor(cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    requestIsSuccessful: boolean;
    cardType: CardTypeSealed | null;
    readonly cardTypeName: string | null;
    readonly cardMetadata: CardMetadataSealed;
    readonly session: IUserSession;
    validationResult: IValidationResultBuilder;
    readonly info: IStorage;
}
