import { WorkplaceMetadataComponent, IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
import { IWorkplaceMetadataVisitor } from './workplaceMetadataVisitor';
import { ExpandingMode } from './expandingMode';
export interface IWorkplaceExpandingMetadata {
    expandingMode: ExpandingMode;
}
export interface WorkplaceExpandingMetadataSealed {
    readonly expandingMode: ExpandingMode;
}
export interface IWorkplaceCompositeMetadata extends IWorkplaceComponentMetadata {
    items: Array<IWorkplaceComponentMetadata>;
    addMetadata(metadata: IWorkplaceComponentMetadata): any;
    removeMetadata(metadata: IWorkplaceComponentMetadata): any;
    seal<T = WorkplaceCompositeMetadataSealed>(): T;
}
export interface WorkplaceCompositeMetadataSealed extends WorkplaceMetadataComponentSealed {
    readonly items: ReadonlyArray<WorkplaceMetadataComponentSealed>;
    seal<T = WorkplaceCompositeMetadataSealed>(): T;
}
export declare class WorkplaceCompositeMetadata extends WorkplaceMetadataComponent implements IWorkplaceCompositeMetadata {
    constructor();
    items: Array<IWorkplaceComponentMetadata>;
    addMetadata(metadata: IWorkplaceComponentMetadata): void;
    removeMetadata(metadata: IWorkplaceComponentMetadata): void;
    visit(visitor: IWorkplaceMetadataVisitor, reverse?: boolean): void;
    seal<T = WorkplaceCompositeMetadataSealed>(): T;
}
