import { WorkplaceFilteringRule, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext, CheckingResult } from 'tessa/views/workplaces';
/**
 * Правило фильтрации элементов рабочего места исключающее из метаданных рабочего места
 * элементы WorkplaceUnusedComponentsMetadata
 */
export declare class WorkplaceUnusedComponentsRemovingRule extends WorkplaceFilteringRule {
    evaluate(metadata: WorkplaceMetadataComponentSealed, _context: WorkplaceFilteringContext): CheckingResult;
}
