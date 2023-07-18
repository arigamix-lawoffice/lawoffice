import { WorkplaceFilteringRule, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext, CheckingResult } from 'tessa/views/workplaces';
/**
 * Правило фильтрации элементов рабочего места основанное на IItemShowMode.ShowMode элемента
 */
export declare class ByItemsShowRemovingRule extends WorkplaceFilteringRule {
    evaluate(metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
}
