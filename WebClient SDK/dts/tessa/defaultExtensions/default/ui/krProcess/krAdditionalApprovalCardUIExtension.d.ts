import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrAdditionalApprovalCardUIExtension extends CardUIExtension {
    private _disposes;
    private _card;
    private _handleManager;
    private _lastSelectedItem;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private isCardAvailableForExtension;
    private approvalStagesTable_RowInvoked;
    private transferData;
    private static markName;
    private static unmarkName;
}
