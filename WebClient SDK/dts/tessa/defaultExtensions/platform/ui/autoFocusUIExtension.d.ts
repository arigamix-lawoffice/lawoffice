import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class AutoFocusUIExtension extends CardUIExtension {
    contextInitialized(context: ICardUIExtensionContext): void;
    private static tryFindControlToFocus;
}
