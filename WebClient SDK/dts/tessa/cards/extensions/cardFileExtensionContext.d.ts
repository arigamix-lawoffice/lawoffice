import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardFileExtensionContext extends ICardExtensionContext {
    fileType: CardTypeSealed | null;
    readonly fileTypeName: string | null;
}
export declare abstract class CardFileExtensionContext extends CardExtensionContext {
    constructor(cardType: CardTypeSealed | null, cardTypeName: string | null, fileType: CardTypeSealed | null, fileTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    fileType: CardTypeSealed | null;
    readonly fileTypeName: string | null;
}
