import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
export declare class KrPermissionsMandatoryStoreExtension extends CardStoreExtension {
    afterRequest(context: ICardStoreExtensionContext): void;
    private validateFromRules;
}
