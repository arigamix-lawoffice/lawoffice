import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrSettingsForumsSettingsUIExtension extends CardUIExtension {
    private _cardRowsListener;
    private _disposers;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private attachHandlersToCardTypeRow;
}
