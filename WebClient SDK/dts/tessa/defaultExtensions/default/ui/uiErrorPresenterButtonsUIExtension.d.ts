import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class UIErrorPresenterButtonsUIExtension extends CardUIExtension {
    private _disposer;
    initializing(context: ICardUIExtensionContext): void;
    finalized(): void;
    private addRefreshButton;
}
