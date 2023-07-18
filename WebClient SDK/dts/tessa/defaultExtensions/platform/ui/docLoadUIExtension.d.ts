import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class DocLoadUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
}
