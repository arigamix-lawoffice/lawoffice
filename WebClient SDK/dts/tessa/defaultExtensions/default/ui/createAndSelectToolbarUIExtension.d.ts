import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class CreateAndSelectToolbarUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static tryGetParentSelectionViewContext;
    private saveCardAndSelectCommand;
}
