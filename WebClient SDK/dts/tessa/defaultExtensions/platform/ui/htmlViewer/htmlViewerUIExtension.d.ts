import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { TypeExtensionContext } from 'tessa/cards';
export declare class HtmlViewerUIExtension extends CardUIExtension {
    initializing(context: ICardUIExtensionContext): Promise<void>;
    executeInitializingAction: (typeContext: TypeExtensionContext) => Promise<void>;
}
