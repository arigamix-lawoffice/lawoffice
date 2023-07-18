import { WorkplaceFilteringRule, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext, CheckingResult } from 'tessa/views/workplaces';
/**
 * Правило фильтрации использующее фильтр заданный у конкретного узла дерева
 */
export declare class CustomWorkplaceFilteringRuleExecutor extends WorkplaceFilteringRule {
    evaluate(metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
    private getCustomFilteringRules;
}
