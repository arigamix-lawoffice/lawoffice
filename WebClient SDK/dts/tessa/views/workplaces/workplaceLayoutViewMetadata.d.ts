import { WorkplaceMetadataComponent, IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from './workplaceMetadataComponent';
import { IWorkplaceMetadataVisitor } from './workplaceMetadataVisitor';
export interface IWorkplaceLayoutViewMetadata extends IWorkplaceComponentMetadata {
    seal<T = WorkplaceLayoutViewMetadataSealed>(): T;
}
export interface WorkplaceLayoutViewMetadataSealed extends WorkplaceMetadataComponentSealed {
    seal<T = WorkplaceLayoutViewMetadataSealed>(): T;
}
export declare class WorkplaceLayoutViewMetadata extends WorkplaceMetadataComponent implements IWorkplaceLayoutViewMetadata {
    constructor();
    visit(visitor: IWorkplaceMetadataVisitor, reverse?: boolean): void;
    seal<T = WorkplaceLayoutViewMetadataSealed>(): T;
}
