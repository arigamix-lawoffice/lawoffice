import { TypeExtensionContext } from 'tessa/cards';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class WfTaskHistoryViewUIExtension extends CardUIExtension {
    _disposes: Array<Function | null>;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    executeInitializedAction: (typeContext: TypeExtensionContext) => Promise<void>;
}
