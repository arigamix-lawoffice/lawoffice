import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrStageTemplateUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    private static hasSqlQuery;
    private static hasComputedRole;
    private static hasComputedRoleInRow;
}
