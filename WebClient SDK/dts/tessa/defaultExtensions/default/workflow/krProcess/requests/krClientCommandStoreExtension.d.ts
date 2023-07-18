import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
export declare class KrClientCommandStoreExtension extends CardStoreExtension {
    afterRequest(context: ICardStoreExtensionContext): Promise<void>;
}
