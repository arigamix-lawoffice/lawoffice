import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class CalendarTypeUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    private static readonly WeekDaysControlAlias;
    private static readonly WorkTimeBlockAlias;
    private static readonly LunchTimeBlockAlias;
    initialized(context: ICardUIExtensionContext): void;
}
