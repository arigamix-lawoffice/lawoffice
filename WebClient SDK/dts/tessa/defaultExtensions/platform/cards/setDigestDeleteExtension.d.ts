import { CardDeleteExtension, ICardDeleteExtensionContext } from 'tessa/cards/extensions';
export declare class SetDigestDeleteExtension extends CardDeleteExtension {
    beforeRequest(context: ICardDeleteExtensionContext): Promise<void>;
}
