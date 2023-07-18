import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrHideCardTypeSettingsUIExtension extends CardUIExtension {
    private static _typesBlock;
    private static _typesControl;
    private static _typeSettingBlock;
    private static _useDocTypesField;
    private static _useDocTypesControl;
    private static _useApprovingField;
    private static _useApprovingControl;
    private static _useApprovingBlock;
    private static _useAutoApprovingField;
    private static _useAutoApprovingControl;
    private static _autoApprovalSettingsBlock1;
    private static _autoApprovalSettingsBlock2;
    private static _useRegistrationBlock;
    private static _useRegistrationControl;
    private static _useRegistrationField;
    private static _registrationSettingsBlock;
    private _disposes;
    private _gridDisposes;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private gridRowInvoked;
    private gridRowEditorClosed;
    private collapseIfDocTypesUnused;
    private collapseSettingsIfDocTypesInUse;
    private collapseIfUncheckedInRow;
    private collapseIfUnchecked;
}
