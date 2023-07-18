import { IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
export interface IWorkplaceMetadataVisitor {
    stopItemsIteration?: boolean;
    visitEnter(component: IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed): any;
    visitLeave(component: IWorkplaceComponentMetadata | WorkplaceMetadataComponentSealed): any;
}
