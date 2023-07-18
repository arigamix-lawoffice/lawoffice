import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class HelpSectionUIExtension extends CardUIExtension {
    readonly _dispose: Array<(() => void) | null>;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
}
