import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class SettingsDigestCardUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
}
