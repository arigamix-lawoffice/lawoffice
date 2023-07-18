import { WorkplaceFilteringRule, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext, CheckingResult } from 'tessa/views/workplaces';
/**
 * Правило фильтрации элементов рабочего места исключающее из метаданных
 * элементы IDataNodeMetadata в которых параметры не соответствуют контексту фильтрации
 */
export declare class DataNodesByRefSectionRemovingRule extends WorkplaceFilteringRule {
    evaluate(metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
    private static conditionEquals;
    private static parametersEquals;
}
