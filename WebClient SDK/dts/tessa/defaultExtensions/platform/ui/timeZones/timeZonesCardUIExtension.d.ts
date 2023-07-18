import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class TimeZonesCardUIExtension extends CardUIExtension {
    private _gridDisposers;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
}
