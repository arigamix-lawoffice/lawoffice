import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class CardToolbarUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static cardLinkEnabled;
    private static deleteEnabled;
    private saveCardCloseAndCreateCommand;
    private saveCardAndCloseCommand;
    private saveCardCommand;
    private refreshCardCommand;
    private closeCommand;
    private cardStructureCommand;
    private copyCardLinkCommand;
    private deleteCardCommand;
}
