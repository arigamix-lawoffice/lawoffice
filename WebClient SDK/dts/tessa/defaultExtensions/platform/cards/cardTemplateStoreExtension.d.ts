import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
export declare class CardTemplateStoreExtension extends CardStoreExtension {
    beforeRequest(context: ICardStoreExtensionContext): Promise<void>;
}
