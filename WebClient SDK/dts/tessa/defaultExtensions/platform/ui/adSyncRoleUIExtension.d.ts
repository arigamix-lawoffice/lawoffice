import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class AdSyncRoleUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    private attachCommandToButton;
    private adSyncManualSyncButtonAction;
    private getOperationType;
}
