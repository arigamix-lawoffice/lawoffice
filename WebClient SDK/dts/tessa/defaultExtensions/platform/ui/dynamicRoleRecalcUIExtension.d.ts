import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class DynamicRoleRecalcUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
}
