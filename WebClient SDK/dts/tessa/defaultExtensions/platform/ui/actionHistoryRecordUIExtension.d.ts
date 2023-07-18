import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class ActionHistoryRecordUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
}
