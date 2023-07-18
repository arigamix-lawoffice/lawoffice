import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardRequest, CardResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardRequestExtensionContext extends ICardExtensionContext {
    readonly request: CardRequest;
    response: CardResponse | null;
    readonly requestType: guid;
    readonly fileType: CardTypeSealed | null;
    readonly fileTypeName: string | null;
    readonly taskType: CardTypeSealed | null;
    readonly taskTypeName: string | null;
}
export declare class CardRequestExtensionContext extends CardExtensionContext implements ICardRequestExtensionContext {
    constructor(request: CardRequest, requestType: guid, cardType: CardTypeSealed | null, cardTypeName: string | null, fileType: CardTypeSealed | null, fileTypeName: string | null, taskType: CardTypeSealed | null, taskTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardRequest;
    response: CardResponse | null;
    readonly requestType: guid;
    readonly fileType: CardTypeSealed | null;
    readonly fileTypeName: string | null;
    readonly taskType: CardTypeSealed | null;
    readonly taskTypeName: string | null;
}
