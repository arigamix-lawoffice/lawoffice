import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardNewMethod } from 'tessa/cards';
import { CardNewRequest, CardNewResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardNewExtensionContext extends ICardExtensionContext {
    readonly request: CardNewRequest;
    response: CardNewResponse | null;
    readonly method: CardNewMethod;
}
export declare class CardNewExtensionContext extends CardExtensionContext implements ICardNewExtensionContext {
    constructor(request: CardNewRequest, method: CardNewMethod, cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardNewRequest;
    response: CardNewResponse | null;
    readonly method: CardNewMethod;
}
