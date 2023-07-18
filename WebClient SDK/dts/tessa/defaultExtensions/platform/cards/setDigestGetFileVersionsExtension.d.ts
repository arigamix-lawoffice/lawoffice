import { CardGetFileVersionsExtension, ICardGetFileVersionsExtensionContext } from 'tessa/cards/extensions';
export declare class SetDigestGetFileVersionsExtension extends CardGetFileVersionsExtension {
    beforeRequest(context: ICardGetFileVersionsExtensionContext): Promise<void>;
}
