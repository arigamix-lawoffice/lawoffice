import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardDeleteMethod } from 'tessa/cards';
import { CardDeleteRequest, CardDeleteResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardDeleteExtensionContext extends ICardExtensionContext {
    readonly request: CardDeleteRequest;
    response: CardDeleteResponse | null;
    readonly method: CardDeleteMethod;
}
export declare class CardDeleteExtensionContext extends CardExtensionContext implements ICardDeleteExtensionContext {
    constructor(request: CardDeleteRequest, method: CardDeleteMethod, cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardDeleteRequest;
    response: CardDeleteResponse | null;
    readonly method: CardDeleteMethod;
}
