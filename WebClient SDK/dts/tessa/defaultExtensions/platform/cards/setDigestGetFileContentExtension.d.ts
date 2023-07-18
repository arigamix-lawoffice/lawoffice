import { CardGetFileContentExtension, ICardGetFileContentExtensionContext } from 'tessa/cards/extensions';
export declare class SetDigestGetFileContentExtension extends CardGetFileContentExtension {
    beforeRequest(context: ICardGetFileContentExtensionContext): Promise<void>;
}
