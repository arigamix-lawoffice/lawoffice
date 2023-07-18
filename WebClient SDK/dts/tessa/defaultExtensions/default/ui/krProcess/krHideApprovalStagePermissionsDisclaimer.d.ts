import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrHideApprovalStagePermissionsDisclaimer extends CardUIExtension {
    private _disposes;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private gridRowInvoked;
}
