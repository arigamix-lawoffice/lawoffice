import { CheckingResult } from './checkingResult';
import { WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
import { WorkplaceFilteringContext } from './workplaceFilteringContext';
import { IExtension } from 'tessa/extensions';
export interface IWorkplaceFilteringRule extends IExtension {
    evaluate(metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
}
export declare abstract class WorkplaceFilteringRule implements IWorkplaceFilteringRule {
    static readonly type = "WorkplaceFilteringRule";
    shouldExecute(): boolean;
    evaluate(_metadata: WorkplaceMetadataComponentSealed, _context: WorkplaceFilteringContext): CheckingResult;
}
