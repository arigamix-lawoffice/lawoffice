import { WorkplaceFilteringRule, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext, CheckingResult } from 'tessa/views/workplaces';
/**
 * Правило фильтрации рабочего места исключающее из метаданных пустые папки
 */
export declare class EmptyFoldersRemovingRule extends WorkplaceFilteringRule {
    evaluate(metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
}
