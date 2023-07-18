import { IExtensionMetadata, ExtensionMetadataSealed } from './extensionMetadata';
import { IWorkplaceMetadataVisitor } from './workplaceMetadataVisitor';
import { NodeClientVisibility } from '../metadata';
export interface IWorkplaceComponentMetadata {
    _getType: string;
    alias: string;
    tabCaption: string;
    compositionId: guid;
    expandedIconName: string;
    extensions: Map<string, IExtensionMetadata>;
    iconName: string;
    nodeClientVisibility: NodeClientVisibility;
    orderPos: number;
    ownerId: guid;
    parentCompositionId: guid;
    seal<T = WorkplaceMetadataComponentSealed>(): T;
    visit(visitor: IWorkplaceMetadataVisitor, reverse?: boolean): any;
}
export interface WorkplaceMetadataComponentSealed {
    readonly _getType: string;
    readonly alias: string;
    readonly tabCaption: string;
    readonly compositionId: guid;
    readonly expandedIconName: string;
    readonly extensions: ReadonlyMap<string, ExtensionMetadataSealed>;
    readonly iconName: string;
    readonly nodeClientVisibility: NodeClientVisibility;
    readonly orderPos: number;
    readonly ownerId: guid;
    readonly parentCompositionId: guid;
    seal<T = WorkplaceMetadataComponentSealed>(): T;
    visit(visitor: IWorkplaceMetadataVisitor, _reverse?: boolean): any;
}
export declare class WorkplaceMetadataComponent implements IWorkplaceComponentMetadata {
    constructor();
    _getType: string;
    alias: string;
    tabCaption: string;
    compositionId: guid;
    expandedIconName: string;
    extensions: Map<string, IExtensionMetadata>;
    iconName: string;
    nodeClientVisibility: NodeClientVisibility;
    orderPos: number;
    ownerId: guid;
    parentCompositionId: guid;
    visit(_visitor: IWorkplaceMetadataVisitor, _reverse?: boolean): void;
    seal<T = WorkplaceMetadataComponentSealed>(): T;
}
