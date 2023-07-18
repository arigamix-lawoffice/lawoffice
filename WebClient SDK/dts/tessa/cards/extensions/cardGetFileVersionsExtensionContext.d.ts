import { CardFileExtensionContext, ICardFileExtensionContext } from './cardFileExtensionContext';
import { CardGetFileVersionsRequest, CardGetFileVersionsResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
export interface ICardGetFileVersionsExtensionContext extends ICardFileExtensionContext {
    readonly request: CardGetFileVersionsRequest;
    response: CardGetFileVersionsResponse | null;
}
export declare class CardGetFileVersionsExtensionContext extends CardFileExtensionContext implements ICardGetFileVersionsExtensionContext {
    constructor(request: CardGetFileVersionsRequest, cardType: CardTypeSealed | null, cardTypeName: string | null, fileType: CardTypeSealed | null, fileTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession);
    readonly request: CardGetFileVersionsRequest;
    response: CardGetFileVersionsResponse | null;
}
