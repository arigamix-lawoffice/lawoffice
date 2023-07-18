import { CardRequestExtension, ICardRequestExtensionContext } from 'tessa/cards/extensions';
export declare class DeskiInvalidateFileContentExtension extends CardRequestExtension {
    beforeRequest(context: ICardRequestExtensionContext): Promise<void>;
}
