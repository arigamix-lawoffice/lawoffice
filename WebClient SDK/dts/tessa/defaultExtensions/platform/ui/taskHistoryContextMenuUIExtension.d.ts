import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class TaskHistoryContextMenuUIExtension extends CardUIExtension {
    private _dispose;
    initialized(context: ICardUIExtensionContext): void;
    finalize(): void;
    private copyToClipboard;
}
