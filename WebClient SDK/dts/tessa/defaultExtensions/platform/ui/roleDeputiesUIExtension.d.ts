import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class RoleDeputiesUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): void;
    private attachHandlersToRoleDeputies;
    private attachHandlersToRow;
    private rowInvokedHandler;
    private rowValidatingHandler;
    private static switchIsPermanent;
}
