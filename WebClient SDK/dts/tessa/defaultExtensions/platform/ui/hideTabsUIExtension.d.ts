import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class HideTabsUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): Promise<void>;
    private executeAction;
}
