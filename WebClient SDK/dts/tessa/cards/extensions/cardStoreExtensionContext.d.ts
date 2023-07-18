import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardStoreMethod } from 'tessa/cards';
import { CardStoreRequest, CardStoreResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardStoreExtensionContext extends ICardExtensionContext {
    readonly request: CardStoreRequest;
    response: CardStoreResponse | null;
    readonly method: CardStoreMethod;
}
export declare class CardStoreExtensionContext extends CardExtensionContext implements ICardStoreExtensionContext {
    constructor(request: CardStoreRequest, method: CardStoreMethod, cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardStoreRequest;
    response: CardStoreResponse | null;
    readonly method: CardStoreMethod;
}
