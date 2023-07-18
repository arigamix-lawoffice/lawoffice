import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class FunctionRolesUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    private static readonly HideTaskByDefaultControlAlias;
    private static readonly CanTakeInProgressFieldName;
    private static readonly HideTaskByDefaultFieldName;
    initialized(context: ICardUIExtensionContext): void;
}
