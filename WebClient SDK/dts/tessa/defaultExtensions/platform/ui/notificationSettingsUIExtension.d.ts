import { MySettingsExtension, IMySettingsExtensionContext } from 'tessa/ui/cards/mySettings';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class NotificationSettingsUIExtension extends MySettingsExtension {
    private _extension;
    initialized(context: IMySettingsExtensionContext): void;
    finalized(): void;
}
export declare class NotificationSettingsCardUIExtension extends CardUIExtension {
    private _extension;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
}
