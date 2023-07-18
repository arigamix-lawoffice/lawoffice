import { CardFileExtensionContext, ICardFileExtensionContext } from './cardFileExtensionContext';
import { CardGetFileContentRequest, CardGetFileContentResponse } from 'tessa/cards/service';
import { CardTypeSealed } from 'tessa/cards/types';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { IUserSession } from 'common/utility/userSession';
import { CardGetFileContentMethod } from 'tessa/cards';
export interface ICardGetFileContentExtensionContext extends ICardFileExtensionContext {
    readonly request: CardGetFileContentRequest;
    response: CardGetFileContentResponse | null;
    readonly method: CardGetFileContentMethod;
    readonly openInNewTab: boolean;
}
export declare class CardGetFileContentExtensionContext extends CardFileExtensionContext implements ICardGetFileContentExtensionContext {
    constructor(request: CardGetFileContentRequest, method: CardGetFileContentMethod, cardType: CardTypeSealed | null, cardTypeName: string | null, fileType: CardTypeSealed | null, fileTypeName: string | null, cardMetadata: CardMetadataSealed, session: IUserSession, openInNewTab: boolean);
    readonly request: CardGetFileContentRequest;
    response: CardGetFileContentResponse | null;
    readonly method: CardGetFileContentMethod;
    readonly openInNewTab: boolean;
}
