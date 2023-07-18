import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { TypeExtensionContext } from 'tessa/cards';
export declare class MakeViewTaskHistoryUIExtension extends CardUIExtension {
    private _disposes;
    initializing(context: ICardUIExtensionContext): Promise<void>;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    executeInitializingAction: (typeContext: TypeExtensionContext) => Promise<void>;
    executeInitializedAction: (typeContext: TypeExtensionContext) => Promise<void>;
    private getTaskHistoryItemInfo;
    private copyToClipboard;
    private createGroupMenu;
    private toggleRow;
}
