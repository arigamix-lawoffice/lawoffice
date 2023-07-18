import { CardUIExtension } from 'tessa/ui/cards/cardUIExtension';
import { ICardUIExtensionContext } from 'tessa/ui/cards/cardUIExtensionContext';
export declare class TagDemoCardActionExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): Promise<void>;
    private addTagToToolbar;
    shouldExecute(_context: ICardUIExtensionContext): boolean;
}
