import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class CardWindowResizeUIExtension extends CardUIExtension {
    private _disposes;
    initialized(context: ICardUIExtensionContext): void;
    private updateWindowSize;
    finalized(): void;
}
