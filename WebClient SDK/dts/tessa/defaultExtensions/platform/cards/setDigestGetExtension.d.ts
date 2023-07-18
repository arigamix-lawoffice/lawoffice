import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
export declare class SetDigestGetExtension extends CardGetExtension {
    beforeRequest(context: ICardGetExtensionContext): Promise<void>;
}
