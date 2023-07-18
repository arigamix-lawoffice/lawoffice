import { IWorkplaceMetadataVisitor, WorkplaceMetadataComponentSealed, WorkplaceFilteringContext } from 'tessa/views/workplaces';
export declare class ApplyPolicyVisitor implements IWorkplaceMetadataVisitor {
    private _policy;
    private _tree;
    constructor(_policy: (workplace: WorkplaceMetadataComponentSealed) => boolean);
    visitEnter(component: WorkplaceMetadataComponentSealed): void;
    visitLeave(component: WorkplaceMetadataComponentSealed): void;
}
export declare class ApplyPolicyWithContextVisitor implements IWorkplaceMetadataVisitor {
    private _policy;
    private _filteringContext;
    private _tree;
    constructor(_policy: (workplace: WorkplaceMetadataComponentSealed, context: WorkplaceFilteringContext) => boolean, _filteringContext: WorkplaceFilteringContext);
    visitEnter(component: WorkplaceMetadataComponentSealed): void;
    visitLeave(component: WorkplaceMetadataComponentSealed): void;
}
export declare function workplaceItemsAccessibleInNormalMode(metadata: WorkplaceMetadataComponentSealed, userId: guid, isAdministrator: boolean): boolean;
