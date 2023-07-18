import { CardExtensionContext, ICardExtensionContext } from './cardExtensionContext';
import { CardCreateFromTemplateRequest, CardNewResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardCreateFromTemplateExtensionContext extends ICardExtensionContext {
    readonly request: CardCreateFromTemplateRequest;
    response: CardNewResponse | null;
}
export declare class CardCreateFromTemplateExtensionContext extends CardExtensionContext implements ICardCreateFromTemplateExtensionContext {
    constructor(request: CardCreateFromTemplateRequest, cardType: CardTypeSealed | null, cardTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardCreateFromTemplateRequest;
    response: CardNewResponse | null;
}
