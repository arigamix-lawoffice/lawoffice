import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class CalendarUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static attachCommandToButton;
    private static validateCalendarButtonAction;
    private static rebuildCalendarButtonAction;
    private openCalendarTypeInDialogAsync;
}
