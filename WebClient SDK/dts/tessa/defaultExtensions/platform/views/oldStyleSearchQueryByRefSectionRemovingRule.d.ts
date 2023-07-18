import { WorkplaceFilteringRule, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext, CheckingResult } from 'tessa/views/workplaces';
/**
 * Правило фильтрации элементов рабочего места исключающее из метаданных
 * поисковые запросы которые не подпадают под параметры фильтрации
 */
export declare class OldStyleSearchQueryByRefSectionRemovingRule extends WorkplaceFilteringRule {
    evaluate(metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
}
