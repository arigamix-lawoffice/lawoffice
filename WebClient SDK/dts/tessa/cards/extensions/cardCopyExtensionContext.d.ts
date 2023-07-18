import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardCopyRequest, CardNewResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
import { CardCopyResponse } from '../service/cardCopyResponse';
export interface ICardCopyExtensionContext extends ICardExtensionContext {
    readonly request: CardCopyRequest;
    response: CardNewResponse | null;
}
export declare class CardCopyExtensionContext extends CardExtensionContext implements ICardCopyExtensionContext {
    constructor(request: CardCopyRequest, cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardCopyRequest;
    response: CardCopyResponse | null;
}
