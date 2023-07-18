import { TypeExtensionContext } from 'tessa/cards';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrTokenToTaskHistoryViewUIExtension extends CardUIExtension {
    initializing(context: ICardUIExtensionContext): Promise<void>;
    executeInitializingAction: (typeContext: TypeExtensionContext) => Promise<void>;
}
