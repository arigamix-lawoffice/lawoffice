import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceFilteringRule, CheckingResult, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext } from 'tessa/views/workplaces';
export declare class RefSectionExtension extends TreeItemExtension implements IWorkplaceFilteringRule {
    getExtensionName(): string;
    evaluate(_metadata: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext): CheckingResult;
    private static parametersEquals;
}
