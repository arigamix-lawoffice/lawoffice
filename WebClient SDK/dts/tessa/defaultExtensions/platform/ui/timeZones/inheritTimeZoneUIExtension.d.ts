import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class InheritTimeZoneUIExtension extends CardUIExtension {
    private _disposer;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
}
