import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrVirtualFilesUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    private rowStateChanged;
    private compile;
}
