import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardGetMethod } from 'tessa/cards';
import { CardGetRequest, CardGetResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardGetExtensionContext extends ICardExtensionContext {
    readonly request: CardGetRequest;
    response: CardGetResponse | null;
    readonly method: CardGetMethod;
}
export declare class CardGetExtensionContext extends CardExtensionContext implements ICardGetExtensionContext {
    constructor(request: CardGetRequest, method: CardGetMethod, cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardGetRequest;
    response: CardGetResponse | null;
    readonly method: CardGetMethod;
}
