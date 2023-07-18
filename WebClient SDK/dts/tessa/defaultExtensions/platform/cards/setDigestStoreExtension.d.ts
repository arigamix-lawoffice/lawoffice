import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
export declare class SetDigestStoreExtension extends CardStoreExtension {
    beforeRequest(context: ICardStoreExtensionContext): Promise<void>;
}
