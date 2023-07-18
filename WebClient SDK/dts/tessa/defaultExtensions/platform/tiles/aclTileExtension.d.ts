import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
export declare class AclTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
    private initializeCardTilesAsync;
    private initializeGenerationRuleTilesAsync;
    private static updateCardAclEvaluating;
    private static updateRuleAclEvaluating;
    private static updateCardAclAsync;
    private static updateRuleAclAsync;
    private static showAclAsync;
}
